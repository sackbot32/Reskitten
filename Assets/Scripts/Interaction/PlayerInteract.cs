using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public InputActionAsset inputActions;
    private IInteract currentInteract;
    private GameObject currentInteractObject;
    Ray ray;
    RaycastHit hit;
    private bool mantainInteraction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit, 100))
        {
            if(hit.collider.gameObject.TryGetComponent<IInteract>(out currentInteract) 
                && hit.collider.gameObject != currentInteractObject)
            {
                currentInteractObject = hit.collider.gameObject;
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
        }
        if (inputActions.FindAction("Interact").IsPressed())
        {
            if (currentInteractObject != null && !currentInteract.IsInteracting())
            {
                mantainInteraction = currentInteract.RecieveInteraction();
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
        
    }
}
