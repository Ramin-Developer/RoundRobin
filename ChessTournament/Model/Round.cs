namespace ChessTournament.Model;

public class Round(Interfaces.IProblemDesc problemDesc)
{
    /********************************************** Class Interface **********************************************/
    public int NoOfPlayers { get; } = problemDesc.Players.Count;

    public int NoOfMatchesPerRound { get; } = problemDesc.NoOfMatchesPerRound;

    internal int Cost { get; set; }

    internal int Count => RoundMatches.Count;

    internal bool IsEmpty => RoundMatches.Count == 0;

    internal void Setup()
    {
        RoundMatches = SetupRound().ToList();
        if (RoundMatches.Count == NoOfMatchesPerRound)
        { Cost = RoundCost; }
    }

    public override string ToString() => Display();

    /*********************************************** Private Fields **********************************************/
    private const int IdStep = Utility.IdStep;

    private List<Match> RoundMatches { get; set; } = [];

    private List<Player> Players { get; } = problemDesc.Players.ToList();

    private IEnumerable<HashSet<Match>> AllMatches { get; } = problemDesc.AllMatches;

    private int RoundCost => RoundMatches.Sum(item => Math.Abs(item.SndPlayerRank - item.FstPlayerRank));

    private IEnumerable<Match> SetupRound()
    {
        var matches = new List<Match>();
        int? startSndId = null;
        while (matches.Count < NoOfMatchesPerRound)
        {
            var match = ChooseMatch(startSndId);
            if (match == null && matches.Count == 0)
                return matches;

            if (match == null)
            {
                var lastMatch = matches.Last();
                Utility.UpdateMatch(lastMatch, false);
                matches.Remove(lastMatch);
                startSndId = lastMatch.SndPlayerId + IdStep;
                continue;
            }

            Utility.UpdateMatch(match, true);
            matches.Add(match);
            startSndId = null;
        }

        UpdateDualMatches(matches);
        return matches;
    }

    private void UpdateDualMatches(IEnumerable<Match> matches)
    { matches.ToList().ForEach(item => Utility.UpdateMatch(FindDualMatch(item)!, true)); }

    private Match? FindDualMatch(Match match)
    {
        var matches = Utility.FindAllMatchesFor(match.SndPlayer, AllMatches, Players);
        return matches.FirstOrDefault(item => item.SndPlayerId == match.FstPLayerId);
    }

    private Match? ChooseMatch(int? startSndId)
    {
        var fstPlayer = FindFreePlayer();
        if (fstPlayer == null)
            return null;

        var playerMatches = Utility.FindAllMatchesFor(fstPlayer, AllMatches, Players);

        startSndId ??= fstPlayer.Id + IdStep;

        return playerMatches.FirstOrDefault(match =>
            !(match.SndPlayerId < startSndId) && !match.SndPlayer.IsBusy && !match.IsPlayed);
    }

    private string Display()
    {
        var sb = new StringBuilder();
        foreach (var aMatch in RoundMatches)
            sb.Append($"{aMatch}  ");

        return sb.ToString();
    }

    private Player? FindFreePlayer() => Players.FirstOrDefault(player => !player.IsBusy);
}
