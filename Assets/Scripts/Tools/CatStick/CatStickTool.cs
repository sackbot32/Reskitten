using UnityEngine;

public class CatStickTool : MonoBehaviour, ITool
{
    public Transform point;
    public Sprite toolSprite;
    [SerializeField]
    private LineRenderer lRenderer;
    public Transform startPoint;
    public Transform endPoint;
    [SerializeField]
    private AudioSource equipSource;
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
        if (lRenderer != null) 
        { 
            lRenderer.SetPosition(0,startPoint.position);
            lRenderer.SetPosition(1,endPoint.position);
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
        if (equipSource != null)
        {
            SFXPlayer.StaticPlaySound(equipSource, equipSource.clip, true);
        }
    }

    public void OnUnequip()
    {
        if (equipSource != null)
        {
            SFXPlayer.StaticPlaySound(equipSource, equipSource.clip, true);
        }
    }

    public void StickCatToTool(CatController catController,Transform ballToStick)
    {
        catController.enabled = false;
        catController.agent.enabled = false;
        catController.transform.parent = ballToStick;
        catController.anim.Play("PoseT");
        catController.gameObject.GetComponent<Collider>().enabled = false;
        catController.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        foreach (Transform item in catController.transform)
        {
            item.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            if(item.childCount > 0)
            {
                foreach (Transform itemChild in item)
                {
                    itemChild.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                }
            }
        }
        catController.transform.localPosition = new Vector3(-0.0023f,4e-05f,-0.00571f);
        catController.transform.localRotation = Quaternion.Euler(180,-110,-90);
        ToolUser.instance.canChange = false;
    }
}
