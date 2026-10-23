using System.Collections.Generic;

namespace EarlyExploration.Core;

/// <summary>One exploration zone on the post-disaster map: its map point id in the game's MapPoint table and its config key.</summary>
public sealed record Zone(int MapPointId, string Key, string English, string Chinese, string GameRule, string GameRuleChinese);

/// <summary>
/// The post-disaster zones the story opens over time. Each map point names a condition set whose SurvivalToDay holds
/// it shut until that day, and the story ones also need a tag (School_Open and the like) that a story event adds. The
/// Gas Station is left out: in story runs the game never shows it (no unlock tag, never marked unlocked), and only the
/// endless mode's start event gives its tag.
/// </summary>
public static class Zones
{
    public static readonly IReadOnlyList<Zone> All = new[]
    {
        new Zone(1000, "HardwareStore", "Hardware Store", "社区检修小屋", "day 5", "第 5 天"),
        new Zone(1004, "DesertedSupermarket", "Deserted Supermarket", "残破超市", "through the story, not before day 5", "随剧情开放，最早第 5 天"),
        new Zone(1002, "AbandonedSchool", "Abandoned School", "废弃学校", "through the story", "随剧情开放"),
        new Zone(1005, "AbandonedHospital", "Abandoned Hospital", "废弃医院", "through the story", "随剧情开放"),
        new Zone(1003, "TechCompany", "Tech Company", "科技公司", "through the story", "随剧情开放"),
    };
}
