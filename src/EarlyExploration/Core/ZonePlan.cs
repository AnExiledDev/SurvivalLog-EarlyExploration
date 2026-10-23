using System;
using System.Collections.Generic;
using System.Linq;

namespace EarlyExploration.Core;

/// <summary>
/// The rules for one zone's open day. Days are counted as the game shows them: day 1 is the first day after the
/// disaster. The game's clock counts from 0 and a condition set holds a zone shut while clock day &lt; SurvivalToDay,
/// so opening on shown day N means SurvivalToDay N - 1.
/// </summary>
public static class ZonePlan
{
    /// <summary>The setting that leaves a zone to the game's own rule.</summary>
    public const int GameRule = 0;

    public const int LatestDay = 100;

    /// <summary>The SurvivalToDay that opens the zone on <paramref name="openDay"/>; null keeps the game's own value.</summary>
    public static int? DayGate(int openDay) => openDay > GameRule ? openDay - 1 : null;

    /// <summary>Whether the zone's story lock should be lifted now. Never before the disaster.</summary>
    public static bool IsOpenDay(int openDay, bool isAfterDisaster, int clockDay) =>
        openDay > GameRule && isAfterDisaster && clockDay + 1 >= openDay;

    /// <summary>The zone's unlock tags the run does not have yet, each once.</summary>
    public static IReadOnlyList<string> MissingTags(IEnumerable<string?> zoneTags, Func<string, bool> hasTag) =>
        zoneTags.Where(tag => !string.IsNullOrEmpty(tag)).Select(tag => tag!).Distinct().Where(tag => !hasTag(tag)).ToList();
}

/// <summary>
/// Remembers each condition set's own SurvivalToDay the first time it is seen, so a zone set back to the game's rule
/// gets the game's value back. Entries are told apart by object (a table reload builds fresh ones holding the game's
/// values), and a value this mod wrote is never taken for the game's.
/// </summary>
public sealed class DayGates
{
    private readonly Dictionary<long, int> _gameValues = new();

    /// <summary>The value to write into the entry now, or null when it already holds the right one.</summary>
    public int? Target(long entry, int current, int openDay)
    {
        if (!_gameValues.TryGetValue(entry, out var gameValue))
        {
            gameValue = current;
            _gameValues[entry] = gameValue;
        }

        var target = ZonePlan.DayGate(openDay) ?? gameValue;

        return target == current ? null : target;
    }
}
