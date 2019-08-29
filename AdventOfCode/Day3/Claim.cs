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
                    LeftEdgeOffset = offset[0].AsInt();
                    TopEdgeOffset = offset[1].AsInt();
                }

                string[] dimension = rawClaim.ElementAt(2)?.Replace(",", "").Split('x');

                if (dimension.Any() && dimension.Count() == 2)
                {
                    Width = dimension[0].AsInt();
                    Height = dimension[1].AsInt();
                }
            }
        }
    }
}