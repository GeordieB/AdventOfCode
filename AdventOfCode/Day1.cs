using System;

namespace AdventOfCode
{
    class Day1
    {
        public const string PLUS = "+";

        /// <summary>
        /// Takes comma seperated list of frequency changes, such as +1, -2, +3, -1
        /// Calculates current frequency
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int frequency = 0;
            foreach (string change in args)
            {
                CalculateFrequency(change, frequency);
            }
        }

        /// <summary>
        /// Takes a frequency change and adds it to the current frequency
        /// Change must be a sign (+ or -) followed by a number
        /// </summary>
        /// <param name="change"></param>
        /// <param name="frequency"></param>
        public static void CalculateFrequency(string change, int frequency)
        {
            string sign = change.Substring(0, 1);
            string temp = change.Substring(1).Replace(",", "");
            if (int.TryParse(temp, out int result))
            {
                int oldFrequency = frequency;
                frequency += sign == PLUS ? result : -result;
                Console.WriteLine($"Current frequency {oldFrequency}, change of {sign}{temp}; resulting frequency {frequency},");
            }
        }
    }
}