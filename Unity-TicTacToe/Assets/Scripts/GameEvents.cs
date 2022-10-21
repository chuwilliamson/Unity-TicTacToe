using System.Collections.Generic;
using UnityEngine.Events;
public class GridInitialized : UnityEvent<List<TicTacToeBehaviour>> { }
public class PlayerEndTurn : UnityEvent { }

public class WinCondition : UnityEvent<bool, string> { }

public class TurnKeeperInitialized : UnityEvent<TurnKeeper>{}



public class GameEvents
{
    public static readonly GridInitialized GridInitializedEvent = new GridInitialized();
    
    public static readonly PlayerEndTurn PlayerEndTurnEvent = new PlayerEndTurn();
    
    public static readonly WinCondition WinConditionEvent = new WinCondition();
    
    public static readonly TurnKeeperInitialized TurnKeeperInitializedEvent = new TurnKeeperInitialized();
}
