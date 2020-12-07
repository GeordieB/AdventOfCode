using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day4
{
    public class Guard
    {
        public string Id { get; set; }
        public List<Record> Records { get; set; }
        public List<Record> OnDuty { get { return Records.Where(p => !p.ShiftStart).ToList(); } }
        public List<SleepCycle> SleepCycles { get; set; }
        public int MaxSlept { get { return SleepCycles.Sum(p => p.TimeAsleep); } }

        public Guard(string id, List<Record> records)
        {
            Id = id;
            Records = records;

            Init();
        }

        private void Init()
        {
            SleepCycles = new List<SleepCycle>();
            for (int i = 0; i < OnDuty.Count; i++)
            {
                DateTime fallsAsleep = OnDuty.ElementAt(i).Timestamp;
                DateTime wakesUp = OnDuty.ElementAt(++i).Timestamp;

                SleepCycles.Add(new SleepCycle(fallsAsleep, wakesUp));
            }
        }
    }
}