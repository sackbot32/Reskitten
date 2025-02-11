using UnityEngine;

public interface ISwitchInput 
{
    public bool SwitchInput(int input);

    public int GetLength();

    public int CurrentPos();
}
