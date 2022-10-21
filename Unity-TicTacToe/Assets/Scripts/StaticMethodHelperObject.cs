using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Helpers/StaticMethodHelper")]
public class StaticMethodHelperObject : ScriptableObject
{
    public void Quit()
    {
        Application.Quit();
    }
}
