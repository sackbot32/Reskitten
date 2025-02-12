using UnityEngine;

public class CatStickTool : MonoBehaviour, ITool
{
    public Transform point;
    public Sprite toolSprite;
    [SerializeField]
    private LineRenderer lRenderer;
    public Transform startPoint;
    public Transform endPoint;

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

    }

    public void OnUnequip()
    {

    }

    public void StickCatToTool(CatController catController,Transform ballToStick)
    {
        catController.enabled = false;
        catController.agent.enabled = false;
        catController.transform.parent = ballToStick;
        catController.anim.Play("PoseT");
        catController.gameObject.GetComponent<Collider>().enabled = false;
        catController.transform.localPosition = new Vector3(-0.0023f,4e-05f,-0.00571f);
        catController.transform.localRotation = Quaternion.Euler(180,-110,-90);
    }
}
