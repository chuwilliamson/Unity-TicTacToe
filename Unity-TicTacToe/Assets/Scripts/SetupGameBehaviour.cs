using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class SetupGameBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;


    [SerializeField]
    private List<TicTacToeBehaviour> TicTacToeBehaviours = new List<TicTacToeBehaviour>();
    public int offset = 5;

    private readonly List<GameObject> Grid = new List<GameObject>();
    // Start is called before the first frame update
    private void Start()
    {
        if (transform.childCount  <=0)
            SpawnGrid();
        
        TicTacToeBehaviour.TicTacToeClicked.AddListener(WinCondition);
        
    }
    
    [ContextMenu("Spawn Grid")]
    private void SpawnGrid()
    {
        int count = 0;
        for (int i = 0; i < 3; i++)
        {
            
            for (int j = 0; j < 3; j++)
            {
                var go = Instantiate(prefab, new Vector3(j * offset, i * offset), Quaternion.identity, transform);
                go.name = $"{i}, {j}, {count}";
                TicTacToeBehaviours.Add(go.AddComponent<TicTacToeBehaviour>());
                Grid.Add(go);
                count++;
            }
        }
    }

    public void WinCondition()
    {
        var row1 = new List<TicTacToeBehaviour>() { TicTacToeBehaviours[0], TicTacToeBehaviours[1], TicTacToeBehaviours[2] };
        var row2 = new List<TicTacToeBehaviour>() { TicTacToeBehaviours[3], TicTacToeBehaviours[4], TicTacToeBehaviours[5] };
        var row3 = new List<TicTacToeBehaviour>() { TicTacToeBehaviours[6], TicTacToeBehaviours[7], TicTacToeBehaviours[8] };

        var col1 = new List<TicTacToeBehaviour>() { TicTacToeBehaviours[0], TicTacToeBehaviours[3], TicTacToeBehaviours[6] };
        var col2 = new List<TicTacToeBehaviour>() { TicTacToeBehaviours[1], TicTacToeBehaviours[4], TicTacToeBehaviours[7] };
        var col3 = new List<TicTacToeBehaviour>() { TicTacToeBehaviours[2], TicTacToeBehaviours[5], TicTacToeBehaviours[8] };

        var items = new List<List<TicTacToeBehaviour>>() { row1, row2, row3, col1, col2, col3 };
        foreach (var item in items)
        {
            var result = IsWinner(item);
            if(result.Item1!= null)
                Debug.Log($"{result.Item2}with items {result.Item1}");
        }
    }

    public (List<TicTacToeBehaviour>,string) IsWinner(List<TicTacToeBehaviour> triplet)
    {
        var o_winner = triplet.TrueForAll(x => x.isO == true);
        if (o_winner) return (triplet, "o winner");

        var x_winner = triplet.TrueForAll(x => x.isX == true);
        if (x_winner) return (triplet, "x winner");

        return (null, "no winner yet");
    }
    
}
