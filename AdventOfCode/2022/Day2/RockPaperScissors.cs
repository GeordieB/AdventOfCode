using AdventOfCode._2022.Day1;

namespace AdventOfCode._2022;

public class RockPaperScissors
{
    public static void StrategyGuideBasedOnMove()
    {
        var rawData = ReadFile();

        var score = 0;
        foreach (var strategy in rawData)
        {
            var moves = strategy.Split(' ');
            var playerRound = new PlayerRoundBasedOnMove(moves[0], moves[1]);

            score += playerRound.CalculateScore();
        }

        Console.WriteLine($"Total score: {score}");
    }

    public static void StrategyGuideBasedOnResult()
    {
        var rawData = ReadFile();

        var score = 0;
        foreach (var strategy in rawData)
        {
            var moves = strategy.Split(' ');
            var playerRound = new PlayerRoundBasedOnOutcome(moves[0], moves[1]);

            score += playerRound.CalculateScore();
        }

        Console.WriteLine($"Total score: {score}");
    }

    private static List<string> ReadFile()
    {
        return FileUtils.ReadFile(Const.FILE_ROCK_PAPER_SCISSORS).ToList();
    }
}
