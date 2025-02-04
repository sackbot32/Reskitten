using UnityEngine;
using UnityEngine.Events;

public class InteractAction : MonoBehaviour,IInteract
{
    private bool isInteracting;
    public Sprite interactImage;
    public UnityEvent interactEvent;
    private bool doing;
    public void EndInteraction(bool mode = false)
    {
         doing = false;
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
        if (!doing)
        {
            interactEvent.Invoke();
            doing = true;
        }
        return false;
    }

    public Sprite ReturnInteractSprite()
    {
         return interactImage;
    }

    
}
