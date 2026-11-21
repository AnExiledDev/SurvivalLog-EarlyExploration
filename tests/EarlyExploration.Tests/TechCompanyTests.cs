using System.Collections.Generic;
using EarlyExploration.Core;
using Xunit;

namespace EarlyExploration.Tests;

public class TechCompanyTests
{
    [Fact]
    public void More_visits_takes_the_tech_company_out_of_the_once_a_day_list()
    {
        // An empty list is read by the game as "no zone is once a day".
        Assert.Equal("", TechCompany.DailyOnceMaps("1003", manyVisits: true));
    }

    [Fact]
    public void More_visits_keeps_every_other_once_a_day_zone_as_the_game_has_it()
    {
        Assert.Equal("1005;2000", TechCompany.DailyOnceMaps("1005; 1003 ;2000", manyVisits: true));
    }

    [Fact]
    public void With_the_setting_off_the_list_is_the_games()
    {
        Assert.Equal("1005; 1003", TechCompany.DailyOnceMaps("1005; 1003", manyVisits: false));
    }

    [Theory]
    [InlineData("1003", true)]
    [InlineData(" 1003 ;1005", true)]
    [InlineData("10030;1005", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void The_tech_company_counts_as_once_a_day_only_when_its_id_is_in_the_list(string? list, bool expected)
    {
        Assert.Equal(expected, TechCompany.IsOnceADay(list));
    }

    [Fact]
    public void With_the_setting_on_the_turnstiles_stop_asking_for_the_badge()
    {
        var items = TechCompany.GateItems(new[] { TechCompany.BadgeItemId }, noBadge: true);

        Assert.Empty(items);
    }

    [Fact]
    public void Only_the_badge_is_dropped_so_anything_else_the_turnstiles_need_stays()
    {
        var items = TechCompany.GateItems(new[] { 555, TechCompany.BadgeItemId }, noBadge: true);

        Assert.Equal(new[] { 555 }, items);
    }

    [Fact]
    public void With_the_setting_off_the_turnstiles_keep_the_badge()
    {
        var items = TechCompany.GateItems(new[] { TechCompany.BadgeItemId }, noBadge: false);

        Assert.Equal(new[] { TechCompany.BadgeItemId }, items);
    }

    [Fact]
    public void The_first_value_seen_is_kept_as_the_games_even_after_the_mod_writes_its_own()
    {
        var seen = new FirstSeen<string>();

        seen.Remember(1, "1003");

        Assert.Equal("1003", seen.Remember(1, ""));
    }

    [Fact]
    public void A_reloaded_table_entry_is_a_new_entry_with_the_games_value()
    {
        var seen = new FirstSeen<IReadOnlyList<int>>();

        seen.Remember(1, new List<int>());

        Assert.Equal(new[] { TechCompany.BadgeItemId }, seen.Remember(2, new List<int> { TechCompany.BadgeItemId }));
    }
}
