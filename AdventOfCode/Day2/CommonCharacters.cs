using System;
using System.Linq;

namespace AdventOfCode.Day2
{
    public class CommonCharacters
    {
        public static void Find(string[] boxIds)
        {
            string commonChars = "";
            int index = 0;
            foreach (string boxId in boxIds)
            {
                int count = index;
                while (++count < boxIds.Length && string.IsNullOrEmpty(commonChars))
                {
                    string original = boxId.Replace(",", "");
                    string temp = boxIds.ElementAt(count).Replace(",", "");

                    if (original == temp)
                    {
                        continue;
                    }
                    char[] comparedId = temp.ToCharArray();

                    commonChars = FindCommonChar(original, comparedId);
                }
                index++;
            }
            Console.WriteLine(!string.IsNullOrEmpty(commonChars) ? $"Common characters between correct IDs are: {commonChars}" : $"There are no common characters");
        }

        public static string FindCommonChar(string original, char[] toCompare)
        {
            int charIndex = 0;
            int errors = 0;

            string commonChars = "";
            foreach (char part in original)
            {
                if (part != toCompare[charIndex])
                {
                    errors++;
                    if (errors == 2)
                    {
                        break;
                    }
                }
                else
                {
                    commonChars += part;
                }
                charIndex++;
            }

            return errors == 2 ? "" : commonChars;
        }
    }
}