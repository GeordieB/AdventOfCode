using System;
using System.Collections.Generic;

namespace AdventOfCode.Day4
{
    public class Strategy1
    {
        public static void FindAsleepGuard()
        {
            List<Guard> guards = ReposeRecord.Build();

            Guard maxSleptGuard = ReposeRecord.LongestSleeper(guards);
            int maxSleptMinute = ReposeRecord.MostSleptMinute(maxSleptGuard);

            int guardMinute = maxSleptGuard.Id.AsInt() * maxSleptMinute;
            Console.WriteLine($"Guard #{maxSleptGuard.Id} slept the most on minute {maxSleptMinute}. Key is: {guardMinute}");
        }
    }
}