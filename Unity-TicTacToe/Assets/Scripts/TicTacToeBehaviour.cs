using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class TicTacToeBehaviour : MonoBehaviour, IPointerClickHandler
{
    private Animator _animator;
    public bool isO;
    public bool isX;
    public bool Visited;
    public bool WinCondition;
    private TurnKeeper _turnKeeper;
    
    private static readonly int s_IsX = Animator.StringToHash("is_x");
    private static readonly int s_IsO = Animator.StringToHash("is_o");
    private TicTacToeBehaviour _ticTacToeBehaviour;
    
    private void Awake()
    {
        _ticTacToeBehaviour = GetComponent<TicTacToeBehaviour>();
        _animator = GetComponent<Animator>();
        GameEvents.TurnKeeperInitializedEvent.AddListener(OnTurnKeeperInitialized);
    }
    private void OnTurnKeeperInitialized(TurnKeeper arg0)
    {
        _turnKeeper = arg0;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        _turnKeeper.TakeTurn(this, TurnKeeper.Turn.Player);
    }
    
    public void Choose(TurnKeeper.Turn turn)
    {
        if (Visited)
            return;
        switch (turn)
        {
            case TurnKeeper.Turn.Player:
                isX = Visited = true;
                _animator.SetTrigger(s_IsX);
                _turnKeeper.ReleaseTurnTo(TurnKeeper.Turn.AI);
                break;
            case TurnKeeper.Turn.AI:
                isO = Visited = true;
                _animator.SetTrigger(s_IsO);
                _turnKeeper.ReleaseTurnTo(TurnKeeper.Turn.Player);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(turn), turn, null);
        }
    }
}
