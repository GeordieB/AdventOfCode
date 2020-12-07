namespace AdventOfCode._2020.Day2
{
    public class TobogganRuleSet
    {
        public int FirstIndex { get; set; }
        public int SecondIndex { get; set; }
        public char Letter { get; set; }
        public string Password { get; set; }
        public bool IsValid
        {
            get
            {
                return FirstIndex != 0 && SecondIndex != 0 &&
                    ((Password[FirstIndex - 1] == Letter && Password[SecondIndex - 1] != Letter) ||
                    (Password[FirstIndex - 1] != Letter && Password[SecondIndex - 1] == Letter));
            }
        }
    }
}