using UnityEngine;
using UnityEngine.Windows;

public class SwitchMultipleOutput : MonoBehaviour
{
    public GameObject[] switchObjects;
    private int currentIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject switchObject in switchObjects) 
        {
            if(switchObject.TryGetComponent<ISwitchInput>(out ISwitchInput input))
            {
                input.SwitchInput(currentIndex);
            }
            
        }
        
        
    }


    public void GoUp()
    {
        currentIndex++;
        foreach (GameObject switchObject in switchObjects)
        {
            if (switchObject.TryGetComponent<ISwitchInput>(out ISwitchInput input))
            {
                if (!input.SwitchInput(currentIndex))
                {
                    currentIndex = 0;
                    input.SwitchInput(currentIndex);
                }
            }

        }

    }

    public void GoDown()
    {
        currentIndex--;
        foreach (GameObject switchObject in switchObjects)
        {
            if (switchObject.TryGetComponent<ISwitchInput>(out ISwitchInput input))
            {
                if (!input.SwitchInput(currentIndex))
                {
                    currentIndex = input.GetLength() - 1;
                    input.SwitchInput(currentIndex);
                }
            }

        }
    }
}
