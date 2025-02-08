using UnityEngine;

public class CatStickTool : MonoBehaviour, ITool
{
    public Transform point;
    public Sprite toolSprite;

    public Sprite GetImage()
    {
        return toolSprite;
    }
    private void Update()
    {
        if(gameObject.activeSelf)
        {
            Passive();
        }
    }
    public void Main()
    {
         
    }

    public void Passive()
    {
        if (CatManager.instance != null)
        {
            if(CatManager.instance.currentCat != null)
            {
                CatManager.instance.currentCat.GoToPoint(point.position);
            }
        }
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

    public void OnEquip()
    {

    }

    public void OnUnequip()
    {

    }
}
