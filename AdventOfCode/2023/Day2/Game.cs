namespace AdventOfCode._2023.Day2;
public class Game
{
    public string Name { get; set; }
    public int Id { get; set; }
    public List<Round> Rounds { get; set; } = new();
    public Round MaxRound
    {
        get
        {
            return new Round()
            {
                RedCubes = Rounds.Select(p => p.RedCubes).Max(),
                GreenCubes = Rounds.Select(p => p.GreenCubes).Max(),
                BlueCubes = Rounds.Select(p => p.BlueCubes).Max()
            };
        }
    }
    public long Power { get { return MaxRound.RedCubes * MaxRound.GreenCubes * MaxRound.BlueCubes; } }
}

public class Round
{
    public int RedCubes { get; set; }
    public int GreenCubes { get; set; }
    public int BlueCubes { get; set; }
}
