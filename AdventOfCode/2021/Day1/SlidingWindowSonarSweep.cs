using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021.Day1
{
    public class SlidingWindowSonarSweep
    {
        public static void AnalyseSweep()
        {
            var rawSweep = ReadFile();
            var formattedSweep = FormatData(rawSweep);

            var measurements = ProcessData(formattedSweep);
            var changes = DetermineDepthChanges(measurements);

            var numIncreased = changes.Count(p => p == DepthChange.Increased);
            Console.WriteLine($"There are {numIncreased} measurements that are larger than the previous measurement.");
        }

        private static Dictionary<int, int> ProcessData(List<string> rawSweep)
        {
            var measurements = new Dictionary<int, int>();
            foreach (var sweep in rawSweep)
            {
                var data = sweep.Split(" ").Where(p => !string.IsNullOrEmpty(p)).ToArray();
                if (data == null)
                    continue;

                var reading = int.Parse(data[0]);
                ProcessData(reading, data, measurements);
            }

            return measurements;
        }

        private static void ProcessData(int reading, string[] data, Dictionary<int, int> measurements)
        {
            foreach (var group in data.Skip(1))
            {
                var groupNumber = int.Parse(group);
                if (measurements.ContainsKey(groupNumber))
                    measurements[groupNumber] = measurements[groupNumber] + reading;
                else
                    measurements.TryAdd(groupNumber, reading);
            }
        }

        private static List<DepthChange> DetermineDepthChanges(Dictionary<int, int> measurements)
        {
            var changes = new List<DepthChange>();
            if (measurements == null || measurements.Count <= 1)
                return changes;

            var previousDepth = measurements.FirstOrDefault().Value;
            foreach (var depth in measurements.Values.Skip(1))
            {
                var change = DepthChange.Unchanged;
                if (depth > previousDepth)
                    change = DepthChange.Increased;
                if (depth < previousDepth)
                    change = DepthChange.Decreased;

                Console.WriteLine($"depth has {change} from {previousDepth} to {depth}");
                previousDepth = depth;
                changes.Add(change);
            }

            return changes;
        }

        private static List<string> ReadFile()
        {
            return FileUtils.ReadFile(Const.FILE_SONAR_SWEEP).ToList();
        }

        private static List<string> FormatData(List<string> rawData)
        {
            var formattedData = new List<string>();
            for (int i = 0; i < rawData.Count; i++)
            {
                if (i == 0)
                    formattedData.Add($"{rawData[i]} 1");
                else if (i == 1)
                    formattedData.Add($"{rawData[i]} 1 2");
                else
                    formattedData.Add($"{rawData[i]} {i - 1} {i} {i +1}");
            }

            return formattedData;
        }
    }
}