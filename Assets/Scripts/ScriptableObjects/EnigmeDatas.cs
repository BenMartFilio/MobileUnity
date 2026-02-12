using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "EnigmeDatas", menuName = "Scriptable Objects/EnigmeDatas")]
public class EnigmeDatas : ScriptableObject
{
    public bool[] lampsActivated;

    public bool IsAllActivated()
    {
        return !lampsActivated.Contains(false);
    }
}
