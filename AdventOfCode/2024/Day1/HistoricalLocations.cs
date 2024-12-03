namespace AdventOfCode._2024.Day1;

public class HistoricalLocations
{
    public static void GetLocationDistance()
    {
        var lanes = GetLocationLanes(out var numberOfLanes);

        var distance = 0;
        for (var i = 0; i < numberOfLanes; i++)
        {
            var smallestLeftLane = lanes.leftLane.Min();
            var smallestRightLane = lanes.rightLane.Min();

            Console.WriteLine($"Smallest left lane Id is: {smallestLeftLane}");
            Console.WriteLine($"Smallest right lane Id is: {smallestRightLane}");
            Console.WriteLine($"Distance is: {Math.Abs(smallestLeftLane - smallestRightLane)}");
            distance += Math.Abs(smallestLeftLane - smallestRightLane);

            lanes.leftLane.Remove(smallestLeftLane);
            lanes.rightLane.Remove(smallestRightLane);
        }

        Console.WriteLine($"Total distance is: {distance}");
    }

    public static void GetSimillarityScore()
    {
        var lanes = GetLocationLanes(out var numberOfLanes);

        var score = 0;
        for (var i = 0; i < numberOfLanes; i++)
        {
            var currentLocationId = lanes.leftLane.ElementAt(i);
            score += lanes.rightLane.Count(x => x == currentLocationId) * currentLocationId;
        }

        Console.WriteLine($"Total score is: {score}");
    }

    private static (List<int> leftLane, List<int> rightLane) GetLocationLanes(out int numberOfLocations)
    {
        var leftLane = new List<int>();
        var rightLane = new List<int>();
        var locations = FileUtils.ReadFile(Const.FILE_HISTORICAL_LOCATIONS);
        numberOfLocations = locations.Length;

        foreach (var location in locations)
        {
            var locationLane = location.Split(" ").Where(p => !string.IsNullOrWhiteSpace(p)).ToList();
            leftLane.Add(int.Parse(locationLane.First()));
            rightLane.Add(int.Parse(locationLane.Last()));
        }

        return (leftLane, rightLane);
    }
}