using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlugTrigger : MonoBehaviour
{
    public Transform plugPosition;
    public UnityEvent interactPlugInEvent;
    public UnityEvent interactPlugOffEvent;
    public bool doOnce;
    public Collider plugCollider;

    private void Start()
    {
        plugCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        GrabInteract grab = other.gameObject.GetComponent<GrabInteract>();
        if (grab != null)
        {
            if (grab.isPlug && grab.IsInteracting() && !doOnce)
            {
                grab.allowToGrab = false;
                doOnce = true;
                grab.plugged = true;
                grab.plugIn = gameObject.GetComponent<PlugTrigger>();
                grab.EndInteraction();
                grab.PlugIn(plugPosition);
                interactPlugInEvent.Invoke();
                StartCoroutine(AllowGrabAfterTime(grab));
            }
        }
    }

    IEnumerator AllowGrabAfterTime(GrabInteract plug, float time = 1f)
    {
        yield return new WaitForSeconds(time);
        plug.allowToGrab = true;
        plugCollider.enabled = false;
        plug.isInteracting = false;

    }

    


    public void TestIn()
    {
        print("Is in");
    }

    public void TestOff()
    {
        print("Is off");
    }
}
