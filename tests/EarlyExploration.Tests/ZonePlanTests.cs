using EarlyExploration.Core;
using Xunit;

namespace EarlyExploration.Tests;

public class ZonePlanTests
{
    [Fact]
    public void Day_one_clears_the_day_lock_so_the_zone_opens_on_the_first_day_after_the_disaster()
    {
        // The game keeps a zone shut while its clock day (from 0) is below SurvivalToDay.
        Assert.Equal(0, ZonePlan.DayGate(1));
    }

    [Fact]
    public void A_later_day_holds_the_zone_shut_until_that_shown_day()
    {
        var gate = ZonePlan.DayGate(8)!.Value;

        Assert.True(6 < gate, "shown day 7 (clock 6) must still be locked");
        Assert.False(7 < gate, "shown day 8 (clock 7) must be open");
    }

    [Fact]
    public void The_game_rule_setting_keeps_the_games_own_day()
    {
        Assert.Null(ZonePlan.DayGate(ZonePlan.GameRule));
    }

    [Theory]
    [InlineData(1, true, 0, true)]
    [InlineData(3, true, 1, false)]
    [InlineData(3, true, 2, true)]
    [InlineData(3, true, 9, true)]
    [InlineData(1, false, 5, false)]
    [InlineData(0, true, 40, false)]
    public void The_story_lock_lifts_on_the_open_day_after_the_disaster_and_not_before(int openDay, bool isAfterDisaster, int clockDay, bool expected)
    {
        Assert.Equal(expected, ZonePlan.IsOpenDay(openDay, isAfterDisaster, clockDay));
    }

    [Fact]
    public void Only_tags_the_run_lacks_are_added_and_each_once()
    {
        var had = new HashSet<string> { "Office_Open" };

        var missing = ZonePlan.MissingTags(new[] { "School_Open", "", null, "School_Open", "Office_Open" }, had.Contains);

        Assert.Equal(new[] { "School_Open" }, missing);
    }

    [Fact]
    public void Setting_a_zone_back_to_the_game_rule_restores_the_games_value()
    {
        var gates = new DayGates();

        Assert.Equal(0, gates.Target(entry: 1, current: 4, openDay: 1));
        Assert.Null(gates.Target(entry: 1, current: 0, openDay: 1));
        Assert.Equal(4, gates.Target(entry: 1, current: 0, openDay: ZonePlan.GameRule));
    }

    [Fact]
    public void A_reloaded_table_entry_is_read_fresh_and_still_changed()
    {
        var gates = new DayGates();

        gates.Target(entry: 1, current: 4, openDay: 2);

        Assert.Equal(1, gates.Target(entry: 2, current: 4, openDay: 2));
        Assert.Equal(4, gates.Target(entry: 2, current: 1, openDay: ZonePlan.GameRule));
    }

    [Fact]
    public void Every_zone_has_a_distinct_key_and_map_point()
    {
        Assert.Equal(Zones.All.Count, Zones.All.Select(z => z.Key).Distinct().Count());
        Assert.Equal(Zones.All.Count, Zones.All.Select(z => z.MapPointId).Distinct().Count());
        Assert.DoesNotContain(Zones.All, z => z.MapPointId == 1001);
    }
}
