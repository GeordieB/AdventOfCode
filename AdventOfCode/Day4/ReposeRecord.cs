using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day4
{
    public class ReposeRecord
    {
        public static List<Guard> Build()
        {
            string[] rawRecords = FileUtils.ReadFile(Const.FILE_RECORDS);

            Dictionary<DateTime, string> orderedRawRecords = new Dictionary<DateTime, string>();
            List<Record> records = new List<Record>();

            foreach (string rawRecord in rawRecords)
            {
                DateTime timestamp = DateTime.Parse(rawRecord.Substring(1, 16));
                orderedRawRecords.Add(timestamp, rawRecord);
            }

            foreach (KeyValuePair<DateTime, string> rawRecord in orderedRawRecords.OrderBy(p => p.Key))
            {
                records.Add(new Record(rawRecord.Value));
            }

            List<Guard> guards = new List<Guard>();
            foreach (string guardId in records.Select(p => p.GuardId).Distinct())
            {
                guards.Add(new Guard(guardId, records.Where(p => p.GuardId == guardId).OrderBy(p => p.Timestamp).ToList()));
            }

            return guards;
        }

        public static Guard LongestSleeper(List<Guard> guards)
        {
            int maxSlept = 0;
            Guard longestSleeper = null;
            foreach (Guard guard in guards)
            {
                if (guard.MaxSlept >= maxSlept)
                {
                    maxSlept = guard.MaxSlept;
                    longestSleeper = guard;
                }
            }

            return longestSleeper;
        }

        public static int MostSleptMinute(Guard guard)
        {
            int mostSleptMinute = 0;

            int mostTimesSlept = 0;
            foreach (KeyValuePair<int, int> timeSleptPerDay in BuildSleepDictionary(guard).OrderBy(p => p.Key))
            {
                if (timeSleptPerDay.Value >= mostTimesSlept)
                {
                    mostTimesSlept = timeSleptPerDay.Value;
                    mostSleptMinute = timeSleptPerDay.Key;
                }
            }
            return mostSleptMinute;
        }

        public static Guard FrequentSleeper(List<Guard> guards, out int minuteSlept)
        {
            int maxTimesSlept = 0;
            minuteSlept = 0;
            Guard frequentSleeper = null;
            foreach (Guard guard in guards)
            {
                foreach (KeyValuePair<int, int> timesSlept in BuildSleepDictionary(guard))
                {
                    if (timesSlept.Value >= maxTimesSlept)
                    {
                        maxTimesSlept = timesSlept.Value;
                        minuteSlept = timesSlept.Key;
                        frequentSleeper = guard;
                    }
                }
            }

            return frequentSleeper;
        }

        public static Dictionary<int, int> BuildSleepDictionary(Guard guard)
        {
            List<int> daysWorked = guard.SleepCycles.Select(p => p.FallAsleep.Day).Distinct().ToList();
            Dictionary<int, int> slept = new Dictionary<int, int>();

            foreach (int dayWorked in daysWorked)
            {
                List<SleepCycle> cycles = guard.SleepCycles.Where(p => p.FallAsleep.Day == dayWorked).ToList();
                for (int minute = 0; minute < 60; minute++)
                {
                    if (cycles.Any(p => minute >= p.FallAsleep.Minute && minute < p.WakeUp.Minute))
                    {
                        int timesSlept = 1;
                        if (slept.ContainsKey(minute))
                        {
                            timesSlept += slept.FirstOrDefault(p => p.Key == minute).Value;
                            slept.Remove(minute);
                        }
                        slept.Add(minute, timesSlept);
                    }
                }
            }

            return slept;
        }
    }
}