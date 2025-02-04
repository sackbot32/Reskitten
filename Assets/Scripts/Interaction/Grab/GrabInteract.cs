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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isInteracting)
        {
            targetPoint = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)).GetPoint(3);
            dir = (targetPoint - transform.position).normalized;
            // Applying the force directly would result in a very wobbly experience
            // Hence we add this check to stabilize the object depending on it's distance from the targetPoint
            if (Vector3.Distance(targetPoint,transform.position) > 0.1f)
            {
                rb.AddForce(dir * force);
            }
        }
    }
    public void EndInteraction(bool mode = false)
    {
        print("ending");
        rb.useGravity = true;
        rb.linearDamping = 0;
        rb.constraints = RigidbodyConstraints.None;
        isInteracting = false;
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


}
