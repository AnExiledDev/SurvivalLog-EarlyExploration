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
[BepInDependency(ExiledToolkit.Plugin.Guid, ">=0.25.0")]
public sealed class Plugin : BasePlugin
{
    internal const string Guid = "com.dev.survivallog.earlyexploration";
    private const string Version = "0.1.11";

    /// <summary>Lines add up over many sessions, so past 1 MB the file is moved aside and started again.</summary>
    private const long MaxLogBytes = 1024 * 1024;

    public override void Load()
    {
        var log = new ModLog(Log, "early-exploration.log") { MaxFileBytes = MaxLogBytes };

        log.Write($"===== Early Exploration {Version} started");

        var openDays = Zones.All.ToDictionary(zone => zone.MapPointId, BindOpenDay);

        var manyVisits = Config.Bind("TechCompany", "MoreThanOneVisitADay", false, new ConfigDescription(
            "Lets you go to the Tech Company more than once a day. Normally a second trip on the same day is refused with \"You have already been here today. Come back tomorrow.\" Off by default.\n" +
            "允许一天内多次前往科技公司。平时同一天第二次前往会被拒绝，提示「今天已经来过了，明天才能再进。」默认关闭。"));
        var noBadge = Config.Bind("TechCompany", "NoBadge", false, new ConfigDescription(
            "The turnstiles in the Tech Company open without a Staff Badge, in story and endless runs. The turnstiles' tip still tells you to swipe one. Off by default.\n" +
            "科技公司的闸机不用工牌也能开启，剧情模式和无尽模式都一样。闸机的提示仍会让你刷工牌。默认关闭。"));

        ZoneOpener.Log = log;
        LiveSettings.Watch(() => UseOpenDays(openDays, log), openDays.Values.Cast<ConfigEntryBase>().ToArray());
        LiveSettings.Watch(() => UseTechCompany(manyVisits.Value, noBadge.Value, log), manyVisits, noBadge);
        AddComponent<ZoneOpener>();
    }

    private ConfigEntry<int> BindOpenDay(Zone zone) =>
        Config.Bind("Zones", zone.Key, ZonePlan.GameRule, new ConfigDescription(
            $"The day the {zone.English} opens on the exploration map: 1 is the first day after the disaster. Leave it at 0, the default, and it opens when it normally does ({zone.GameRule}). Opening it early also lifts any story lock, so you can go before the story sends you there. A trip there still needs everything it normally does, such as items to bring.\n" +
            $"「{zone.Chinese}」在探索地图上开放的天数：1 表示灾难后的第一天。保持 0（默认）则和平时一样开放（{zone.GameRuleChinese}）。提前开放时也会解除剧情锁定，不必等剧情引导就能前往。出发所需的其他条件（例如需要携带的物品）和平时一样。",
            new AcceptableValueRange<int>(ZonePlan.GameRule, ZonePlan.LatestDay)));

    private static void UseOpenDays(IReadOnlyDictionary<int, ConfigEntry<int>> openDays, ModLog log)
    {
        ZoneOpener.OpenDays = openDays.ToDictionary(pair => pair.Key, pair => pair.Value.Value);
        log.Write("open days: " + string.Join(", ", Zones.All.Select(zone => $"{zone.English} {Describe(openDays[zone.MapPointId].Value)}")));
    }

    private static void UseTechCompany(bool manyVisits, bool noBadge, ModLog log)
    {
        TechCompanyLimits.ManyVisits = manyVisits;
        TechCompanyLimits.NoBadge = noBadge;
        log.Write($"Tech Company: more than one visit a day {OnOff(manyVisits)}, no badge for the turnstiles {OnOff(noBadge)}");
    }

    private static string OnOff(bool isOn) => isOn ? "on" : "off";

    private static string Describe(int openDay) => openDay == ZonePlan.GameRule ? "normal" : $"day {openDay}";
}
