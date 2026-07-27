namespace ChessTournament.Test;

public class UtilityMatchTests
{
    [Theory]
    [InlineData(4)]
    [InlineData(6)]
    public void InitializeAllMatches_ProducesNGroupsOfNMatches(int noOfPlayers)
    {
        var players = Utility.InitializePlayers(noOfPlayers).ToList();

        var groups = Utility.InitializeAllMatches(players).ToList();

        Assert.Equal(noOfPlayers, groups.Count);
        Assert.Equal(noOfPlayers * noOfPlayers, groups.Sum(g => g.Count));
    }

    [Fact]
    public void FindAllMatchesFor_ReturnsMatchGroupAtPlayerIndex()
    {
        var players = Utility.InitializePlayers(4).ToList();
        var allMatches = Utility.InitializeAllMatches(players).ToList();

        var matches = Utility.FindAllMatchesFor(players[0], allMatches, players);

        Assert.Equal(allMatches[0], matches);
    }

    [Fact]
    public void ExtractEqualPlayerLists_ReturnsGroupsThatShareMembers()
    {
        var players = Utility.InitializePlayers(4).ToList();
        var listA = new HashSet<Player> { players[0], players[1] };
        var listB = new HashSet<Player> { players[0], players[1] };
        var listC = new HashSet<Player> { players[2] };

        var result = Utility.ExtractEqualPlayerLists([listA, listB, listC]).ToList();

        Assert.Equal(2, result.Count);
        Assert.Contains(listA, result);
        Assert.Contains(listB, result);
    }

    [Fact]
    public void ExtractEqualPlayerLists_ReturnsEmptyWhenAllDistinct()
    {
        var players = Utility.InitializePlayers(4).ToList();
        var listA = new HashSet<Player> { players[0] };
        var listB = new HashSet<Player> { players[1] };

        var result = Utility.ExtractEqualPlayerLists([listA, listB]);

        Assert.Empty(result);
    }

    [Fact]
    public void DisplayRemainigLists_RendersHeaderAndPlayerIds()
    {
        var players = Utility.InitializePlayers(4).ToList();
        var group = new HashSet<Player> { players[0], players[1] };

        var text = Utility.DisplayRemainigLists([group]);

        Assert.Contains("Remaining Groups:", text);
        Assert.Contains(players[1].Id.ToString(), text);
    }
}
