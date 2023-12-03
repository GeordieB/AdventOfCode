using System.Text.RegularExpressions;

namespace AdventOfCode._2023.Day1;
public class Trebuchet
{
    private static readonly Dictionary<string, string> _numbers = new Dictionary<string, string>() {
        {"1", "1"},
        {"2", "2"},
        {"3", "3"},
        {"4", "4"},
        {"5", "5"},
        {"6", "6"},
        {"7", "7"},
        {"8", "8"},
        {"9", "9"},
        {"one", "1"},
        {"two", "2"},
        {"three", "3"},
        {"four", "4"},
        {"five", "5"},
        {"six", "6"},
        {"seven", "7"},
        {"eight", "8"},
        {"nine", "9"}
    };
    private static Dictionary<string, int> _indexedCharacters = [];

    public static void Calibrate()
    {
        var calibrationValues = ReadFile();
        var convertedCalibrations = new List<long>();

        foreach (var calibrationValue in calibrationValues)
        {
            var extractedCharacters = calibrationValue.Where(char.IsDigit).ToList();
            var convertedCalibration = long.Parse($"{extractedCharacters.First()}{extractedCharacters.Last()}");

            convertedCalibrations.Add(convertedCalibration);
            Console.WriteLine($"Converted calibration: {convertedCalibration}");
        }

        Console.WriteLine($"The sum of all calibrations is: {convertedCalibrations.Sum()}");
    }

    public static void CalibrateWithSpelledOutLetters()
    {
        var calibrationValues = ReadFile();
        var convertedCalibrations = new List<int>();

        foreach (var calibrationValue in calibrationValues.Select(p => p.ToLower()))
        {
            var extractedCharacters = new List<string>();
            foreach (var number in _numbers.Keys)
            {
                var regex = new Regex(number);
                var count = regex.Count(calibrationValue);
                for (var i = 0; i < count; i++)
                    extractedCharacters.Add(number);
            }

            _indexedCharacters.Clear();
            extractedCharacters = [.. extractedCharacters.OrderBy(p => OrderCharacters(p, calibrationValue))];
            var firstDigit = GetDigit(extractedCharacters.First().ToString());
            var secondDigit = GetDigit(extractedCharacters.Last().ToString());
            var convertedCalibration = int.Parse($"{firstDigit}{secondDigit}");

            convertedCalibrations.Add(convertedCalibration);
            Console.WriteLine($"Converted calibration: {convertedCalibration}");
        }

        Console.WriteLine($"The sum of all calibrations is: {convertedCalibrations.Sum()}");
    }

    private static string GetDigit(string extractedCharacter)
    {
        return _numbers.TryGetValue(extractedCharacter, out string value) ? value : extractedCharacter;
    }

    private static int OrderCharacters(string value, string originalString)
    {
        var regex = new Regex(value);
        var count = regex.Count(originalString);

        var index = originalString.IndexOf(value);
        if (count == 1)
            return index;

        if (_indexedCharacters.ContainsKey(value))
        {
            index = originalString.IndexOf(value, _indexedCharacters[value] + 1);
            _indexedCharacters[value] = index;
            return index;
        }

        _indexedCharacters.Add(value, index);
        return index;
    }

    private static List<string> ReadFile()
    {
        return FileUtils.ReadFile(Const.FILE_TREBUCHET).ToList();
    }
}
