using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day3
{
    public class Claim
    {
        public string ID { get; set; }
        public int LeftEdgeOffset { get; set; }
        public int TopEdgeOffset { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Claim(List<string> rawClaim)
        {
            if (rawClaim?.Count() == 3)
            {
                ID = rawClaim.ElementAt(0)?.Replace("#", "");

                string[] offset = rawClaim.ElementAt(1)?.Replace(":", "").Split(',');

                if (offset.Any() && offset.Count() == 2)
                {
                    LeftEdgeOffset = TryParse(offset[0]);
                    TopEdgeOffset = TryParse(offset[1]);
                }

                string[] dimension = rawClaim.ElementAt(2)?.Replace(",", "").Split('x');

                if (dimension.Any() && dimension.Count() == 2)
                {
                    Width = TryParse(dimension[0]);
                    Height = TryParse(dimension[1]);
                }
            }
        }

        private int TryParse(string value)
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            return 0;
        }
    }
}