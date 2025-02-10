using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.Events;

public class Decoration : MonoBehaviour, ISwitchInput
{
    public List<GameObject> decorationParent = new List<GameObject>();
    public int conditionFullfilledPos;
    public int conditionListPos;
    public int assuredPos = 0;
    public int crowdToSpawn = 2;
    public UnityEvent conditionEvent;
    private bool crowdSpawned = false;
    public int GetLength()
    {
        return decorationParent.Count;
    }

    public bool SwitchInput(int input)
    {
        if (input < 0)
        {
            return false;
        }
        else if (input >= decorationParent.Count)
        {
            return false;
        }
        else
        {
            foreach (GameObject child in decorationParent)
            {
                child.SetActive(false);
            }
            decorationParent[input].SetActive(true);
            if(conditionFullfilledPos == input)
            {
                ConditionList.instance.SetConditionTrue(conditionListPos);
                if (!crowdSpawned)
                {
                    crowdSpawned=true;
                    CrowdInstanceManager.instance.SetCrowd(assuredPos,crowdToSpawn);
                }
                conditionEvent.Invoke();
            }
            else
            {
                ConditionList.instance.SetConditionFalse(conditionListPos);
            }
            
            return true;
        }
    }
}
