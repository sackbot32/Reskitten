using UnityEngine;

public class CatMintThrower : MonoBehaviour, ITool
{

    public GameObject mintPrefab;
    public Transform throwPoint;
    public float testThrowForce;
    private GameObject currentMint;

    public void Main()
    {
        if(currentMint == null)
        {
            Rigidbody rb; 
            currentMint = Instantiate(mintPrefab,throwPoint.position,throwPoint.rotation);
            rb = currentMint.GetComponent<Rigidbody>();
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Vector3 dir = (ray.GetPoint(5) - throwPoint.position).normalized;
            rb.AddForce(dir * testThrowForce);

        }
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
