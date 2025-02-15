using UnityEngine;
using UnityEngine.UI;

public class LaserTool : MonoBehaviour, ITool
{
    public LineRenderer lineRenderer;
    public Transform shootPoint;
    public Sprite toolSprite;

    public Sprite GetImage()
    {
        return toolSprite;
    }
    private void Start()
    {

    }

    private void Update()
    {
        if (lineRenderer.enabled)
        {
            Passive();
        }
    }
    public void Main()
    {
        lineRenderer.enabled = true;
    }

    public void UpMain()
    {
        lineRenderer.enabled = false;
    }

    public void Passive()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        lineRenderer.SetPosition(0, shootPoint.position);
        if (Physics.Raycast(ray, out hit, 100))
        {
            lineRenderer.SetPosition(1, hit.point);
            if(CatManager.instance != null)
            {
                CatManager.instance.currentCat.GoToPoint(hit.point, true);
            }
        }
        else
        {
            lineRenderer.SetPosition(1, ray.GetPoint(100));
        }
    }

    public void Secondary()
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
