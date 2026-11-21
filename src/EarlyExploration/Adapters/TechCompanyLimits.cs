using System;
using System.Collections.Generic;
using System.Linq;
using EarlyExploration.Core;
using ExiledToolkit;
using ExiledToolkit.Kit;
using GameCore.HotUpdate;

namespace EarlyExploration.Adapters;

/// <summary>
/// Writes the two Tech Company settings into the game's tables (see TechCompany): the once a day list in GlobalSetting
/// and the turnstiles' item list in its condition set. The game reads both each time it checks, so a change applies at once.
/// Each entry gets the game's value back when its setting is off, and Mod Manager is told when an entry changes.
/// </summary>
internal sealed class TechCompanyLimits
{
    internal static bool ManyVisits;
    internal static bool NoBadgeInEndless;

    private readonly FirstSeen<string> _gameLists = new();
    private readonly FirstSeen<IReadOnlyList<int>> _gameGateItems = new();
    private readonly Action<string, string, bool> _reportOnce;
    private string? _reportedList;
    private string? _reportedGateItems;

    public TechCompanyLimits(Action<string, string, bool> reportOnce) => _reportOnce = reportOnce;

    public void Apply(ConfigManager configs, ModLog? log) => KeepDailyOnceList(configs, log);

    /// <summary>The gate only changes once the run is known, so a run still loading keeps what the table holds.</summary>
    public void ApplyToRun(ConfigManager configs, bool isEndless, ModLog? log) => KeepGateItems(configs, isEndless, log);

    private void KeepDailyOnceList(ConfigManager configs, ModLog? log)
    {
        if (configs._Config_GlobalSetting_Dict is not { } table) return;

        if (!table.TryGetValue(TechCompany.DailyOnceKey, out var entry) || entry == null)
        {
            if (ManyVisits) _reportOnce("missing daily once", $"the game has no {TechCompany.DailyOnceKey} setting, so the Tech Company stays once a day", true);

            return;
        }

        var current = entry._Params3_k__BackingField ?? "";
        var gameList = _gameLists.Remember(entry.Pointer.ToInt64(), current);

        if (ManyVisits && !TechCompany.IsOnceADay(gameList)) _reportOnce("not daily once", $"the Tech Company is not once a day in this game version ({TechCompany.DailyOnceKey} is \"{gameList}\"), so there is nothing to change", false);

        var target = TechCompany.DailyOnceMaps(gameList, ManyVisits);

        if (target != current)
        {
            entry._Params3_k__BackingField = target;
            log?.Write(target == gameList ? "Tech Company: back to one visit a day" : "Tech Company: any number of visits a day");
        }

        if (_reportedList == target) return;

        _reportedList = target;

        const string field = nameof(Config_GlobalSetting.Params3);

        if (target == gameList) Claims.PutBack(Plugin.Guid, nameof(Config_GlobalSetting), TechCompany.DailyOnceKey, field);
        else Claims.Wrote(Plugin.Guid, nameof(Config_GlobalSetting), TechCompany.DailyOnceKey, field, target);
    }

    private void KeepGateItems(ConfigManager configs, bool isEndless, ModLog? log)
    {
        if (configs._Config_ConditionSet_Dict is not { } table) return;

        if (!table.TryGetValue(TechCompany.GateConditionSetId, out var entry) || entry == null || entry._BagRequire_k__BackingField is not { } items)
        {
            if (NoBadgeInEndless) _reportOnce("missing gate", $"the Tech Company turnstiles check (condition set {TechCompany.GateConditionSetId}) is not in the game's tables, so the turnstiles still need the Staff Badge", true);

            return;
        }

        var current = ToList(items);
        var gameItems = _gameGateItems.Remember(entry.Pointer.ToInt64(), current);

        if (NoBadgeInEndless && !gameItems.Contains(TechCompany.BadgeItemId)) _reportOnce("no badge", $"the Tech Company turnstiles do not need the Staff Badge ({TechCompany.BadgeItemId}) in this game version, so there is nothing to change", false);

        var target = TechCompany.GateItems(gameItems, NoBadgeInEndless, isEndless);
        var isGameValue = target.SequenceEqual(gameItems);

        if (!target.SequenceEqual(current))
        {
            items.Clear();

            foreach (var id in target) items.Add(id);

            log?.Write(isGameValue ? "Tech Company: the turnstiles need the Staff Badge again" : "Tech Company: the turnstiles open without the Staff Badge in this endless run");
        }

        var reported = string.Join(",", target);

        if (_reportedGateItems == reported) return;

        _reportedGateItems = reported;

        const string field = nameof(Config_ConditionSet.BagRequire);

        if (isGameValue) Claims.PutBack(Plugin.Guid, nameof(Config_ConditionSet), TechCompany.GateConditionSetId, field);
        else Claims.Wrote(Plugin.Guid, nameof(Config_ConditionSet), TechCompany.GateConditionSetId, field, reported);
    }

    private static IReadOnlyList<int> ToList(Il2CppSystem.Collections.Generic.List<int> items)
    {
        var list = new List<int>(items.Count);

        for (var i = 0; i < items.Count; i++) list.Add(items[i]);

        return list;
    }
}
