using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day2
{
    public class CheckSum
    {
        public static void Calculate(string[] boxIds)
        {
            int appearsTwice = 0;
            int appearsThrice = 0;

            foreach (string boxId in boxIds)
            {
                Dictionary<char, int> boxSums = new Dictionary<char, int>();
                foreach (char part in boxId)
                {
                    int value = 1;
                    if (boxSums.ContainsKey(part))
                    {
                        value += boxSums.FirstOrDefault(p => p.Key == part).Value;
                        boxSums.Remove(part);
                    }

                    boxSums.Add(part, value);
                }
                if(boxSums.Any(p => p.Value == 2))
                {
                    appearsTwice++;
                }
                if (boxSums.Any(p => p.Value == 3))
                {
                    appearsThrice++;
                }
            }

            int checksum = appearsTwice * appearsThrice;
            Console.WriteLine($"Checksum is: {checksum}");
        }
    }
}