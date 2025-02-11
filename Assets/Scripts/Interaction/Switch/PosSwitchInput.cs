using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class PosSwitchInput : MonoBehaviour, ISwitchInput
{
    public List<Vector3> posList = new List<Vector3>();
    public float moveDuration;
    public bool relativeToOriginal;
    public bool rebuildNavMesh;
    private Vector3 ogPos;
    private void Start()
    {
        ogPos = transform.position;
    }

    private void OnValidate()
    {
        ogPos = transform.position;
    }
    public int GetLength()
    {
        return posList.Count;
    }

    public bool SwitchInput(int input)
    {
        if (input < 0)
        {
            return false;
        }
        else if (input >= posList.Count)
        {
            return false;
        } else
        {
            if (relativeToOriginal)
            {
                //transform.position = ogPos + posList[input];
                print("new pos = " + (ogPos + posList[input]));
                transform.DOMove(ogPos + posList[input],moveDuration).OnComplete(() =>
                {
                    if (rebuildNavMesh)
                    {
                        NavMeshManager.instance.navMesh.BuildNavMesh();
                    }
                });
            }
            else
            {
                transform.DOMove(posList[input], moveDuration).OnComplete(() =>
                {
                    if (rebuildNavMesh)
                    {
                        NavMeshManager.instance.navMesh.BuildNavMesh();
                    }
                });
            }
            return true;
        }
       
    }
}
