namespace AdventOfCode._2022.Day3;

public abstract class Rucksack
{
    public char CommonType { get; set; }

    public int GetItemPriority()
    {
        var priority = CommonType % 32;
        priority += IsUpperCase(CommonType) ? 26 : 0;
        return priority;
    }

    private bool IsUpperCase(char value)
    {
        return value.ToString() == value.ToString().ToUpper();
    }
}
