using System;
using System.Collections.Generic;
using UnityEngine;


//This class is meant to be called by events to spawn a certain ammount of crowd members,
//it needs another class to list to call the exact type of spanwing

[Serializable]
public class CrowdCall
{
    public bool hasAssurePos;
    public int assuredPos;
    public int amount;
    private bool done = false;

  
    public void CallCrowd()
    {
        if (!done)
        {
            done = true;
            if (!hasAssurePos)
            {
                CrowdInstanceManager.instance.SetCrowd(amount);
            } else
            {
                CrowdInstanceManager.instance.SetCrowd(assuredPos,amount);
            }
        }
    }
}

public class CrowdCallList : MonoBehaviour
{
    public List<CrowdCall> crowdCallList = new List<CrowdCall>();
    
    public void Call(int index)
    {
        crowdCallList[index].CallCrowd();
    }
}
