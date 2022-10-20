using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class TicTacToeBehaviour : MonoBehaviour, IPointerClickHandler
{
    public bool isO;
    public bool isX;
    public bool Visited;
    public static UnityEvent TicTacToeClicked = new UnityEvent();
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Visited)
            return;
        isX = Visited = true;
        TicTacToeClicked.Invoke();
    }
}
