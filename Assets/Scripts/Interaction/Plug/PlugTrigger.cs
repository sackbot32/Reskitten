using UnityEngine;
using UnityEngine.Events;

public class PlugTrigger : MonoBehaviour
{
    public Transform plugPosition;
    public UnityEvent interactPlugInEvent;
    public UnityEvent interactPlugOffEvent;
    public bool doOnce;
    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        GrabInteract grab = other.gameObject.GetComponent<GrabInteract>();
        if (grab != null)
        {
            if (grab.isPlug && !grab.IsInteracting() && !doOnce)
            {
                doOnce = true;
                grab.plugged = true;
                grab.EndInteraction();
                grab.PlugIn(plugPosition);
                grab.plugIn = gameObject.GetComponent<PlugTrigger>();
                interactPlugInEvent.Invoke();
            }
        }
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
