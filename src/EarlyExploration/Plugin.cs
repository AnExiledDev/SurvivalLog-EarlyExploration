using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using EarlyExploration.Adapters;
using EarlyExploration.Core;

namespace EarlyExploration;

[BepInPlugin(Guid, "Early Exploration", Version)]
[BepInDependency(OptionalLive.ToolkitGuid, BepInDependency.DependencyFlags.SoftDependency)]
public sealed class Plugin : BasePlugin
{
    private const string Guid = "com.dev.survivallog.earlyexploration";
    private const string Version = "0.1.1";

    public override void Load()
    {
        if (!OptionalLive.IsToolkitInstalled) Log.LogInfo(OptionalLive.MissingNote);

        var log = new ExploreLog(Log, Path.Combine(Paths.BepInExRootPath, "early-exploration.log"));

        log.TrimIfLarge();
        log.Write($"===== Early Exploration {Version} started");

        var openDays = Zones.All.ToDictionary(zone => zone.MapPointId, BindOpenDay);

        ZoneOpener.Log = log;
        OptionalLive.Watch(() => UseOpenDays(openDays, log), openDays.Values.Cast<ConfigEntryBase>().ToArray());
        AddComponent<ZoneOpener>();
    }

    private ConfigEntry<int> BindOpenDay(Zone zone) =>
        Config.Bind("Zones", zone.Key, 1, new ConfigDescription(
            $"The day after the disaster the {zone.English} opens on the exploration map: 1 is the first day. 0 keeps the game's own rule ({zone.GameRule}). Opening it early also lifts the story lock, so you can go before the story sends you. Anything else a trip needs, such as items to bring, still applies.\n" +
            $"灾难发生后，「{zone.Chinese}」在探索地图上开放的天数：1 表示第一天。设为 0 则沿用游戏原本的规则（{zone.GameRuleChinese}）。提前开放时也会解除剧情锁定，不必等剧情引导就能前往。出发所需的其他条件（例如需要携带的物品）照常生效。",
            new AcceptableValueRange<int>(ZonePlan.GameRule, ZonePlan.LatestDay)));

    private static void UseOpenDays(IReadOnlyDictionary<int, ConfigEntry<int>> openDays, ExploreLog log)
    {
        ZoneOpener.OpenDays = openDays.ToDictionary(pair => pair.Key, pair => pair.Value.Value);
        log.Write("open days: " + string.Join(", ", Zones.All.Select(zone => $"{zone.English} {Describe(openDays[zone.MapPointId].Value)}")));
    }

    private static string Describe(int openDay) => openDay == ZonePlan.GameRule ? "game's rule" : $"day {openDay}";
}
