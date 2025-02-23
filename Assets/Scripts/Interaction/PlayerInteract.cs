using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    public InputActionAsset inputActions;
    private IInteract currentInteract;
    private GameObject currentInteractObject;
    public Image interactImage;
    Ray ray;
    RaycastHit hit;
    private bool mantainInteraction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit, 3.5f))
        {
            if(hit.collider.gameObject.TryGetComponent<IInteract>(out currentInteract) 
                && hit.collider.gameObject != currentInteractObject)
            {
                currentInteractObject = hit.collider.gameObject;
                if(!interactImage.enabled)
                {
                    interactImage.sprite = currentInteract.ReturnInteractSprite();
                    interactImage.enabled = true;
                }
            } else
            {
                if(hit.collider.gameObject != currentInteractObject && currentInteract != null)
                {
                    currentInteract.EndInteraction();
                    mantainInteraction = false;
                    currentInteract = null;
                }
                
                currentInteractObject = null;
            }
        } else
        {
            if(currentInteract != null)
            {
                currentInteract.EndInteraction();
                mantainInteraction = false;
                currentInteract = null;
            }
            currentInteractObject = null;
        }


        if (inputActions.FindAction("Interact").IsPressed())
        {
            if (currentInteract != null)
            {
                if (!currentInteract.IsInteracting())
                {
                    print("is Interacting exists");
                    mantainInteraction = currentInteract.RecieveInteraction();
                }
            }

        }

        if (mantainInteraction)
        {
            if (currentInteractObject != null)
            {
                currentInteract.MaintainInteraction();
            }
        }

        if (inputActions.FindAction("Interact").WasReleasedThisFrame())
        {
            if (currentInteract != null)
            { 
                currentInteract.EndInteraction();
            }
            mantainInteraction = false;
            currentInteract = null;
        }
        if(currentInteract == null && interactImage.enabled)
        {
            interactImage.enabled = false;
        }
        
    }
}
