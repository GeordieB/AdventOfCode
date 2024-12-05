namespace AdventOfCode._2024.Day2;

public static class RedNoseReports
{
    public static void FindSafeReports()
    {
        var reports = FileUtils.ReadFile(Const.FILE_RED_NOSE_REPORTS);
        var safeReports = 0;
        var unsafeReports = 0;

        for (var index = 0; index < reports.Length; index++)
        {
            var levels = reports[index].Split(' ').Select(int.Parse).ToList();
            LevelStatus levelStatus;
            var reportStatus = ReportStatus.Safe;
            var indexToIgnore = -1;

            for (var i = 0; i < levels.Count - 1; i++)
            {
                if (indexToIgnore == i)
                    continue;
                if (indexToIgnore == levels.Count)
                {
                    reportStatus = ReportStatus.Unsafe;
                    unsafeReports++;
                    Console.WriteLine($"Unsafe Level: {string.Join(",", levels)} at index {index}");
                    break;
                }

                levelStatus = GetInitialLevelStatus(indexToIgnore, levels);
                var currentLevelStatus = GetCurrentLevelStatus(indexToIgnore, i, levels);
                if (currentLevelStatus != null && levelStatus != currentLevelStatus)
                {
                    indexToIgnore++;
                    i = -1;
                    continue;
                }

                var difference = GetDifference(indexToIgnore, i, levels);
                if (difference is null or >= 1 and <= 3) continue;
                indexToIgnore++;
                i = -1;
            }

            if (reportStatus is not ReportStatus.Safe) continue;
            Console.WriteLine($"Safe Level: {string.Join(",", levels)} at index {index}");
            safeReports++;
        }

        Console.WriteLine($"There are {safeReports} safe reports");
        Console.WriteLine($"There are {unsafeReports} unsafe reports");
    }

    private static LevelStatus GetInitialLevelStatus(int indexToIgnore, List<int> levels)
    {
        var firstLevel = indexToIgnore == 0 ? 1 : 0;
        var secondLevel = indexToIgnore == firstLevel + 1 ? firstLevel + 2 : firstLevel + 1;
        secondLevel = secondLevel == levels.Count ? secondLevel - 1 : secondLevel;
        return levels.ElementAt(firstLevel) < levels.ElementAt(secondLevel)
            ? LevelStatus.Increasing
            : LevelStatus.Decreasing;
    }

    private static LevelStatus? GetCurrentLevelStatus(int indexToIgnore, int currentIndex, List<int> levels)
    {
        var secondLevel = indexToIgnore == currentIndex + 1 ? currentIndex + 2 : currentIndex + 1;
        secondLevel = secondLevel == levels.Count ? secondLevel - 1 : secondLevel;
        if (secondLevel == indexToIgnore)
            return null;
        return levels.ElementAt(currentIndex) < levels.ElementAt(secondLevel)
            ? LevelStatus.Increasing
            : LevelStatus.Decreasing;
    }

    private static int? GetDifference(int indexToIgnore, int currentIndex, List<int> levels)
    {
        var secondLevel = indexToIgnore == currentIndex + 1 ? currentIndex + 2 : currentIndex + 1;
        secondLevel = secondLevel == levels.Count ? secondLevel - 1 : secondLevel;
        if (secondLevel == indexToIgnore)
            return null;
        return Math.Abs(levels.ElementAt(currentIndex) - levels.ElementAt(secondLevel));
    }
}

public enum LevelStatus
{
    Increasing,
    Decreasing
}

public enum ReportStatus
{
    Safe,
    Unsafe
}