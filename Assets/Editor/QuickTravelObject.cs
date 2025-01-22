using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "QuickTravelObject", menuName = "Scriptable Objects/QuickTravelObject")]
public class QuickTravelObject : ScriptableObject
{
    public string sceneID;
    public List<QuickTravelPos> positions = new();
}

[Serializable]
public class QuickTravelPos
{
    public string posName;
    public Vector3 pos;

    public QuickTravelPos(string newName,Vector3 newPos) 
    { 
        posName = newName;
        pos = newPos;
    }
}
