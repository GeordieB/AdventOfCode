using System.Collections.Generic;

namespace AdventOfCode._2020.Day3
{
    public class Map
    {
        public Terrain[][] Terrains { get; set; }

        public Map(List<string> data)
        {
            Terrains = new Terrain[data.Count][];
            int i = 0;
            foreach (string map in data)
            {
                Terrain[] values = new Terrain[map.Length];
                int j = 0;
                foreach (char terrain in map)
                {
                    values[j] = terrain == '.' ? Terrain.Open : Terrain.Tree;
                    j++;
                }
                Terrains[i] = values;
                i++;
            }
        }
    }
}