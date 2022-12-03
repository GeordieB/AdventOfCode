namespace AdventOfCode._2022.Day3;

public class GroupedRucksacks : Rucksack
{
    public List<string> Items { get; set; }

    public GroupedRucksacks(List<string> items) {
        Items = items;
    }

    public int GetGroupPriority()
    {
        var firstSack = Items.FirstOrDefault();
        var otherSacks = Items.Where(p => p != firstSack).ToList();
        for (int i = 0; i < firstSack.Length; i++)
        {
            if (otherSacks.All(p => p.Contains(firstSack[i])))
            {
                CommonType = firstSack[i];
                break;
            }
        }

        return GetItemPriority();
    }
}
