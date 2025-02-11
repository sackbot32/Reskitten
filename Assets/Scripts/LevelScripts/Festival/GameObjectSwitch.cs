using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.Events;

public class GameObjectSwitch : MonoBehaviour, ISwitchInput
{
    public List<GameObject> gameObjectParent = new List<GameObject>();
    public int conditionFullfilledPos;
    public int conditionListPos;
    public UnityEvent conditionEvent;
    //private bool crowdSpawned = false;
    public int GetLength()
    {
        return gameObjectParent.Count;
    }

    public bool SwitchInput(int input)
    {
        if (input < 0)
        {
            return false;
        }
        else if (input >= gameObjectParent.Count)
        {
            return false;
        }
        else
        {
            foreach (GameObject child in gameObjectParent)
            {
                child.SetActive(false);
            }
            gameObjectParent[input].SetActive(true);
            if(conditionFullfilledPos == input)
            {
                ConditionList.instance.SetConditionTrue(conditionListPos);
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
