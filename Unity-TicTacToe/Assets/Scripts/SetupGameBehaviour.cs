using System.Collections.Generic;
using UnityEngine;
public class SetupGameBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    public int offset = 5;

    private readonly List<GameObject> Grid = new List<GameObject>();
    // Start is called before the first frame update
    private void Start()
    {
        if (transform.childCount > 0)
            return;
        SpawnGrid();
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
                go.AddComponent<TicTacToeBehaviour>();
                Grid.Add(go);
                count++;
            }
        }
    }
    
    
}
