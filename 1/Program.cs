List<Player> players = new()
{
    new Player("John", 20),
    new Player("Eve", 10),
    new Player("Mickael", 15),
    new Player("Emma", 15),
};

Console.WriteLine(String.Join(", ", players));
players.Sort();
Console.WriteLine(String.Join(", ", players));

/// <summary>
///     Create a class player with a name and high score property.
///     Create a list of multiple players. Use the IComparable interface
///     to be able to sort the players based on their high score.
/// </summary>
internal class Player : IComparable
{
    public readonly int HighScore;
    public readonly string Name;

    public Player(string name, int highScore)
    {
        Name = name;
        HighScore = highScore;
    }

    public int CompareTo(object? obj)
    {
        if (obj is not Player player)
            throw new ArgumentException();

        return HighScore - player.HighScore;
    }

    public override string ToString()
    {
        return $"{Name}({HighScore})";
    }
}