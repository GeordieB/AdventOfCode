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

    private static void AddValidPartNumber(List<long> partNumbers, int i, List<char> numbers, bool isValidPart)
    {
        if (isValidPart)
        {
            var partNumber = long.Parse(string.Join("", numbers));
            Console.WriteLine($"The following part number on line {i + 1} is valid: {partNumber}");
            partNumbers.Add(partNumber);
        }
    }

    private static bool IsValidPart(List<string> raw, int i, bool isValidPart, int j)
    {
        var isLeftSymbol = false;
        var isDiagTopLeftSymbol = false;
        var isTopSymbol = false;
        var isDiagTopRightSymbol = false;
        var isRightSymbol = false;
        var isDiagBottomRightSymbol = false;
        var isBottomSymbol = false;
        var isDiagBottomLeftSymbol = false;

        if (j != 0)
            isLeftSymbol = IsSymbol(raw[i][j - 1]);
        if (i != 0)
        {
            if (j != 0)
                isDiagTopLeftSymbol = IsSymbol(raw[i - 1][j - 1]);

            isTopSymbol = IsSymbol(raw[i - 1][j]);

            if (!IsLastColumn(raw, i, j))
                isDiagTopRightSymbol = IsSymbol(raw[i - 1][j + 1]);
        }
        if (!IsLastColumn(raw, i, j))
        {
            isRightSymbol = IsSymbol(raw[i][j + 1]);
            if (i != raw.Count - 1)
                isDiagBottomRightSymbol = IsSymbol(raw[i + 1][j + 1]);
        }
        if (!IsLastRow(raw, i))
        {
            isBottomSymbol = IsSymbol(raw[i + 1][j]);
            if (j != 0)
                isDiagBottomLeftSymbol = IsSymbol(raw[i + 1][j - 1]);
        }

        isValidPart = isValidPart || isLeftSymbol || isDiagTopLeftSymbol || isTopSymbol || isDiagTopRightSymbol ||
            isRightSymbol || isDiagBottomRightSymbol || isBottomSymbol || isDiagBottomLeftSymbol;
        return isValidPart;
    }

    private static bool IsSymbol(char character)
    {
        return _symbols.Contains(character);
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
