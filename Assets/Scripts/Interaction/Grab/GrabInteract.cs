using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GrabInteract : MonoBehaviour,IInteract
{
    public Sprite interactImage;
    private bool isInteracting;
    private Rigidbody rb;
    Vector3 dir;
    private Vector3 targetPoint;
    public float force = 50.0f;
    public bool isPlug;
    public bool plugged;
    public PlugTrigger plugIn;
    public bool allowToGrab = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isInteracting && allowToGrab && !plugged)
        {
            targetPoint = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)).GetPoint(3);
            dir = (targetPoint - transform.position).normalized;
            // Applying the force directly would result in a very wobbly experience
            // Hence we add this check to stabilize the object depending on it's distance from the targetPoint
            float distance = Vector3.Distance(targetPoint, transform.position);
            if(distance < 1)
            {
                distance = 1;
            }
            if (Vector3.Distance(targetPoint,transform.position) > 0.1f && Vector3.Distance(targetPoint, transform.position) < 1f)
            {
                rb.AddForce((dir * force)/distance);
            }
        }
    }
    public void EndInteraction(bool mode = false)
    {
        if (!plugged)
        {
            print("ending");
            rb.useGravity = true;
            rb.linearDamping = 0;
            rb.constraints = RigidbodyConstraints.None;
            isInteracting = false;
        }
    }

    public bool IsInteracting()
    {
        return isInteracting;
    }

    public void MaintainInteraction()
    {


    }

    public bool RecieveInteraction()
    {
        print("Before if");
        if (plugged)
        {
            print("After if");
            PlugOff();
        }
        rb.useGravity = false;
        rb.linearDamping = 10;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        isInteracting = true;
        return true;
    }

    public Sprite ReturnInteractSprite()
    {
        return interactImage;
    }

    public void PlugIn(Transform plugTransform)
    {
        transform.position = plugTransform.position;
        transform.forward = plugTransform.forward;
        rb.isKinematic = true;
    }

    public void PlugOff()
    {
        
        if (plugIn != null)
        {
            
            plugged = false;
            plugIn.plugCollider.enabled = false;
            rb.isKinematic = false;
            transform.position = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)).GetPoint(3);
            plugIn.interactPlugOffEvent.Invoke();
            StartCoroutine(DisablePluggingForTime(1f,plugIn));
            plugIn = null;
        }
    }

    IEnumerator DisablePluggingForTime(float time,PlugTrigger plug)
    {
        yield return new WaitForSeconds(time);
        plug.plugCollider.enabled = true;
        plug.doOnce = false;

    }

}
