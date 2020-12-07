using System;
using System.Collections.Generic;

namespace AdventOfCode.Day4
{
    public class Strategy2
    {
        public static void FindAsleepGuard()
        {
            List<Guard> guards = ReposeRecord.Build();
            Guard frequentSleeperGuard = ReposeRecord.FrequentSleeper(guards, out int maxSleptMinute);

            int guardMinute = frequentSleeperGuard.Id.AsInt() * maxSleptMinute;
            Console.WriteLine($"Guard #{frequentSleeperGuard.Id} slept the most frequently on minute {maxSleptMinute}. Key is: {guardMinute}");
        }
    }
}