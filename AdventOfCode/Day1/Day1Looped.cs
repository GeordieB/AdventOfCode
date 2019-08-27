using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day1Looped
    {
        /// <summary>
        /// Takes comma seperated list of frequency changes, such as +1, -2, +3, -1
        /// Calculates current frequency
        /// </summary>
        /// <param name="args"></param>
        public static void CalculateFrequency(string[] args)
        {
            bool foundDoubleFrequency = false;
            int frequency = 0;
            List<int> seenFrequencies = new List<int>();
            int loops = 0;
            while (!foundDoubleFrequency)
            {
                foreach (string change in args)
                {
                    frequency = Day1.CalculateFrequency(change, frequency);
                    if (seenFrequencies.Contains(frequency))
                    {
                        foundDoubleFrequency = true;
                        break;
                    }
                    seenFrequencies.Add(frequency);
                }
                loops++;
            }
            Console.WriteLine($"\nAfter {loops} loops, the first frequency reached twice is: {frequency}");
        }
    }
}