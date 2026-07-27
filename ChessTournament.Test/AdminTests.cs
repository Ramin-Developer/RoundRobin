namespace ChessTournament.Test;

public class AdminTests
{
    private static Admin Simulate(int noOfPlayers)
    {
        var admin = new Admin(new ProblemDesc(noOfPlayers, noOfPlayers - 1));
        admin.Simulate();
        return admin;
    }

    [Theory]
    [MemberData(nameof(PlayerCounts.FullRange), MemberType = typeof(PlayerCounts))]
    public void Simulate_GeneratesRequestedNumberOfRounds(int noOfPlayers)
    {
        var sut = Simulate(noOfPlayers);

        Assert.Equal(noOfPlayers - 1, sut.NoOfActualRounds);
    }

    [Theory]
    [MemberData(nameof(PlayerCounts.Sample), MemberType = typeof(PlayerCounts))]
    public void Simulate_FillsEveryMatchSlotAcrossAllRounds(int noOfPlayers)
    {
        var sut = Simulate(noOfPlayers);
        var expectedMatchesPerRound = noOfPlayers / 2;

        Assert.NotEmpty(sut.Rounds);
        Assert.Equal(sut.Rounds.Count * expectedMatchesPerRound, sut.NoOfMatchesPlayed);
    }

    [Fact]
    public void ToString_ReportsDesiredRoundsMet_AfterSuccessfulSimulation()
    {
        var sut = Simulate(4);

        Assert.True(sut.IsDesiredNoOfRoundsMet);
        Assert.Contains("Met", sut.ToString());
    }
}
