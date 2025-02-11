using UnityEngine;
using System.Collections.Generic;

public class SwitchMultipleOutput : MonoBehaviour
{
    public GameObject[] switchObjects;
    private List<ISwitchInput> inputList = new List<ISwitchInput>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject switchObject in switchObjects) 
        {
            if(switchObject.TryGetComponent(out ISwitchInput input))
            {
                inputList.Add(input);
            }
            
        }

        foreach (ISwitchInput input in inputList)
        {
            input.SwitchInput(0);
        }
        
        
    }


    public void GoUp()
    {
        foreach (ISwitchInput input in inputList)
        {
            
            if (!input.SwitchInput(input.CurrentPos() + 1))
            {
                input.SwitchInput(0);
            }
        }
        

    }

    public void GoDown()
    {
        foreach (ISwitchInput input in inputList)
        {
            
            if (!input.SwitchInput(input.CurrentPos() + 1))
            {
                input.SwitchInput(input.GetLength() - 1);
            }
        }
    }
}
