using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SetupGameBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    
    [SerializeField]

    public int offset = 5;
    private readonly List<GameObject> Grid = new List<GameObject>();
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(ResetGame(0));
    }

    [ContextMenu("Destroy Grid")]
    private void DestroyGrid()
    {
        Grid.Clear();
        for(int i = transform.childCount -1 ; i >=0 ; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        } 
    }
    
    [ContextMenu("Spawn Grid")]
    private void SpawnGrid()
    {
        var ttbs = new List<TicTacToeBehaviour>();
        int count = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                var go = Instantiate(prefab, new Vector3(j * offset, i * offset), Quaternion.identity, transform);
                go.name = $"{i}, {j}, {count}";
                ttbs.Add(go.AddComponent<TicTacToeBehaviour>());
                Grid.Add(go);
                count++;
            }
        } 
        GameEvents.GridInitializedEvent.Invoke(ttbs);
        GameEvents.TurnKeeperInitializedEvent.Invoke(new TurnKeeper(ttbs, this));
    }


    internal IEnumerator ResetGame(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        DestroyGrid();
        SpawnGrid();
    }


}
