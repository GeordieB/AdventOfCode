namespace AdventOfCode._2022.Day4;

public class Pair
{
    public int Min { get; set; }
    public int Max { get; set; }

    public Pair(string assignment)
    {
        var range = assignment.Split('-');
        Min = int.Parse(range[0]);
        Max = int.Parse(range[1]);
    }
}
