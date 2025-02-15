using UnityEngine;
using System.Collections.Generic;

public class ToolHud : MonoBehaviour
{

    public List<ToolImageInstance> toolImageInstances = new List<ToolImageInstance>();

    private void Awake()
    {
        foreach (Transform toolInstancesChild in transform)
        {
            toolImageInstances.Add(toolInstancesChild.gameObject.GetComponent<ToolImageInstance>());
        }
    }

}
