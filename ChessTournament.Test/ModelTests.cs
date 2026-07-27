namespace ChessTournament.Test;

public class UtilityTests
{
    [Theory]
    [MemberData(nameof(PlayerCounts.Sample), MemberType = typeof(PlayerCounts))]
    public void InitializePlayers_CreatesExpectedCountWithSequentialIds(int noOfPlayers)
    {
        var players = Utility.InitializePlayers(noOfPlayers).ToList();

        Assert.Equal(noOfPlayers, players.Count);
        Assert.Equal(Enumerable.Range(Utility.StartPlayerId, noOfPlayers), players.Select(p => p.Id));
    }

    [Fact]
    public void FindPlayerById_ReturnsMatchingPlayer()
    {
        var players = Utility.InitializePlayers(6).ToList();

        var found = Utility.FindPlayerById(Utility.StartPlayerId + 2, players);

        Assert.NotNull(found);
        Assert.Equal(Utility.StartPlayerId + 2, found!.Id);
    }

    [Fact]
    public void FindPlayerById_ReturnsNullWhenAbsent()
    {
        var players = Utility.InitializePlayers(4).ToList();

        var found = Utility.FindPlayerById(9999, players);

        Assert.Null(found);
    }

    [Fact]
    public void UpdateMatch_TogglesBusyAndPlayedFlags()
    {
        var players = Utility.InitializePlayers(4).ToList();
        var match = new Match(players[0], players[1]);

        Utility.UpdateMatch(match, isPlayed: true);
        Assert.True(match.IsPlayed);
        Assert.True(match.FstPlayer.IsBusy);
        Assert.True(match.SndPlayer.IsBusy);

        Utility.UpdateMatch(match, isPlayed: false);
        Assert.False(match.IsPlayed);
        Assert.False(match.FstPlayer.IsBusy);
        Assert.False(match.SndPlayer.IsBusy);
    }
}

public class RoundTests
{
    [Theory]
    [MemberData(nameof(PlayerCounts.Sample), MemberType = typeof(PlayerCounts))]
    public void Setup_FillsAllMatchSlots(int noOfPlayers)
    {
        var problemDesc = new ProblemDesc(noOfPlayers, noOfPlayers - 1);
        var sut = new Round(problemDesc);

        sut.Setup();

        Assert.Equal(noOfPlayers, sut.NoOfPlayers);
        Assert.Equal(noOfPlayers / 2, sut.NoOfMatchesPerRound);
        Assert.Equal(noOfPlayers / 2, sut.Count);
        Assert.False(sut.IsEmpty);
    }
}
