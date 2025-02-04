using UnityEngine;

public interface IInteract 
{
    public bool RecieveInteraction();
    public void MaintainInteraction();
    public void EndInteraction(bool mode = false);

    public bool IsInteracting();

    public Sprite ReturnInteractSprite();
}
