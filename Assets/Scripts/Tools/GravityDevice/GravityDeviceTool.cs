using UnityEngine;

public class GravityDeviceTool : MonoBehaviour, ITool
{
    public Sprite toolImage;
    public Sprite GetImage()
    {
         return toolImage;
    }

    public void Main()
    {
         
    }

    public void OnEquip()
    {
        PlayerManager.instance.stopDiferentGravity = true;
    }

    public void OnUnequip()
    {
        PlayerManager.instance.stopDiferentGravity = false;
    }

    public void Passive()
    {
         
    }

    public void Secondary()
    {
         
    }

    public void UpMain()
    {
         
    }

    public void UpSecondary()
    {
         
    }
}
