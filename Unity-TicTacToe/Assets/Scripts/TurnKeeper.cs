using System.Collections.Generic;
public class TurnKeeper
{
    public enum Turn
    {
        None = 0,
        Player = 1 ,
        AI = 2 ,
    }
    
    private readonly SetupGameBehaviour _sgb;
    private readonly List<TicTacToeBehaviour> _ttbs;
    public Turn Current;
    public TurnKeeper(List<TicTacToeBehaviour> ttbs, SetupGameBehaviour sgb)
    {
        _ttbs = ttbs;
        _sgb = sgb;
        Current = Turn.Player;
    }

    public bool CheckWinCondition()
    {
        var row1 = new List<TicTacToeBehaviour> { _ttbs[0], _ttbs[1], _ttbs[2] };
        var row2 = new List<TicTacToeBehaviour> { _ttbs[3], _ttbs[4], _ttbs[5] };
        var row3 = new List<TicTacToeBehaviour> { _ttbs[6], _ttbs[7], _ttbs[8] };
        var col1 = new List<TicTacToeBehaviour> { _ttbs[0], _ttbs[3], _ttbs[6] };
        var col2 = new List<TicTacToeBehaviour> { _ttbs[1], _ttbs[4], _ttbs[7] };
        var col3 = new List<TicTacToeBehaviour> { _ttbs[2], _ttbs[5], _ttbs[8] };
        var diag1 = new List<TicTacToeBehaviour> { _ttbs[0], _ttbs[4], _ttbs[8] };
        var diag2 = new List<TicTacToeBehaviour> { _ttbs[2], _ttbs[4], _ttbs[6] };

        var items = new List<List<TicTacToeBehaviour>>
            { row1, row2, row3, col1, col2, col3, diag1, diag2 };
        
        foreach (var item in items)
        {
            var result = IsWinner(item);
            if (result.Item1 != null)
            {
                GameEvents.WinConditionEvent.Invoke(true, result.Item2);
                return true;
            }
        }
        return false;
    }

    private (List<TicTacToeBehaviour>, string) IsWinner(List<TicTacToeBehaviour> triplet)
    {
        bool o_winner = triplet.TrueForAll(x => x.isO);
        if (o_winner) return (triplet, "o winner");

        bool x_winner = triplet.TrueForAll(x => x.isX);
        if (x_winner) return (triplet, "x winner");

        return (null, "no winner yet");
    }

    public void TakeTurn(TicTacToeBehaviour ttb, Turn turn)
    {
        if (turn != Current)
            return;
        ttb.Choose(Current);
    }

    public void ReleaseTurnTo(Turn turn)
    {
        if (CheckWinCondition())
        {
            Current = Turn.None;
            _sgb.StartCoroutine(_sgb.ResetGame(3));
            return;
        }

        Current = turn;

        if (Current == Turn.AI)
        {
            GameEvents.PlayerEndTurnEvent.Invoke();
        }

    }
}
