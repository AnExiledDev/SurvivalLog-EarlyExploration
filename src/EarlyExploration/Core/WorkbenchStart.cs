namespace EarlyExploration.Core;

/// <summary>
/// A Noisemaker is made with the workbench's Handcraft button, which only shows once the workbench is repaired
/// (the WorkDeskFixed tag). The game starts the repair when the player finds the workbench manual (a "Search for the
/// Manual" event from day 4) or, failing that, works it out alone on day 6. So when a zone opens earlier, the
/// manual's own reward is given on the earliest open day instead: it adds WorkDeskBook, which ends both of those
/// routes, and starts the repair event for the character being played, the same way finding the manual would.
/// </summary>
public static class WorkbenchStart
{
    /// <summary>The reward the game gives when the player finds the workbench manual (story 1318).</summary>
    public const int ManualFoundRewardSetId = 10033;

    /// <summary>Set once the manual is found or the player starts working the workbench out alone.</summary>
    public const string ManualTag = "WorkDeskBook";

    /// <summary>Set once the workbench is repaired.</summary>
    public const string FixedTag = "WorkDeskFixed";

    /// <summary>
    /// Whether to give the manual's reward now. Only on or after the earliest open day, never before the disaster,
    /// and never once the run has the manual or a repaired workbench (the game's own route got there first, or this
    /// already ran: the tags are saved with the run).
    /// </summary>
    public static bool ShouldStart(int? earliestOpenDay, bool isAfterDisaster, int clockDay, bool hasManual, bool isFixed)
    {
        if (earliestOpenDay is not { } openDay || hasManual || isFixed) return false;

        return ZonePlan.IsOpenDay(openDay, isAfterDisaster, clockDay);
    }
}
