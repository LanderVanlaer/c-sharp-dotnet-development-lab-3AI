// See https://aka.ms/new-console-template for more information


// Write a program that displays the best players of a video game. Create a class PlayerStats that has the
// following properties: name, timeplayed, headshots, totalscore. Create 3 methods that accept a list of
// PlayerStats as a parameter: MostTimePlayed, MostHeadshots, HighestTotalScore. Test your program with some
// testdata and output the player name returned by the three functions.
//
// The property that is used to pick the best player is now hard coded inside the functions.
// (ex: score = player.headshots; ) Use a delegate so that you only have one method to call:
// GetBestPlayer that has a list of playerstats and a delage as parameters and that returns the name of the
// player. You have to create 3 methods that return the integer that you want to compare.

List<PlayerStats> l = new()
{
    new PlayerStats("John", 200, 3, 150),
    new PlayerStats("Marie", 150, 4, 170),
    new PlayerStats("Jane", 190, 1, 110),
    new PlayerStats("Frank", 200, 0, 200),
};

Console.WriteLine("MostTimePlayed " + PlayerStats.MostTimePlayed(l)?.Name);
Console.WriteLine("MostHeadshots " + PlayerStats.MostHeadshots(l)?.Name);
Console.WriteLine("HighestTotalScore " + PlayerStats.HighestTotalScore(l)?.Name);
Console.WriteLine();
Console.WriteLine("GetBestPlayer(MostTimePlayed) " + PlayerStats.GetBestPlayer(l, stats => stats.TimePlayed)?.Name);
Console.WriteLine("GetBestPlayer(MostHeadshots) " + PlayerStats.GetBestPlayer(l, stats => stats.Headshots)?.Name);
Console.WriteLine("GetBestPlayer(HighestTotalScore) " + PlayerStats.GetBestPlayer(l, stats => stats.TotalScore)?.Name);

internal class PlayerStats
{
    public int Headshots;
    public string Name;
    public int TimePlayed;
    public int TotalScore;

    public PlayerStats(string name, int timePlayed, int headshots, int totalScore)
    {
        Name = name;
        TimePlayed = timePlayed;
        Headshots = headshots;
        TotalScore = totalScore;
    }

    public static PlayerStats? MostTimePlayed(IEnumerable<PlayerStats> enumerable)
    {
        return enumerable.MaxBy(p => p.TimePlayed);
    }

    public static PlayerStats? MostHeadshots(IEnumerable<PlayerStats> enumerable)
    {
        return enumerable.MaxBy(p => p.Headshots);
    }

    public static PlayerStats? HighestTotalScore(IEnumerable<PlayerStats> enumerable)
    {
        return enumerable.MaxBy(p => p.TotalScore);
    }

    public delegate int BestCalc(PlayerStats playerStats);

    public static PlayerStats? GetBestPlayer(IEnumerable<PlayerStats> enumerable, BestCalc bestCalc)
    {
        return enumerable.MaxBy(bestCalc.Invoke);
    }
}