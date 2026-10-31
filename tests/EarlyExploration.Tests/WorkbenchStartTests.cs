using EarlyExploration.Core;
using Xunit;

namespace EarlyExploration.Tests;

public class WorkbenchStartTests
{
    [Fact]
    public void Zones_open_on_day_one_start_the_repair_on_the_first_day_after_the_disaster()
    {
        Assert.True(WorkbenchStart.ShouldStart(1, isAfterDisaster: true, clockDay: 0, hasManual: false, isFixed: false));
    }

    [Fact]
    public void Before_the_disaster_nothing_starts()
    {
        Assert.False(WorkbenchStart.ShouldStart(1, isAfterDisaster: false, clockDay: 0, hasManual: false, isFixed: false));
    }

    [Fact]
    public void Nothing_starts_before_the_earliest_open_day()
    {
        Assert.False(WorkbenchStart.ShouldStart(3, isAfterDisaster: true, clockDay: 1, hasManual: false, isFixed: false));
        Assert.True(WorkbenchStart.ShouldStart(3, isAfterDisaster: true, clockDay: 2, hasManual: false, isFixed: false));
    }

    [Fact]
    public void A_run_that_already_found_the_manual_is_left_to_the_game()
    {
        // Giving the reward again would start a second repair that costs Scrap Metal for nothing.
        Assert.False(WorkbenchStart.ShouldStart(1, isAfterDisaster: true, clockDay: 2, hasManual: true, isFixed: false));
    }

    [Fact]
    public void A_repaired_workbench_is_left_alone()
    {
        Assert.False(WorkbenchStart.ShouldStart(1, isAfterDisaster: true, clockDay: 2, hasManual: false, isFixed: true));
    }

    [Fact]
    public void With_every_zone_on_the_game_rule_it_is_left_to_the_game()
    {
        Assert.False(WorkbenchStart.ShouldStart(null, isAfterDisaster: true, clockDay: 20, hasManual: false, isFixed: false));
    }

    [Fact]
    public void A_later_day_still_starts_it_when_the_manual_was_never_found()
    {
        Assert.True(WorkbenchStart.ShouldStart(1, isAfterDisaster: true, clockDay: 9, hasManual: false, isFixed: false));
    }
}
