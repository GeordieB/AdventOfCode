using System.Collections.Immutable;

namespace AdventOfCode._2022.Day1;

public class CalorieCounting
{
    public static void FindMaxCalorieElf()
    {
        var rawData = ReadFile();
        var caloriePerElf = new int[rawData.Count(p => string.IsNullOrEmpty(p)) + 1];

        var index = 0;
        foreach (var calories in rawData)
        {
            if (string.IsNullOrEmpty(calories))
            {
                index++;
                continue;
            }

            caloriePerElf[index] += calories.AsInt();
        }

        var maxCalories = caloriePerElf.Max();
        Console.WriteLine($"The elf with the max calories is carrying {maxCalories} calories");
    }

    public static void FindTopThreeCalorieElves()
    {
        var rawData = ReadFile();
        var caloriePerElf = new int[rawData.Count(p => string.IsNullOrEmpty(p)) + 1];

        var index = 0;
        foreach (var calories in rawData)
        {
            if (string.IsNullOrEmpty(calories))
            {
                index++;
                continue;
            }

            caloriePerElf[index] += calories.AsInt();
        }

        var sortedCalories = caloriePerElf.ToImmutableSortedSet();
        var maxCalories = sortedCalories.Last();
        var secondMaxCalories = sortedCalories[sortedCalories.Count - 2];
        var thirdMaxCalories = sortedCalories[sortedCalories.Count - 3];

        var sumTopCalories = maxCalories + secondMaxCalories + thirdMaxCalories;
        Console.WriteLine($"The top elves are carrying {sumTopCalories} calories");
    }

    private static List<string> ReadFile()
    {
        return FileUtils.ReadFile(Const.FILE_CALORIE_COUNTING).ToList();
    }
}