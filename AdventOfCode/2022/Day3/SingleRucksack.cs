namespace AdventOfCode._2022.Day3;

public class SingleRucksack : Rucksack
{
    public string Items { get; set; }

    public SingleRucksack(string items)
    {
        Items = items;
    }

    public int GetRucksackPriority()
    {
        var halwayMark = Items.Length / 2;
        var firstCompartment = Items.Substring(0, halwayMark);
        var secondCompartment = Items.Substring(halwayMark);

        for (int i = 0; i < firstCompartment.Length; i++)
        {
            if (secondCompartment.Any(p => p == firstCompartment[i]))
            {
                CommonType = firstCompartment[i];
                break;
            }
        }

        return GetItemPriority();
    }
}
