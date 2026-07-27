namespace ChessTournament.Test;

public class ProblemDescTests
{
    [Theory]
    [InlineData(4)]
    [InlineData(10)]
    [InlineData(30)]
    public void Constructor_SetsExpectedDerivedValues(int noOfPlayers)
    {
        var noOfRounds = noOfPlayers - 1;

        var sut = new ProblemDesc(noOfPlayers, noOfRounds);

        Assert.Equal(noOfPlayers, sut.NoOfPlayers);
        Assert.Equal(noOfPlayers - 1, sut.MaxNoOfRounds);
        Assert.Equal(noOfPlayers / 2, sut.NoOfMatchesPerRound);
        Assert.Equal(noOfPlayers * (noOfPlayers - 1) / 2, sut.NoOfPossibleMatches);
        Assert.Equal(noOfPlayers, sut.Players.Count);
    }

    [Theory]
    [InlineData(2)]   // below MinNoOfPlayers
    [InlineData(32)]  // above MaxNoOfPlayers
    [InlineData(5)]   // odd
    public void Constructor_ThrowsForInvalidPlayerCount(int noOfPlayers)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new ProblemDesc(noOfPlayers, noOfPlayers - 1));
    }

    [Theory]
    [InlineData(8, 2)]   // below MinNoOfRounds
    [InlineData(8, 8)]   // above MaxNoOfRounds (max = 7)
    public void Constructor_ThrowsForInvalidRoundCount(int noOfPlayers, int noOfRounds)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new ProblemDesc(noOfPlayers, noOfRounds));
    }

    [Fact]
    public void OutputFile_IncludesPlayerCount()
    {
        var sut = new ProblemDesc(8, 7);

        Assert.Contains("8", sut.OutputFile);
    }
}
