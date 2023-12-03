namespace AdventOfCode._2023.Day2;
public class CubeConundrum
{
    private static int _maxRedCubes = 12;
    private static int _maxGreenCubes = 13;
    private static int _maxBlueCubes = 14;

    public static void PossibleGames()
    {
        var rawGames = ReadFile();
        var games = BuildGames(rawGames);

        var possibleGames = new List<Game>();
        foreach (var game in games)
        {
            if (IsGamePossible(game))
            {
                Console.WriteLine($"{game.Name} is possible");
                possibleGames.Add(game);
            }
        }

        Console.WriteLine($"The sum of all possible games is: {possibleGames.Select(p => p.Id).Sum()}");
    }

    public static void PossiblePowerGames()
    {
        var rawGames = ReadFile();
        var games = BuildGames(rawGames);

        Console.WriteLine($"The sum of all possible games is: {games.Select(p => p.Power).Sum()}");
    }

    private static bool IsGamePossible(Game game)
    {
        foreach (var round in game.Rounds)
        {
            if (round.RedCubes > _maxRedCubes || round.GreenCubes > _maxGreenCubes || round.BlueCubes > _maxBlueCubes)
                return false;
        }

        return true;
    }

    private static List<Game> BuildGames(List<string> rawGames)
    {
        var result = new List<Game>();
        foreach (var raw in rawGames)
        {
            var game = new Game();
            var idSplit = raw.Split(':');

            game.Name = idSplit[0];
            game.Id = GetNumerberValue(idSplit[0].Trim());

            var rawRounds = idSplit[1].Trim().Split(";");
            game.Rounds = BuildRounds(rawRounds);

            result.Add(game);
        }

        return result;
    }

    private static List<Round> BuildRounds(IEnumerable<string> rawRounds)
    {
        var result = new List<Round>();
        foreach (var rawRound in rawRounds)
        {
            var round = new Round();
            foreach (var raw in rawRound.Split(","))
            {
                var number = GetNumerberValue(raw.Trim());

                if (raw.Contains("red"))
                    round.RedCubes = number;
                else if (raw.Contains("green"))
                    round.GreenCubes = number;
                else if (raw.Contains("blue"))
                    round.BlueCubes = number;

            }
            result.Add(round);
        }

        return result;
    }

    private static int GetNumerberValue(string round)
    {
        var digits = string.Join("", round.Where(char.IsDigit).ToList());
        return int.Parse(digits);
    }

    private static List<string> ReadFile()
    {
        return FileUtils.ReadFile(Const.FILE_CUBECONUNDRUM).ToList();
    }
}
