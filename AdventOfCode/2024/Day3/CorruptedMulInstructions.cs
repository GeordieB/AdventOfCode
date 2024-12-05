using System.Text.RegularExpressions;

namespace AdventOfCode._2024.Day3;

public static partial class CorruptedMulInstructions
{
    public static void FindMultInstructions()
    {
        var rawInput = FileUtils.ReadFile(Const.FILE_CORRUPTED_MUL_INSTRUCTIONS);

        var functionRegex = MulFunctionRegex();
        var sum = 0;
        foreach (var line in rawInput)
        {
            var matchCollection = functionRegex.Matches(line);
            var parameterRegex = MulParametersRegex();
            foreach (var match in matchCollection.Where(p => p != null))
            {
                var parameters = parameterRegex.Matches(match.ToString());
                sum += parameters.First().ToString().AsInt() * parameters.Last().ToString().AsInt();
            }
        }

        Console.WriteLine($"Sum of all multiplications: {sum}");
    }

    public static void FindConditionalMultInstructions()
    {
        var rawInput = FileUtils.ReadFile(Const.FILE_CORRUPTED_MUL_INSTRUCTIONS);

        var functionRegex = MulFunctionRegex();
        var sum = 0;
        var enabled = true;
        var lastKnownEnabled = true;
        foreach (var line in rawInput)
        {
            var conditionalInstructions = line.Split("do");
            foreach (var conditionalInstruction in conditionalInstructions)
            {
                if (!lastKnownEnabled || conditionalInstruction.StartsWith("n't"))
                {
                    lastKnownEnabled = true;
                    enabled = false;
                    continue;
                }

                enabled = true;
                var matchCollection = functionRegex.Matches(conditionalInstruction);
                var parameterRegex = MulParametersRegex();
                foreach (var match in matchCollection.Where(p => p != null))
                {
                    var parameters = parameterRegex.Matches(match.ToString());
                    sum += parameters.First().ToString().AsInt() * parameters.Last().ToString().AsInt();
                }
            }

            lastKnownEnabled = enabled;
        }

        Console.WriteLine($"Sum of all multiplications: {sum}");
    }

    [GeneratedRegex(@"mul\(\d{1,3},\d{1,3}\)")]
    private static partial Regex MulFunctionRegex();

    [GeneratedRegex(@"\d{1,3}")]
    private static partial Regex MulParametersRegex();
}