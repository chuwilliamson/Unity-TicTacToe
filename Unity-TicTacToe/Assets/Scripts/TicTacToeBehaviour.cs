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
    public static UnityEvent TicTacToeClicked = new UnityEvent();
    private static readonly int s_IsX = Animator.StringToHash("is_x");
    private static readonly int s_IsO = Animator.StringToHash("is_o");
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Visited)
            return;
        isX = Visited = true;
        _animator.SetTrigger(s_IsX);
        TicTacToeClicked.Invoke();
    }
}
