using System.Collections.Generic;
using System.Linq;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using EarlyExploration.Adapters;
using EarlyExploration.Core;
using ExiledToolkit;
using ExiledToolkit.Kit;

namespace EarlyExploration;

[BepInPlugin(Guid, "Early Exploration", Version)]
[BepInDependency(ExiledToolkit.Plugin.Guid, ">=0.19.0")]
public sealed class Plugin : BasePlugin
{
    private const string Guid = "com.dev.survivallog.earlyexploration";
    private const string Version = "0.1.5";

    /// <summary>Lines add up over many sessions, so past 1 MB the file is moved aside and started again.</summary>
    private const long MaxLogBytes = 1024 * 1024;

    public override void Load()
    {
        var log = new ModLog(Log, "early-exploration.log") { MaxFileBytes = MaxLogBytes };

        log.Write($"===== Early Exploration {Version} started");

        var openDays = Zones.All.ToDictionary(zone => zone.MapPointId, BindOpenDay);

        ZoneOpener.Log = log;
        ZoneOpener.LogError = message =>
        {
            Log.LogError(message);
            log.WriteFileOnly(message);
        };
        LiveSettings.Watch(() => UseOpenDays(openDays, log), openDays.Values.Cast<ConfigEntryBase>().ToArray());
        AddComponent<ZoneOpener>();
    }

    private ConfigEntry<int> BindOpenDay(Zone zone) =>
        Config.Bind("Zones", zone.Key, ZonePlan.GameRule, new ConfigDescription(
            $"The day after the disaster the {zone.English} opens on the exploration map: 1 is the first day. 0, the default, leaves it to open when it normally does ({zone.GameRule}). Opening it early also lifts the story lock, so you can go before the story sends you. Anything else a trip needs, such as items to bring, still applies.\n" +
            $"灾难发生后，「{zone.Chinese}」在探索地图上开放的天数：1 表示第一天。设为 0（默认）则保持原样，和平时一样开放（{zone.GameRuleChinese}）。提前开放时也会解除剧情锁定，不必等剧情引导就能前往。出发所需的其他条件（例如需要携带的物品）照常生效。",
            new AcceptableValueRange<int>(ZonePlan.GameRule, ZonePlan.LatestDay)));

    private static void UseOpenDays(IReadOnlyDictionary<int, ConfigEntry<int>> openDays, ModLog log)
    {
        ZoneOpener.OpenDays = openDays.ToDictionary(pair => pair.Key, pair => pair.Value.Value);
        log.Write("open days: " + string.Join(", ", Zones.All.Select(zone => $"{zone.English} {Describe(openDays[zone.MapPointId].Value)}")));
    }

    private static string Describe(int openDay) => openDay == ZonePlan.GameRule ? "normal" : $"day {openDay}";
}
