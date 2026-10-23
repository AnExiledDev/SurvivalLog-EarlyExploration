using System;
using System.Collections.Generic;
using EarlyExploration.Core;
using GameCore.HotUpdate;
using GameCore.HotUpdate.Battle;
using GameCore.HotUpdate.Battle.Logic;
using UnityEngine;

namespace EarlyExploration.Adapters;

/// <summary>
/// Opens each zone on its set day. The day lock lives in the zone's condition set (SurvivalToDay), which the explore
/// map and the trip check both read, so it is rewritten in the game's table: checked every second, since the tables
/// load after the plugin and a reload brings back the game's values. The story lock is a tag in the run's TagManager
/// (the map point's UnlockTag and the condition set's EventTag); on the open day the missing ones are added, the same
/// way the story event would, and they are saved with the run. Endless, challenge and tutorial runs get the game's own
/// day locks back.
/// </summary>
public sealed class ZoneOpener : MonoBehaviour
{
    private const float CheckSeconds = 1f;

    internal static IReadOnlyDictionary<int, int> OpenDays = new Dictionary<int, int>();
    internal static ExploreLog? Log;

    private readonly DayGates _gates = new();
    private readonly HashSet<string> _reported = new();
    private float _nextCheckAt;

    public ZoneOpener(IntPtr pointer) : base(pointer)
    {
    }

    private void Update()
    {
        var now = Time.realtimeSinceStartup;

        if (now < _nextCheckAt) return;

        _nextCheckAt = now + CheckSeconds;

        try
        {
            Check();
        }
        catch (Exception ex)
        {
            ReportOnce("failure " + ex.GetType().FullName + ex.Message, "the zone check failed, zones keep the game's rules for now (each distinct failure logged once): " + ex, isError: true);
        }
    }

    private void Check()
    {
        var configs = GameSingleton.Existing<ConfigManager>();
        var mapPoints = configs?._Config_MapPoint_Dict;
        var conditions = configs?._Config_ConditionSet_Dict;

        if (mapPoints == null || conditions == null || mapPoints.Count == 0) return;

        var run = CurrentRun();

        if (run.Kind == RunKind.Loading) return;

        foreach (var zone in Zones.All)
        {
            if (!OpenDays.TryGetValue(zone.MapPointId, out var setting)) continue;

            if (!mapPoints.TryGetValue(zone.MapPointId, out var mapPoint) || mapPoint == null)
            {
                ReportOnce($"missing {zone.Key}", $"{zone.English} (map point {zone.MapPointId}) is not in the game's tables, so it keeps the game's rules", isError: true);

                continue;
            }

            var openDay = run.Kind == RunKind.Story ? setting : ZonePlan.GameRule;

            conditions.TryGetValue(mapPoint._ConditionSetId_k__BackingField, out var condition);

            if (condition != null) SetDayGate(zone, condition, openDay);

            if (run is { Tags: { } tags, Clock: { } clock } && ZonePlan.IsOpenDay(openDay, clock.WorldStateType != WorldStateType.PreDisaster, clock.GetDay())) LiftStoryLock(zone, tags, mapPoint, condition);
        }
    }

    private void SetDayGate(Zone zone, Config_ConditionSet condition, int openDay)
    {
        var current = condition._SurvivalToDay_k__BackingField;

        if (_gates.Target(condition.Pointer.ToInt64(), current, openDay) is not { } target) return;

        condition._SurvivalToDay_k__BackingField = target;
        Log?.Write(openDay == ZonePlan.GameRule
            ? $"{zone.English}: back to the game's own day lock"
            : $"{zone.English}: opens on day {openDay}");
    }

    private static void LiftStoryLock(Zone zone, TagManager tags, Config_MapPoint mapPoint, Config_ConditionSet? condition)
    {
        var zoneTags = new List<string?> { mapPoint._UnlockTag_k__BackingField };

        if (condition?._EventTag_k__BackingField is { } eventTags)
        {
            foreach (var tag in eventTags) zoneTags.Add(tag);
        }

        var missing = ZonePlan.MissingTags(zoneTags, tags.HasTag);

        if (missing.Count == 0) return;

        var gameList = new Il2CppSystem.Collections.Generic.List<string>();

        foreach (var tag in missing) gameList.Add(tag);

        tags.AddTags(gameList);
        Log?.Write($"{zone.English}: opened, added {string.Join(", ", missing)}");
    }

    private enum RunKind
    {
        /// <summary>No run loaded yet, or one still loading: the tables are left as they are.</summary>
        Loading,

        /// <summary>A story run (casual included): zones open on their set days.</summary>
        Story,

        /// <summary>Endless, challenge or tutorial: the game's own day locks are put back.</summary>
        Other,
    }

    private readonly record struct Run(RunKind Kind, TagManager? Tags = null, GameTimeManager? Clock = null);

    private static Run CurrentRun()
    {
        var main = GameSingleton.Existing<BattleMain>();
        var world = GameSingleton.Existing<BattleLogicWorld>();
        var save = GameSingleton.Existing<GameSaveManager>()?.gameSave;

        if (main == null || !main._IsLoadingFinished || main.IsGameOver) return new Run(RunKind.Loading);

        if (world == null || !world.IsUpdate || save == null) return new Run(RunKind.Loading);

        if (world.GameModeKey != GameMode.Default || save.EndlessActive || save.ChallengeTargetId > 0) return new Run(RunKind.Other);

        var tags = world._TagManager;
        var clock = world._GameTimeManager;

        return tags == null || clock == null ? new Run(RunKind.Loading) : new Run(RunKind.Story, tags, clock);
    }

    private void ReportOnce(string key, string message, bool isError)
    {
        if (!_reported.Add(key)) return;

        if (isError) Log?.WriteError(message);
        else Log?.Write(message);
    }
}
