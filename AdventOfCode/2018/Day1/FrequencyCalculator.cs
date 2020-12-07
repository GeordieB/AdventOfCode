using System;

namespace AdventOfCode
{
    public class FrequencyCalculator
    {
        /// <summary>
        /// Takes comma seperated list of frequency changes, such as +1, -2, +3, -1
        /// Calculates current frequency
        /// </summary>
        /// <param name="args"></param>
        public static void Calculate(string[] args)
        {
            int frequency = 0;
            foreach (string change in args)
            {
                frequency = Calculate(change, frequency);
            }
        }

        /// <summary>
        /// Takes a frequency change and adds it to the current frequency
        /// Change must be a sign (+ or -) followed by a number
        /// </summary>
        /// <param name="change"></param>
        /// <param name="frequency"></param>
        public static int Calculate(string change, int frequency)
        {
            string sign = change.Substring(0, 1);
            int result = change.Substring(1).Replace(",", "").AsInt();

            int oldFrequency = frequency;
            frequency += sign == Const.PLUS ? result : -result;
            Console.WriteLine($"Current frequency {oldFrequency}, change of {sign}{result}; resulting frequency {frequency},");

            return frequency;
        }
    }
}