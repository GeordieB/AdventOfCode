namespace AdventOfCode._2020.Day2
{
    public class SledRuleSet
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public char Letter { get; set; }
        public string Password { get; set; }
        public bool IsValid
        {
            get
            {
                int count = 0;
                foreach (char letter in Password)
                {
                    if (letter == Letter)
                        count++;
                }
                return count >= Min && count <= Max;
            }
        }
    }
}