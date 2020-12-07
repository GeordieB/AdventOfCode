namespace AdventOfCode._2019.Day3
{
    public class WirePointMarker
    {
        public int WireIndex { get; set; }
        public int Steps { get; set; }

        public WirePointMarker()
        {

        }

        public WirePointMarker(int wireIndex, int steps)
        {
            WireIndex = wireIndex;
            Steps = steps;
        }

        public override bool Equals(object other)
        {
            if (other is WirePointMarker)
            {
                WirePointMarker toCompare = other as WirePointMarker;
                return WireIndex == toCompare.WireIndex && Steps == toCompare.Steps;
            }

            return false;
        }
    }
}