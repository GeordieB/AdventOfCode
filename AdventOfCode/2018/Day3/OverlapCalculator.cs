using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day3
{
    public class OverlapCalculator
    {
        /// <summary>
        /// Given a list of raw claims (#ID @ X,X: XxX) where X is a number,
        /// Find how many square inches are within two or more claims.
        /// Ex: #1 @ 1,3: 4x4, #2 @ 3,1: 4x4
        /// </summary>
        /// <param name="rawClaims"></param>
        public static void Calculate(string[] rawClaims)
        {
            List<Claim> claims = new List<Claim>();

            for (int i = 0; i < rawClaims.Length; i++)
            {
                List<string> rawClaim = new List<string>()
                {
                    rawClaims.ElementAt(i), rawClaims.ElementAt(i + 2),
                    rawClaims.ElementAt(i + 3)
                };

                claims.Add(new Claim(rawClaim));
                i += 3;
            }

            Dictionary<(int, int), List<string>> grid = CreateGrid(claims);

            List<KeyValuePair<(int, int), List<string>>> overlap = grid.Where(p => p.Value.Count() > 1).ToList();
            string intactClaimId = FindIntactClaim(overlap, claims);

            Console.WriteLine($"There are {overlap.Count()} square inches where claims overlap");
            Console.WriteLine($"The ID of the claim that doesn't overlap is: {intactClaimId}");
        }

        private static Dictionary<(int, int), List<string>> CreateGrid(List<Claim> claims)
        {
            int maxRows = claims.Max(p => p.TopEdgeOffset) + claims.Max(p => p.Height);
            int maxColumns = claims.Max(p => p.LeftEdgeOffset) + claims.Max(p => p.Width);

            Dictionary<(int, int), List<string>> grid = new Dictionary<(int, int), List<string>>();
            for (int row = claims.Min(p => p.TopEdgeOffset); row < maxRows; row++)
            {
                for (int column = claims.Min(p => p.LeftEdgeOffset); column < maxColumns; column++)
                {
                    List<string> claimIds = new List<string>();
                    foreach (Claim claim in claims)
                    {
                        if (row >= claim.TopEdgeOffset && row < claim.Height + claim.TopEdgeOffset
                            && column >= claim.LeftEdgeOffset && column < claim.Width + claim.LeftEdgeOffset)
                        {
                            claimIds.Add(claim.ID);
                        }
                    }
                    grid.Add((row, column), claimIds);
                }
            }

            return grid;
        }

        public static string FindIntactClaim(List<KeyValuePair<(int, int), List<string>>> overlap, List<Claim> claims)
        {
            List<string> overlappingIds = overlap.SelectMany(p => p.Value).ToList();
            string intactClaimId = "";
            foreach (Claim claim in claims)
            {
                if (!overlappingIds.Any(p => p == claim.ID))
                {
                    intactClaimId = claim.ID;
                    break;
                }
            }

            return intactClaimId;
        }
    }
}