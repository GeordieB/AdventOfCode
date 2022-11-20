using AdventOfCode._2021.Day7;
using System.Diagnostics;

namespace AdventOfCode.Day7._2021;

public class Whales
{
    public static void FindHorizontalPosition()
    {
        var fuelCosts = new Dictionary<int, int>();
        var result = GetMinAndMaxPositions();

        var stopWatch = Stopwatch.StartNew();
        foreach (var possiblePosition in Enumerable.Range(result.Min, result.Max))
        {
            var cost = 0;
            foreach (var position in result.Data)
                cost += Math.Abs(position - possiblePosition);

            fuelCosts[possiblePosition] = cost;
        }
        stopWatch.Stop();
        var elapsedTime = stopWatch.ElapsedMilliseconds;

        var fuelEfficientCost = fuelCosts.Min(x => x.Value);
        var fuelEfficientPosition = fuelCosts.FirstOrDefault(x => x.Value == fuelEfficientCost).Key;
        Console.WriteLine($"The lowest cost is to position: {fuelEfficientPosition} with cost: {fuelEfficientCost}");
        Console.WriteLine($"This took: {elapsedTime}ms");
    }

    public static void FindHorizontalPositionWithIncreasingFuelCosts()
    {
        var fuelCosts = new Dictionary<int, int>();
        var result = GetMinAndMaxPositions();

        var stopWatch = Stopwatch.StartNew();
        foreach (var possiblePosition in Enumerable.Range(result.Min, result.Max))
        {
            var cost = 0;
            foreach (var position in result.Data)
            {
                var localCost = Math.Abs(position - possiblePosition);
                for (int i = 1; i <= localCost; i++)
                    cost += i;
            }
            fuelCosts[possiblePosition] = cost;
        }
        stopWatch.Stop();
        var elapsedTime = stopWatch.ElapsedMilliseconds;

        var fuelEfficientCost = fuelCosts.Min(x => x.Value);
        var fuelEfficientPosition = fuelCosts.FirstOrDefault(x => x.Value == fuelEfficientCost).Key;
        Console.WriteLine($"The lowest cost is to position: {fuelEfficientPosition} with cost: {fuelEfficientCost}");
        Console.WriteLine($"This took: {elapsedTime}ms");
    }

    private static Result GetMinAndMaxPositions()
    {
        var rawData = ReadFile();
        var splitData = rawData.FirstOrDefault().Split(',').Select(x => x.AsInt());

        var min = splitData.Min();
        var max = splitData.Max();

        return new Result
        {
            Min = min,
            Max = max,
            Data = splitData.ToArray()
        };
    }

    private static List<string> ReadFile()
    {
        return FileUtils.ReadFile(Const.FILE_WHALES).ToList();
    }
}
