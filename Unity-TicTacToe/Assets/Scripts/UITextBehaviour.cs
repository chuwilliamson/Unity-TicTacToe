using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITextBehaviour : MonoBehaviour
{
    private TMP_Text _tmpText;
    // Start is called before the first frame update
    void Awake()
    {
        _tmpText = GetComponent<TMP_Text>();
     GameEvents.WinConditionEvent.AddListener(OnWinConditionMet);   
    }
    private void OnWinConditionMet(bool arg0, string arg1)
    {
        _tmpText.text = arg1;
    }
}
