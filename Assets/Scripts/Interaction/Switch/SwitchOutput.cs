using UnityEngine;

public class SwitchOutput : MonoBehaviour
{
    public GameObject switchObject;
    private ISwitchInput input;
    private int currentIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switchObject.TryGetComponent<ISwitchInput>(out input);
        input.SwitchInput(currentIndex);
    }


    public void GoUp()
    {
        currentIndex++;
        if (!input.SwitchInput(currentIndex))
        {
            currentIndex = 0;
            input.SwitchInput(currentIndex);
        }
        
    }

    public void GoDown()
    {
        currentIndex--;
        if (!input.SwitchInput(currentIndex))
        {
            currentIndex = input.GetLength() -1;
            input.SwitchInput(currentIndex);
        }
    }
}
