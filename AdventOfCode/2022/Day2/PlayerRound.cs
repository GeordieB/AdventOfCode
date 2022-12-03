namespace AdventOfCode._2022.Day1;

public abstract class PlayerRound
{
    public string OpponentMove { get; set; }
    public string PlayerMove { get; set; }

    protected int GetShapeScore()
    {
        if (PlayerMove == "X")
            return 1;
        if (PlayerMove == "Y")
            return 2;
        if (PlayerMove == "Z")
            return 3;

        return 0;
    }

    protected int GetMoveScore(RoundResult result)
    {
        if (result == RoundResult.Loss)
            return 0;
        if (result == RoundResult.Draw)
            return 3;
        if (result == RoundResult.Win)
            return 6;

        return 0;
    }
}
