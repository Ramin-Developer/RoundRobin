namespace ChessTournament.Model;

public class Player(int id, int rank)
{
    /********************************************** Class Interface **********************************************/
    public int Compare(Player? x, Player? y) => x!.Id > y!.Id ? 1 : 0;

    internal int Id
    {
        get => _id;

        set
        {
            if (0 > value || value > 1000000)
                throw new ArgumentOutOfRangeException(nameof(Id), value, "Value of Player ID is out of range!");

            _id = value;
        }
    }

    internal int Rank
    {
        get => _rank;

        set
        {
            if (0 > value || value > 3000)
                throw new ArgumentOutOfRangeException(nameof(Rank), value, "Value of Rank is out of range!");

            _rank = value;
        }
    }

    internal bool IsBusy { get; set; }

    /*********************************************** Private Fields **********************************************/
    private int _id = id is >= 0 and <= 1000000
        ? id
        : throw new ArgumentOutOfRangeException(nameof(id), id, "Value of Player ID is out of range!");

    private int _rank = rank is >= 0 and <= 3000
        ? rank
        : throw new ArgumentOutOfRangeException(nameof(rank), rank, "Value of Rank is out of range!");
}
