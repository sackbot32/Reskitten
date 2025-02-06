using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FishingRodTool : MonoBehaviour,ITool
{
    public GameObject hookPrefab;
    private GameObject currentHook;
    public Transform shootPoint;
    public float travelTime;
    public float shootSpeed;
    public float timeTillSelfDestruct = 5;
    public Sprite toolSprite;
    public LineRenderer lRenderer;

    private void Start()
    {
        lRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if(currentHook != null)
        {
            lRenderer.SetPosition(0, shootPoint.position);
            lRenderer.SetPosition(1, currentHook.transform.position);
        }

         
    }
    public Sprite GetImage()
    {
        return toolSprite;
    }

    public void Main()
    {
        if (currentHook == null) 
        {

            lRenderer.enabled = true;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Vector3 dir = (ray.GetPoint(10) - shootPoint.position).normalized;
            currentHook = Instantiate(hookPrefab,shootPoint.position,Quaternion.identity);
            Rigidbody rb = currentHook.GetComponent<Rigidbody>();
            currentHook.GetComponent<HookCollision>().rodTool = gameObject.GetComponent<FishingRodTool>();
            rb.AddForce(dir * shootSpeed);
            StartCoroutine(DestroyAfterTime(timeTillSelfDestruct,currentHook));
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

    public void TowardHook()
    {
        if (currentHook != null)
        {
            PlayerManager.instance.playerRb.useGravity = false;
            Rigidbody rb = currentHook.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            
            Vector3 toPlayerDir = (PlayerManager.instance.player.transform.position - currentHook.transform.position).normalized;
            PlayerManager.instance.player.transform.DOMove(currentHook.transform.position + toPlayerDir * 2, travelTime).OnComplete(() => {

                lRenderer.enabled = false;
                PlayerManager.instance.playerRb.linearVelocity = Vector3.zero;
                PlayerManager.instance.playerRb.useGravity = true;
                Destroy(currentHook);
                
                });
        }
    }

    IEnumerator DestroyAfterTime(float timeTillDestruction, GameObject objectToDestroy)
    {
        yield return new WaitForSeconds(timeTillDestruction);
        lRenderer.enabled = false;
        Destroy(objectToDestroy);
    }
}
