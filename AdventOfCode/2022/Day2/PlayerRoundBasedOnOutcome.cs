namespace AdventOfCode._2022.Day1;

public class PlayerRoundBasedOnOutcome : PlayerRound
{
    public string Outcome { get; set; }

    public PlayerRoundBasedOnOutcome(string opponentMove, string outcome)
    {
        OpponentMove = opponentMove;
        Outcome = outcome;
    }

    public int CalculateScore()
    {
        RoundResult result = RoundResult.Loss;

        //OpponentMove is Rock
        if (OpponentMove == "A")
        {
            if (Outcome == "X")
                PlayerMove = "Z";
            if (Outcome == "Y")
                PlayerMove = "X";
            if (Outcome == "Z")
                PlayerMove = "Y";
        }

        //OpponentMove is Paper
        if (OpponentMove == "B")
        {
            if (Outcome == "X")
                PlayerMove = "X";
            if (Outcome == "Y")
                PlayerMove = "Y";
            if (Outcome == "Z")
                PlayerMove = "Z";
        }

        //OpponentMove is Scissors
        if (OpponentMove == "C")
        {
            if (Outcome == "X")
                PlayerMove = "Y";
            if (Outcome == "Y")
                PlayerMove = "Z";
            if (Outcome == "Z")
                PlayerMove = "X";
        }

        return GetShapeScore() + GetMoveScore(GetRoundResult());
    }

    private RoundResult GetRoundResult()
    {
        if (Outcome == "X")
            return RoundResult.Loss;
        if (Outcome == "Y")
            return RoundResult.Draw;
        if (Outcome == "Z")
            return RoundResult.Win;

        return RoundResult.Loss;
    }
}
