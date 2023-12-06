namespace AdventOfCode._2023.Day3;
public class GearRatios
{
    private static readonly List<char> _symbols = ['`', '~', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '+', '=', '/', ',', '<', '>', '?'];

    public static void PartNumbers()
    {
        var raw = ReadFile();

        var partNumbers = new List<long>();
        for (int i = 0; i < raw.Count; i++)
        {
            var numbers = new List<char>();
            var isValidPart = false;
            for (int j = 0; j < raw[i].Length; j++)
            {
                var character = raw[i][j];
                if (!char.IsDigit(character))
                {
                    AddValidPartNumber(partNumbers, i, numbers, isValidPart);
                    numbers.Clear();
                    isValidPart = false;
                    continue;
                }

                numbers.Add(character);
                isValidPart = IsValidPart(raw, i, isValidPart, j);

                if (IsLastColumn(raw, i, j))
                    AddValidPartNumber(partNumbers, i, numbers, isValidPart);
            }
        }

        Console.WriteLine($"The sum of all valid parts is: {partNumbers.Sum()}");
    }

    public static void GearRatioSum()
    {
        var raw = ReadFile();

        var gears = new Dictionary<(int row, int column), List<long>>();
        for (int i = 0; i < raw.Count; i++)
        {
            var numbers = new List<char>();
            var starLocation = (0, 0);
            var isValidGear = false;
            for (int j = 0; j < raw[i].Length; j++)
            {
                var character = raw[i][j];
                if (!char.IsDigit(character))
                {
                    AddValidGear(gears, numbers, starLocation.Item1, starLocation.Item2, isValidGear);
                    numbers.Clear();
                    isValidGear = false;
                    starLocation = (0, 0);
                    continue;
                }

                numbers.Add(character);
                if (!isValidGear)
                    starLocation = GetStarLocation(raw, i, j);
                isValidGear = isValidGear || starLocation != (0, 0);

                if (IsLastColumn(raw, i, j))
                    AddValidGear(gears, numbers, starLocation.Item1, starLocation.Item2, isValidGear);
            }
        }
        gears = gears.Where(p => p.Value.Count > 1).ToDictionary();

        long sum = 0;
        foreach (var parts in gears)
        {
            Console.WriteLine($"Gear ratio found for star {parts.Key} for parts {parts.Value.First()} and {parts.Value.Last()}");
            var ratio = parts.Value.First() * parts.Value.Last();
            sum += ratio;
        }

        Console.WriteLine($"The sum of all valid gear ratios is: {sum}");
    }

    private static void AddValidPartNumber(List<long> partNumbers, int i, List<char> numbers, bool isValidPart)
    {
        if (isValidPart)
        {
            var partNumber = long.Parse(string.Join("", numbers));
            Console.WriteLine($"The following part number on line {i + 1} is valid: {partNumber}");
            partNumbers.Add(partNumber);
        }
    }

    private static void AddValidGear(Dictionary<(int row, int column), List<long>> gears, List<char> numbers, int row, int column, bool isValidGear)
    {
        if (isValidGear)
        {
            var partNumber = long.Parse(string.Join("", numbers));
            if (!gears.TryAdd((row, column), new List<long> { partNumber }))
                gears[(row, column)].Add(partNumber);
        }
    }

    private static bool IsValidPart(List<string> raw, int row, bool isValidPart, int column)
    {
        var isLeftSymbol = false;
        var isDiagTopLeftSymbol = false;
        var isTopSymbol = false;
        var isDiagTopRightSymbol = false;
        var isRightSymbol = false;
        var isDiagBottomRightSymbol = false;
        var isBottomSymbol = false;
        var isDiagBottomLeftSymbol = false;

        if (column != 0)
            isLeftSymbol = IsSymbol(raw[row][column - 1]);
        if (row != 0)
        {
            if (column != 0)
                isDiagTopLeftSymbol = IsSymbol(raw[row - 1][column - 1]);

            isTopSymbol = IsSymbol(raw[row - 1][column]);

            if (!IsLastColumn(raw, row, column))
                isDiagTopRightSymbol = IsSymbol(raw[row - 1][column + 1]);
        }
        if (!IsLastColumn(raw, row, column))
        {
            isRightSymbol = IsSymbol(raw[row][column + 1]);
            if (row != raw.Count - 1)
                isDiagBottomRightSymbol = IsSymbol(raw[row + 1][column + 1]);
        }
        if (!IsLastRow(raw, row))
        {
            isBottomSymbol = IsSymbol(raw[row + 1][column]);
            if (column != 0)
                isDiagBottomLeftSymbol = IsSymbol(raw[row + 1][column - 1]);
        }

        isValidPart = isValidPart || isLeftSymbol || isDiagTopLeftSymbol || isTopSymbol || isDiagTopRightSymbol ||
            isRightSymbol || isDiagBottomRightSymbol || isBottomSymbol || isDiagBottomLeftSymbol;
        return isValidPart;
    }

    private static (int row, int column) GetStarLocation(List<string> raw, int row, int column)
    {
        if (column != 0 && IsStarSymbol(raw[row][column - 1]))
            return (row, column - 1);

        if (row != 0)
        {
            if (column != 0 && IsStarSymbol(raw[row - 1][column - 1]))
                return (row - 1, column - 1);

            if (IsStarSymbol(raw[row - 1][column]))
                return (row - 1, column);

            if (!IsLastColumn(raw, row, column) && IsStarSymbol(raw[row - 1][column + 1]))
                return (row - 1, column + 1);
        }
        if (!IsLastColumn(raw, row, column))
        {
            if (IsStarSymbol(raw[row][column + 1]))
                return (row, column + 1);

            if (row != raw.Count - 1 && IsStarSymbol(raw[row + 1][column + 1]))
                return (row + 1, column + 1);
        }
        if (!IsLastRow(raw, row))
        {
            if (IsStarSymbol(raw[row + 1][column]))
                return (row + 1, column);

            if (column != 0 && IsStarSymbol(raw[row + 1][column - 1]))
                return (row + 1, column - 1);
        }

        return (0, 0);
    }

    private static bool IsSymbol(char character)
    {
        return _symbols.Contains(character);
    }

    private static bool IsStarSymbol(char character)
    {
        return character == '*';
    }

    private static bool IsLastColumn(List<string> raw, int i, int j)
    {
        return j == raw[i].Length - 1;
    }

    private static bool IsLastRow(List<string> raw, int i)
    {
        return i == raw.Count - 1;
    }

    private static List<string> ReadFile()
    {
        return FileUtils.ReadFile(Const.FILE_GEARRATIOS).ToList();
    }
}
