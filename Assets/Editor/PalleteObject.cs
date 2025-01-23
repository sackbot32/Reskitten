using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PalleteObject", menuName = "Scriptable Objects/PalleteObject")]
public class PalleteObject : ScriptableObject
{
    public List<GameObject> prefabs;
}
