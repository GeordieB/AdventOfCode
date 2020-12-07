using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class LoopedFrequencyFinder
    {
        /// <summary>
        /// Takes comma seperated list of frequency changes, such as +1, -2, +3, -1
        /// Finds first frequency which appears more than once
        /// </summary>
        /// <param name="args"></param>
        public static void Find(string[] args)
        {
            bool foundDoubleFrequency = false;
            int frequency = 0;
            List<int> seenFrequencies = new List<int>();
            int loops = 0;
            while (!foundDoubleFrequency)
            {
                foreach (string change in args)
                {
                    frequency = FrequencyCalculator.Calculate(change, frequency);
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