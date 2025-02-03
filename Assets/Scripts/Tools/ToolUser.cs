using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class ToolUser : MonoBehaviour
{

    public InputActionReference main;
    public InputActionReference secondary;
    public GameObject tool;
    public List<GameObject> tools;
    private ITool currentTool;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeTool(0);
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

    public void ChangeTool(int whichTool)
    {
        foreach (GameObject tool in tools)
        {
            tool.SetActive(false);
        }

        tools[whichTool].SetActive(true);
        if (tools[whichTool].TryGetComponent<ITool>(out currentTool))
        {
            print("tool gotten");
        }
    }

    public void AddTool(GameObject tool)
    {
        tool.transform.parent = transform;
        tool.transform.position = Vector3.zero;
        tools.Add(tool);
    }
}
