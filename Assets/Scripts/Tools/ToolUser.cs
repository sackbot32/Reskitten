using UnityEngine;
using UnityEngine.InputSystem;

public class ToolUser : MonoBehaviour
{

    public InputActionReference main;
    public InputActionReference secondary;
    public GameObject tool;
    private ITool currentTool;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (tool.TryGetComponent<ITool>(out currentTool))
        {
            print("tool gotten");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (main.action.IsPressed())
        {
            print("Main being pressed");
            currentTool.Main();
            
        }
        if (main.action.WasReleasedThisFrame())
        {
            print("Main being pressed");
            currentTool.UpMain();
        }

        if (secondary.action.IsPressed())
        {
            print("Secondary being pressed");
            currentTool.Secondary();
        }
        if (secondary.action.WasReleasedThisFrame())
        {
            currentTool.UpSecondary();
        }

    }
}
