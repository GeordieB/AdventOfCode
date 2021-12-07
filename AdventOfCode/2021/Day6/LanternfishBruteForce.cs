using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2021.Day6
{
    public partial class Lanternfish
    {
        public static void SimulateGrowthBruteForce()
        {
            var rawData = ReadFile();
            var fishes = ProcessData(rawData);

            Simulate(18, fishes);
            Console.WriteLine($"\n There are now {fishes.Count} fishes!");
        }

        private static void Simulate(int days, List<Fish> fishes)
        {
            for (int i = 0; i < days; i++)
            {
                var temp = new List<Fish>();
                var numAdded = 0;

                foreach (var fish in fishes)
                {
                    if (fish.InternalTimer == 0)
                    {
                        fish.ResetInternalTimer();
                        temp.Add(new Fish(8));
                        numAdded++;
                    }
                    else
                        fish.DecrementInternalTimer();
                }
                fishes.AddRange(temp);
                temp.Clear();

                //Console.WriteLine($"After {i + 1} days: {string.Join(',', fishes.Select(p => p.InternalTimer))}");
                //Console.WriteLine($"Number of fishes added: {numAdded}");
            }
        }

        private static List<Fish> ProcessData(List<string> rawData)
        {
            var fishes = new List<Fish>();
            foreach (var internalTimer in rawData.FirstOrDefault().Split(","))
            {
                var fish = new Fish(int.Parse(internalTimer));
                fishes.Add(fish);
            }

            return fishes;
        }

        private static List<string> ReadFile()
        {
            return FileUtils.ReadFile(Const.FILE_LANTERNFISH).ToList();
        }
    }

    public class Fish
    {
        public int InternalTimer { get; set; }

        public Fish(int internalTimer)
        {
            InternalTimer = internalTimer;
        }

        public void DecrementInternalTimer()
        {
            InternalTimer--;
        }

        public void ResetInternalTimer()
        {
            InternalTimer = 6;
        }
    }
}