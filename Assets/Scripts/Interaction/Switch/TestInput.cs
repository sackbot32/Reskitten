using UnityEngine;
using System.Collections.Generic;
public class TestInput : MonoBehaviour, ISwitchInput
{
    public List<string> testString = new List<string>();
    private int currentIndex;

    public int CurrentPos()
    {
        return currentIndex;
    }
    public int GetLength()
    {
        return testString.Count;
    }

    public bool SwitchInput(int input)
    {
        if(input < 0)
        {
            return false;
        } else if(input >= testString.Count)
        {
            return false;
        } else
        {
            currentIndex = input;
            print(testString[input]);
            return true;
        }


        
    }
}
