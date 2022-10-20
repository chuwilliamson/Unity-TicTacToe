using UnityEngine;
using UnityEngine.EventSystems;
public class TicTacToeBehaviour : MonoBehaviour, IPointerClickHandler
{
    public bool isO;
    public bool isX;
    public bool Visited;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Visited)
            return;
        isX = Visited = true;
    }
}
