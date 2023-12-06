namespace AdventOfCode._2023.Day4;
public class ScratchCards
{
    public static void CalculatePoints()
    {
        var rawCards = ReadFile();
        var totalPoints = 0;

        foreach (var card in rawCards)
        {
            var data = card.Split(':');
            var cardValues = data[1].Split('|');
            var winningNumbers = GetNumberValues(cardValues[0]);
            var numbersOwned = GetNumberValues(cardValues[1]);

            var winners = winningNumbers.Intersect(numbersOwned);
            if (!winners.Any())
                continue;

            Console.WriteLine($"The winning numbers for card {data[0]} are: {string.Join(",", winners)}");
            totalPoints += GetPoints(winners.Count());
        }

        Console.WriteLine($"The cards are worth in total: {totalPoints}");
    }
    public static void CalculatePointsWithCopiedCards()
    {
        var rawCards = ReadFile();
        var originalCards = rawCards.Select(x => x).ToList();

        for (int i = 0; i < originalCards.Count; i++)
        {
            var data = rawCards[i].Split(':');
            var groupedCards = rawCards.Where(p => p.Contains(rawCards[i])).ToList();
            for (int j = 0; j < groupedCards.Count; j++)
            {
                var cardValues = data[1].Split('|');
                var winningNumbers = GetNumberValues(cardValues[0]);
                var numbersOwned = GetNumberValues(cardValues[1]);

                var winners = winningNumbers.Intersect(numbersOwned);
                if (!winners.Any())
                    break;

                rawCards.AddRange(originalCards.Skip(i + 1).Take(winners.Count()).ToList());
            }
        }

        Console.WriteLine($"Total cards : {rawCards.Count}");
    }

    private static List<int> GetNumberValues(string numbers)
    {
        return numbers.Split(" ")
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(int.Parse)
            .ToList();
    }

    private static int GetPoints(int numberOfWinners)
    {
        if (numberOfWinners == 1)
            return 1;

        var sum = 1;
        for (int i = 1; i < numberOfWinners; i++)
            sum *= 2;

        return sum;
    }

    private static List<string> ReadFile()
    {
        return FileUtils.ReadFile(Const.FILE_SCRATCHCARDS).ToList();
    }
}
