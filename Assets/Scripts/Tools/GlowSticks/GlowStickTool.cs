using UnityEngine;

public class GlowStickTool : MonoBehaviour, ITool
{
    public Transform shootPoint;
    public Sprite toolSprite;
    private CrowdInstance selectedInstance;
    private bool used;
    public Sprite GetImage()
    {
        return toolSprite;
    }
    private void Start()
    {

    }

    private void Update()
    {

        Passive();
        
    }
    public void Main()
    {
        if (!used) { 
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if(selectedInstance == null)
                {
                    if (hit.collider.gameObject.GetComponent<CrowdInstance>() != null) 
                    {
                        used = true;
                        selectedInstance = hit.collider.gameObject.GetComponent<CrowdInstance>();
                    }
                } else
                {
                    if (hit.collider.gameObject.GetComponent<CrowdPosition>() != null)
                    {
                        used = true;
                        hit.collider.gameObject.GetComponent<CrowdPosition>().SetCrowd(selectedInstance.gameObject);
                        selectedInstance = null;
                    }
                }
            }
        }
        
    }

    public void UpMain()
    {
        used = false;
    }

    public void Passive()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            //outline for items?
        }
    }

    public void Secondary()
    {
        if (selectedInstance != null) 
        {
            selectedInstance = null;
        }
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
