using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AIBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<TicTacToeBehaviour> _ticTacToeBehaviours;
    public float aiSpeed = 2;

    private TurnKeeper _turnKeeper;
    private void Start()
    {
        GameEvents.GridInitializedEvent.AddListener(OnSetupComplete);
        GameEvents.PlayerEndTurnEvent.AddListener(StartTurn);
        GameEvents.WinConditionEvent.AddListener(OnWinConditionMet);
        GameEvents.TurnKeeperInitializedEvent.AddListener(OnTurnKeeperInitialized);
    }
    private void OnTurnKeeperInitialized(TurnKeeper arg0)
    {
        _turnKeeper = arg0;
    }
    private void OnWinConditionMet(bool winner, string info)
    {
        if (winner)
            StopAllCoroutines();
    }

    public void OnSetupComplete(List<TicTacToeBehaviour> ticTacToeBehaviours)
    {
        _ticTacToeBehaviours = new List<TicTacToeBehaviour>(ticTacToeBehaviours);
    }

    public void StartTurn()
    {
        StartCoroutine(ChoiceRoutine(aiSpeed));
    }
    private IEnumerator ChoiceRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        var ttb = _ticTacToeBehaviours.Find(x => x.Visited == false);
        if (ttb != null)
            _turnKeeper.TakeTurn(ttb, TurnKeeper.Turn.AI);
    }
}
