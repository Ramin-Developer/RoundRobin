namespace ChessTournament.Test;

[TestFixture]
public class ProblemDescTests
{
    [TestCase(4)]
    [TestCase(10)]
    [TestCase(30)]
    public void Constructor_SetsExpectedDerivedValues(int noOfPlayers)
    {
        var noOfRounds = noOfPlayers - 1;

        var sut = new ProblemDesc(noOfPlayers, noOfRounds);

        Assert.Multiple(() =>
        {
            Assert.That(sut.NoOfPlayers, Is.EqualTo(noOfPlayers));
            Assert.That(sut.MaxNoOfRounds, Is.EqualTo(noOfPlayers - 1));
            Assert.That(sut.NoOfMatchesPerRound, Is.EqualTo(noOfPlayers / 2));
            Assert.That(sut.NoOfPossibleMatches, Is.EqualTo(noOfPlayers * (noOfPlayers - 1) / 2));
            Assert.That(sut.Players, Has.Count.EqualTo(noOfPlayers));
        });
    }

    [TestCase(2)]   // below MinNoOfPlayers
    [TestCase(32)]  // above MaxNoOfPlayers
    [TestCase(5)]   // odd
    public void Constructor_ThrowsForInvalidPlayerCount(int noOfPlayers)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new ProblemDesc(noOfPlayers, noOfPlayers - 1));
    }

    [TestCase(8, 2)]   // below MinNoOfRounds
    [TestCase(8, 8)]   // above MaxNoOfRounds (max = 7)
    public void Constructor_ThrowsForInvalidRoundCount(int noOfPlayers, int noOfRounds)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new ProblemDesc(noOfPlayers, noOfRounds));
    }

    [Test]
    public void OutputFile_IncludesPlayerCount()
    {
        var sut = new ProblemDesc(8, 7);

        Assert.That(sut.OutputFile, Does.Contain("8"));
    }
}
