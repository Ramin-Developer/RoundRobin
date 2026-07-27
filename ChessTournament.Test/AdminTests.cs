namespace ChessTournament.Test;

[TestFixture]
public class AdminTests
{
    [TestCase(4), TestCase(6), TestCase(8), TestCase(10), TestCase(12), TestCase(14)]
    [TestCase(16), TestCase(18), TestCase(20), TestCase(22), TestCase(24)]
    public void ShouldGenerateCorrectNoOfActualRounds(int noOfPlayers)
    {
        // Arrange
        var noOfDesiredRounds = noOfPlayers - 1;
        var problemDesc = new ProblemDesc(noOfPlayers, noOfDesiredRounds);
        var expected = noOfDesiredRounds;
        var sut = new Admin(problemDesc);

        // Act
        sut.Simulate();
        var actual = sut.NoOfActualRounds;

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase(4)]
    public void Should_generate_correct_rounds(int noOfPlayers)
    {
        // Arrange
        var noOfDesiredRounds = noOfPlayers - 1;
        var problemDesc = new ProblemDesc(noOfPlayers, noOfDesiredRounds);
        var sut = new Admin(problemDesc);
        var expectedMatchesPerRound = noOfPlayers / 2;

        // Act
        sut.Simulate();
        var actual = sut.Rounds;

        // Assert: rounds are generated and every match slot is filled across all rounds
        Assert.That(actual, Is.Not.Empty);
        Assert.That(sut.NoOfMatchesPlayed, Is.EqualTo(actual.Count * expectedMatchesPerRound));
    }
}
