using System;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day4
{
    public class Record
    {
        private const string BEGINS_SHIFT = "begins shift";
        private const string FALLS_ASLEEP = "falls asleep";

        private static string guardId;

        public DateTime Timestamp { get; set; }
        public string GuardId { get; set; }
        public bool ShiftStart { get; set; }
        public bool IsAsleep { get; set; }

        public Record(string record)
        {
            Timestamp = DateTime.Parse(record.Substring(1, 16));

            if (record.Contains(BEGINS_SHIFT))
            {
                Regex id = new Regex(@"\#\d*");
                Match m = id.Match(record);
                guardId = m?.Value.Replace("#", "");
                ShiftStart = true;
            }
            GuardId = guardId;

            IsAsleep = record.Contains(FALLS_ASLEEP);
        }
    }
}