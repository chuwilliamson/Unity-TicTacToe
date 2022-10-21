using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SetupGameBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    private Animator _animator;
    [SerializeField] public int offset = 5;
    private readonly List<GameObject> Grid = new List<GameObject>();
    private static readonly int s_Start = Animator.StringToHash("start");
    private static readonly int s_End = Animator.StringToHash("end");
    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger(s_Start);
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
    private void Update()
    {
        runtime += Time.deltaTime;
    }
    [ContextMenu("Spawn Grid")]
    private void SpawnGrid()
    {
        _ttbs = new List<TicTacToeBehaviour>();
        int count = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                var go = Instantiate(prefab, new Vector3(j * offset, i * offset), Quaternion.identity, transform);
                go.name = $"{i}, {j}, {count}";
                _ttbs.Add(go.AddComponent<TicTacToeBehaviour>());
                Grid.Add(go);
                count++;
            }
        } 
        GameEvents.GridInitializedEvent.Invoke(_ttbs);
        GameEvents.TurnKeeperInitializedEvent.Invoke(new TurnKeeper(_ttbs, this));
    }
    public float runtime;
    private List<TicTacToeBehaviour> _ttbs;

    internal IEnumerator ResetGame(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        DestroyGrid();
        SpawnGrid();
    }

    public void StartGameFinished()
    {
        StartCoroutine(ResetGame(0));
    }
    public void EndGameFinished()
    {
        _ttbs.ForEach(t=> t.ResetCell());
        _animator.SetTrigger(s_Start);
    }
    
    public IEnumerator WaitAndAnimate(float delay, int trigger)
    {
        yield return new WaitForSeconds(delay);
        _animator.SetTrigger(trigger);
    }
    
    
    public void StartEndGame()
    {
        
        StartCoroutine(WaitAndAnimate(3, s_End));
    }
}
