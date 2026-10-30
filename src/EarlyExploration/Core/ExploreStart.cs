using System.Collections.Generic;
using System.Linq;

namespace EarlyExploration.Core;

/// <summary>
/// The front door only offers a trip once the run has the CanExplore tag, and every zone costs a Noisemaker. Both come
/// from one story event ("Exploration Tool", 10350) that the game plays on day 7 at 14:00: it teaches the Noisemaker
/// recipe and grants CanExplore when one is made. So when a zone opens before day 7, that event is started on the
/// earliest open day instead, and the player goes through it the same way.
/// </summary>
public static class ExploreStart
{
    public const int EventId = 10350;

    public const string CanExploreTag = "CanExplore";

    /// <summary>The earliest day any zone opens, or null when every zone keeps the game's rule.</summary>
    public static int? EarliestOpenDay(IEnumerable<int> openDays)
    {
        var set = openDays.Where(day => day > ZonePlan.GameRule).ToList();

        return set.Count == 0 ? null : set.Min();
    }

    /// <summary>
    /// Whether to start the event now. Never before the disaster, never once the run can explore or the event has
    /// fired, and never on the game's own day for it (the game has it queued for 14:00 then). A later day still
    /// starts it, in case the game's own day went by without it.
    /// </summary>
    public static bool ShouldStart(int? earliestOpenDay, int gameDay, bool isAfterDisaster, int clockDay, bool canExplore, bool hasFired)
    {
        if (earliestOpenDay is not { } openDay || canExplore || hasFired) return false;

        if (!ZonePlan.IsOpenDay(openDay, isAfterDisaster, clockDay)) return false;

        return clockDay + 1 != gameDay;
    }
}
