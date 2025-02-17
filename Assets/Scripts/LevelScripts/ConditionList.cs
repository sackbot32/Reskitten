using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class ConditionList : MonoBehaviour
{
    public static ConditionList instance;
    public List<bool> conditions = new List<bool>();
    private bool doOnce;

    public UnityEvent conditionFullfilledEvent;


    private void Awake()
    {
        instance = this;
    }


    public void SetConditionTrue(int conditionPos)
    {
        conditions[conditionPos] = true;
        if (CheckConditions() && !doOnce)
        {
            doOnce = true;
            conditionFullfilledEvent.Invoke();
        }
    }

    public void SetConditionFalse(int conditionPos)
    {
        conditions[conditionPos] = false;
    }

    public bool CheckConditions()
    {
        bool areConditionsFullfilled = true;
        foreach (bool condition in conditions) 
        {
            if (!condition)
            {
                areConditionsFullfilled = false;
            }
        }

        return areConditionsFullfilled;
    }
}
