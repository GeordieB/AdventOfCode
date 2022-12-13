namespace AdventOfCode._2022.Day4;

public class AssignmentPairs
{
    public static void FindOverlaps()
    {
        var rawData = ReadFile();
        var fullyOverlapPairs = 0;
        var partiallyOverlapPairs = 0;

        foreach (var data in rawData)
        {
            var assignments = data.Split(',');
            var firstPair = new Pair(assignments[0]);
            var secondPair = new Pair(assignments[1]);

            if(PairsFullyOverlap(firstPair, secondPair) ||
                PairsFullyOverlap(secondPair, firstPair))
                fullyOverlapPairs++;

            if (PairsPartiallyOverlap(firstPair, secondPair) ||
                PairsPartiallyOverlap(secondPair, firstPair))
                partiallyOverlapPairs++;
        }

        Console.WriteLine($"There are {fullyOverlapPairs} fully overlapping assignments");
        Console.WriteLine($"There are {partiallyOverlapPairs} partially overlapping assignments");
    }

    private static bool PairsFullyOverlap(Pair firstPair, Pair secondPair)
    {
        var range = GetRange(firstPair.Min, firstPair.Max);
        return range.Contains(secondPair.Min) && range.Contains(secondPair.Max);
    }

    private static bool PairsPartiallyOverlap(Pair firstPair, Pair secondPair)
    {
        var range = GetRange(firstPair.Min, firstPair.Max);
        return range.Contains(secondPair.Min) || range.Contains(secondPair.Max);
    }

    private static IEnumerable<int> GetRange(int min, int max)
    {
        return Enumerable.Range(min, max - min + 1);
    }

    private static List<string> ReadFile()
    {
        return FileUtils.ReadFile(Const.FILE_ASSIGNMENTPAIRS).ToList();
    }

}
