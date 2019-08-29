using AdventOfCode.Day2;
using AdventOfCode.Day3;
using AdventOfCode.Day4;

namespace AdventOfCode
{
    public class Program
    {
        static void Main(string[] args)
        {
            FrequencyCalculator.Calculate(args);
            LoopedFrequencyFinder.Find(args);
            CheckSum.Calculate(args);
            CommonCharacters.Find(args);
            OverlapCalculator.Calculate(args);
            ReposeRecord.FindAsleepGuard();
        }
    }
}