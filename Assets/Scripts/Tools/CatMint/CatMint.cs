using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMint : MonoBehaviour
{
    public float floorDetect = 5;
    private Rigidbody rb;
    private bool hasHitGround;
    private Collider mintCollider;
    [SerializeField]
    private Transform feet;
    public float destructionTime = 15f;
    [SerializeField]
    private AudioSource touchSource;
    private void Start()
    {
        StartCoroutine(DestroyAfterTime());
        mintCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CatController>() != null)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Debug.DrawRay(feet.position, -transform.up * floorDetect, Color.red);
        if (Physics.Raycast(feet.position,-Vector3.up,out RaycastHit hit,floorDetect) && !hasHitGround)
        {
            if(touchSource != null)
            {
                SFXPlayer.StaticPlaySound(touchSource, touchSource.clip, true);
            }
            hasHitGround = true;
            rb.isKinematic = true;
            transform.position = hit.point - feet.localPosition;
            mintCollider.isTrigger = true;
        }

        if(hasHitGround)
        {
            CatManager.instance.currentCat.GoToPoint(hit.point);
        }
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(destructionTime);
        Destroy(gameObject);
    }
}
