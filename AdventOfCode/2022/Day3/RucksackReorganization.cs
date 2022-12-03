namespace AdventOfCode._2022.Day3;

public class RucksackReorganization
{
    public static void CalculateItemPriorities()
    {
        var rawData = ReadFile();

        var priority = 0;
        foreach (var rucksackItems in rawData)
        {
            var rucksack = new SingleRucksack(rucksackItems);
            priority += rucksack.GetRucksackPriority();
        }

        Console.WriteLine($"Total priority: {priority}");
    }

    public static void CalculateBadgePriorities()
    {
        var rawData = ReadFile();

        var batchSize = 3;
        var batches = rawData.Count / batchSize;
        var priority = 0;

        for (int i = 0; i < batches; i++)
        {
            var sacks = rawData.Skip(i * batchSize).Take(batchSize).ToList();
            var rucksacks = new GroupedRucksacks(sacks);
            priority += rucksacks.GetGroupPriority();
        }

        Console.WriteLine($"Total priority: {priority}");
    }

    private static List<string> ReadFile()
    {
        return FileUtils.ReadFile(Const.FILE_RUCKSACKREORGANIZATION).ToList();
    }
}
