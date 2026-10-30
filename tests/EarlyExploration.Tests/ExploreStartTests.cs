using EarlyExploration.Core;
using Xunit;

namespace EarlyExploration.Tests;

public class ExploreStartTests
{
    // The game plays the Noisemaker event on shown day 7.
    private const int GameDay = 7;

    [Fact]
    public void The_earliest_set_day_wins_and_game_rule_zones_are_ignored()
    {
        Assert.Equal(3, ExploreStart.EarliestOpenDay(new[] { 0, 5, 3, 0, 9 }));
    }

    [Fact]
    public void Every_zone_on_the_game_rule_means_no_early_start()
    {
        Assert.Null(ExploreStart.EarliestOpenDay(new[] { 0, 0, 0 }));
    }

    [Fact]
    public void Zones_open_on_day_one_start_the_event_on_the_first_day_after_the_disaster()
    {
        Assert.True(ExploreStart.ShouldStart(1, GameDay, isAfterDisaster: true, clockDay: 0, canExplore: false, hasFired: false));
    }

    [Fact]
    public void Before_the_disaster_nothing_starts()
    {
        Assert.False(ExploreStart.ShouldStart(1, GameDay, isAfterDisaster: false, clockDay: 0, canExplore: false, hasFired: false));
    }

    [Fact]
    public void Nothing_starts_before_the_earliest_open_day()
    {
        Assert.False(ExploreStart.ShouldStart(3, GameDay, isAfterDisaster: true, clockDay: 1, canExplore: false, hasFired: false));
        Assert.True(ExploreStart.ShouldStart(3, GameDay, isAfterDisaster: true, clockDay: 2, canExplore: false, hasFired: false));
    }

    [Fact]
    public void A_run_that_can_already_explore_is_left_alone()
    {
        Assert.False(ExploreStart.ShouldStart(1, GameDay, isAfterDisaster: true, clockDay: 2, canExplore: true, hasFired: false));
    }

    [Fact]
    public void The_event_never_starts_twice()
    {
        Assert.False(ExploreStart.ShouldStart(1, GameDay, isAfterDisaster: true, clockDay: 2, canExplore: false, hasFired: true));
    }

    [Fact]
    public void On_the_games_own_day_the_game_plays_it_itself()
    {
        Assert.False(ExploreStart.ShouldStart(1, GameDay, isAfterDisaster: true, clockDay: GameDay - 1, canExplore: false, hasFired: false));
    }

    [Fact]
    public void A_day_after_the_games_own_day_still_starts_it_if_it_never_played()
    {
        Assert.True(ExploreStart.ShouldStart(1, GameDay, isAfterDisaster: true, clockDay: GameDay, canExplore: false, hasFired: false));
    }

    [Fact]
    public void With_every_zone_on_the_game_rule_it_is_left_to_the_game()
    {
        Assert.False(ExploreStart.ShouldStart(null, GameDay, isAfterDisaster: true, clockDay: 20, canExplore: false, hasFired: false));
    }
}
