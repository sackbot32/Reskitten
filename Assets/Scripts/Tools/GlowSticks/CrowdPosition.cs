using DG.Tweening;
using UnityEngine;

public class CrowdPosition : MonoBehaviour
{
    private Transform posForCrowd;
    public float travelTime;
    private GameObject currentCrowd;

    private void Awake()
    {
        posForCrowd = transform.GetChild(0).transform;
    }

    public void SetCrowd(GameObject newCrowd)
    {
        if (currentCrowd == null)
        {
            currentCrowd = newCrowd;
            if(newCrowd.GetComponent<CrowdInstance>().currentPosition != null)
            {
                newCrowd.GetComponent<CrowdInstance>().currentPosition.RemoveCrowd(newCrowd);
            }
            CrowdInstanceManager.instance.AddToMovingCrowd(newCrowd);
            newCrowd.GetComponent<CrowdInstance>().currentPosition = gameObject.GetComponent<CrowdPosition>();
            newCrowd.gameObject.transform.DOMove(posForCrowd.position, travelTime).OnComplete(() =>
            {
                if (CrowdInstanceManager.instance != null)
                {
                    //CrowdInstanceManager.instance.surface.BuildNavMesh();
                    CrowdInstanceManager.instance.RemoveMovingCrowd(newCrowd);
                }
            });
            
        }
    }

    public void RemoveCrowd(GameObject oldCrowd)
    {
        if (oldCrowd == currentCrowd)
        {
            currentCrowd.GetComponent<CrowdInstance>().currentPosition = gameObject.GetComponent<CrowdPosition>();
            currentCrowd = null;
        }
    }

    public bool HasCrowd()
    {
        return currentCrowd != null;
    }
}
