using System;

namespace AdventOfCode.Day4
{
    public class SleepCycle
    {
        public DateTime FallAsleep { get; set; }
        public DateTime WakeUp { get; set; }
        public int TimeAsleep { get { return WakeUp.Minute - FallAsleep.Minute; } }

        public SleepCycle(DateTime asleep, DateTime awake)
        {
            FallAsleep = asleep;
            WakeUp = awake;
        }
    }
}