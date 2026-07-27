namespace ChessTournament.Test;

/// <summary>
/// Reusable, strongly-typed <see cref="TheoryData{T}"/> sources shared across test classes.
/// Centralizing the player-count sets keeps the individual theories readable and avoids
/// duplicated <c>[InlineData]</c> declarations.
/// </summary>
internal static class PlayerCounts
{
    /// <summary>Valid, even player counts spanning the full supported range.</summary>
    public static TheoryData<int> FullRange => [4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24];

    /// <summary>A small representative sample of valid player counts.</summary>
    public static TheoryData<int> Sample => [4, 8, 12];
}
