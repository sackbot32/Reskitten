using UnityEngine;
using UnityEngine.Events;

public class PlugTrigger : MonoBehaviour
{
    public Transform plugPosition;
    public UnityEvent interactPlugInEvent;
    public UnityEvent interactPlugOffEvent;

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        GrabInteract grab = other.gameObject.GetComponent<GrabInteract>();
        if (grab != null)
        {
            if (grab.isPlug && !grab.IsInteracting())
            {
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
