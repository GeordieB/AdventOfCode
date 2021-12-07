using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2021.Day6
{
    public partial class LanternFish
    {
        public static void SimulateSmart()
        {
            var rawData = ReadFile();
            var fishes = ProcessData(rawData);

            var simulated = Simulate(256, fishes);
            Console.WriteLine($"\n There are now {simulated.Values.Sum()} fishes!");
        }

        private static Dictionary<int, long> Simulate(int days, Dictionary<int, long> fishes)
        {
            for (int i = 0; i < days; i++)
            {
                var temp = new Dictionary<int, long>();
                foreach (var fish in fishes)
                {
                    if(fish.Key == 0)
                    {
                        Add(6, fish.Value, temp);
                        Add(8, fish.Value, temp);
                    }
                    else
                        Add(fish.Key - 1, fish.Value, temp);
                }
                fishes = Copy(temp, fishes);
            }

            return fishes;
        }

        private static Dictionary<int, long> ProcessData(List<string> rawData)
        {
            var fishes = new Dictionary<int, long>();
            foreach (var fish in rawData.FirstOrDefault().Split(","))
            {
                var fishNum = int.Parse(fish);
                if (fishes.ContainsKey(fishNum))
                    fishes[fishNum] = fishes[fishNum] + 1;
                else
                    fishes[fishNum] = 1;
            }

            return fishes;
        }

        private static List<string> ReadFile()
        {
            return FileUtils.ReadFile(Const.FILE_LANTERNFISH).ToList();
        }

        private static Dictionary<int, long> Copy(Dictionary<int, long> toCopy, Dictionary<int, long> old)
        {
            old = new Dictionary<int, long>();
            foreach (var item in toCopy)
            {
                if (old.ContainsKey(item.Key))
                    old[item.Key] = + item.Value;
                else
                    old[item.Key] = item.Value;
            }

            return old;
        }

        private static void Add(int index, long value, Dictionary<int, long> fishes)
        {
            if (fishes.ContainsKey(index))
                fishes[index] = fishes[index] + value;
            else
                fishes[index] = value;
        }
    }
}