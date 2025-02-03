using UnityEngine;

public class Laser : MonoBehaviour, ITool
{
    public LineRenderer lineRenderer;
    public Transform shootPoint;
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
}
