using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2021.Day1
{
    public class SonarSweep
    {
        public static void AnalyseSweep()
        {
            List<int> floorDepths = ReadFile();
            var changes = DetermineDepthChanges(floorDepths);

            var numIncreased = changes.Count(p => p == DepthChange.Increased);
            Console.WriteLine($"There are {numIncreased} measurements that are larger than the previous measurement.");
        }

        private static List<DepthChange> DetermineDepthChanges(List<int> depths)
        {
            var changes = new List<DepthChange>();
            if (depths == null || depths.Count <= 1)
                return changes;

            var previousDepth = depths.FirstOrDefault();
            foreach (var depth in depths.Skip(1))
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

        private static List<int> ReadFile()
        {
            var rawSweep = FileUtils.ReadFile(Const.FILE_SONAR_SWEEP).ToList();
            return rawSweep.Select(p => int.Parse(p)).ToList();
        }
    }
}