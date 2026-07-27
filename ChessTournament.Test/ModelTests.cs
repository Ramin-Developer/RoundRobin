namespace ChessTournament.Test;

[TestFixture]
public class UtilityTests
{
    [TestCase(4)]
    [TestCase(8)]
    [TestCase(12)]
    public void InitializePlayers_CreatesExpectedCountWithSequentialIds(int noOfPlayers)
    {
        var players = Utility.InitializePlayers(noOfPlayers).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(players, Has.Count.EqualTo(noOfPlayers));
            Assert.That(players.Select(p => p.Id),
                Is.EqualTo(Enumerable.Range(Utility.StartPlayerId, noOfPlayers)));
        });
    }

    [Test]
    public void FindPlayerById_ReturnsMatchingPlayer()
    {
        var players = Utility.InitializePlayers(6).ToList();

        var found = Utility.FindPlayerById(Utility.StartPlayerId + 2, players);

        Assert.That(found, Is.Not.Null);
        Assert.That(found!.Id, Is.EqualTo(Utility.StartPlayerId + 2));
    }

    [Test]
    public void FindPlayerById_ReturnsNullWhenAbsent()
    {
        var players = Utility.InitializePlayers(4).ToList();

        var found = Utility.FindPlayerById(9999, players);

        Assert.That(found, Is.Null);
    }

    [Test]
    public void UpdateMatch_TogglesBusyAndPlayedFlags()
    {
        var players = Utility.InitializePlayers(4).ToList();
        var match = new Match(players[0], players[1]);

        Utility.UpdateMatch(match, isPlayed: true);
        Assert.Multiple(() =>
        {
            Assert.That(match.IsPlayed, Is.True);
            Assert.That(match.FstPlayer.IsBusy, Is.True);
            Assert.That(match.SndPlayer.IsBusy, Is.True);
        });

        Utility.UpdateMatch(match, isPlayed: false);
        Assert.Multiple(() =>
        {
            Assert.That(match.IsPlayed, Is.False);
            Assert.That(match.FstPlayer.IsBusy, Is.False);
            Assert.That(match.SndPlayer.IsBusy, Is.False);
        });
    }
}

[TestFixture]
public class RoundTests
{
    [TestCase(4)]
    [TestCase(8)]
    [TestCase(12)]
    public void Setup_FillsAllMatchSlots(int noOfPlayers)
    {
        var problemDesc = new ProblemDesc(noOfPlayers, noOfPlayers - 1);
        var sut = new Round(problemDesc);

        sut.Setup();

        Assert.Multiple(() =>
        {
            Assert.That(sut.NoOfPlayers, Is.EqualTo(noOfPlayers));
            Assert.That(sut.NoOfMatchesPerRound, Is.EqualTo(noOfPlayers / 2));
            Assert.That(sut.Count, Is.EqualTo(noOfPlayers / 2));
            Assert.That(sut.IsEmpty, Is.False);
        });
    }
}
