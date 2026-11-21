using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EarlyExploration.Core;

/// <summary>
/// The two Tech Company limits players asked to turn off. The once a day limit is ExploreManager.IsDailyOnceMap: a
/// trip is refused ("You have already been here today") when the map id is in GlobalSetting Explore_DailyOnceMaps
/// (Params3, ids split on ';', "1003" in the game's table) and was entered on this clock day. The game applies it in
/// story runs and casual endless, not in pure endless. The turnstiles in the lobby (furniture 90113, button 1872) check condition set
/// 9346, whose BagRequire is the Staff Badge (42047); an empty BagRequire passes.
/// </summary>
public static class TechCompany
{
    public const int MapPointId = 1003;

    public const string DailyOnceKey = "Explore_DailyOnceMaps";

    public const int GateConditionSetId = 9346;

    public const int BadgeItemId = 42047;

    /// <summary>
    /// The once a day list to keep in the table: the game's list, with the Tech Company taken out when
    /// <paramref name="manyVisits"/> is on. The other ids stay as the game has them.
    /// </summary>
    public static string DailyOnceMaps(string? gameList, bool manyVisits)
    {
        var list = gameList ?? "";

        if (!manyVisits) return list;

        return string.Join(";", list.Split(';').Where(id => !IsTechCompany(id)));
    }

    /// <summary>Whether the game's list makes the Tech Company once a day, read the way the game reads it.</summary>
    public static bool IsOnceADay(string? gameList) => (gameList ?? "").Split(';').Any(IsTechCompany);

    /// <summary>The turnstiles' item list to keep in the table: the game's, without the badge in an endless run with the setting on.</summary>
    public static IReadOnlyList<int> GateItems(IReadOnlyList<int> gameItems, bool noBadge, bool isEndless) =>
        noBadge && isEndless ? gameItems.Where(id => id != BadgeItemId).ToList() : gameItems.ToList();

    private static bool IsTechCompany(string id) =>
        int.TryParse(id.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var mapId) && mapId == MapPointId;
}

/// <summary>
/// Remembers what a table entry held the first time it was seen, which is the game's value. Entries are told apart by
/// object, since a table reload builds fresh ones holding the game's values, so a value this mod wrote is never taken
/// for the game's.
/// </summary>
public sealed class FirstSeen<T>
{
    private readonly Dictionary<long, T> _values = new();

    public T Remember(long entry, T current)
    {
        if (_values.TryGetValue(entry, out var value)) return value;

        _values[entry] = current;

        return current;
    }
}
