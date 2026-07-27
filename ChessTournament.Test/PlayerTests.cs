namespace ChessTournament.Test;

public class PlayerTests
{
    [Fact]
    public void Compare_ReturnsOne_WhenFirstIdIsGreater()
    {
        var sut = new Player(1, 1);

        Assert.Equal(1, sut.Compare(new Player(5, 1), new Player(3, 1)));
    }

    [Theory]
    [InlineData(3, 5)] // first smaller
    [InlineData(4, 4)] // equal
    public void Compare_ReturnsZero_WhenFirstIdIsNotGreater(int firstId, int secondId)
    {
        var sut = new Player(1, 1);

        Assert.Equal(0, sut.Compare(new Player(firstId, 1), new Player(secondId, 1)));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1_000_001)]
    public void Constructor_ThrowsForIdOutOfRange(int id)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Player(id, 1));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(3001)]
    public void Constructor_ThrowsForRankOutOfRange(int rank)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Player(1, rank));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1_000_001)]
    public void IdSetter_ThrowsForOutOfRange(int id)
    {
        var sut = new Player(1, 1);

        Assert.Throws<ArgumentOutOfRangeException>(() => sut.Id = id);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(3001)]
    public void RankSetter_ThrowsForOutOfRange(int rank)
    {
        var sut = new Player(1, 1);

        Assert.Throws<ArgumentOutOfRangeException>(() => sut.Rank = rank);
    }

    [Fact]
    public void SettersAcceptValidBoundaryValues()
    {
        var sut = new Player(1, 1)
        {
            Id = 1_000_000,
            Rank = 3000
        };

        Assert.Equal(1_000_000, sut.Id);
        Assert.Equal(3000, sut.Rank);
    }
}
