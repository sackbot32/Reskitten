using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class ToolUser : MonoBehaviour
{
    static public ToolUser instance;
    public InputActionAsset inputActions;
    public GameObject tool;
    public List<GameObject> tools;
    private ITool currentTool;
    private int currentToolIndex;
    public bool canChange = true;
    public ToolHud toolHud;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        UpdateHud();
        ChangeTool(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (inputActions.FindAction("Attack").IsPressed())
        {
            //print("Main being pressed");
            currentTool.Main();
            
        }
        if (inputActions.FindAction("Attack").WasReleasedThisFrame())
        {
            //print("Main being pressed");
            currentTool.UpMain();
        }

        if (inputActions.FindAction("Secondary").IsPressed())
        {
            //print("Secondary being pressed");
            currentTool.Secondary();
        }
        if (inputActions.FindAction("Secondary").WasReleasedThisFrame())
        {
            currentTool.UpSecondary();
        }
        if (canChange)
        {
            if (inputActions.FindAction("1").WasPressedThisFrame())
            {
                ChangeTool(0);
            }
            if (inputActions.FindAction("2").WasPressedThisFrame())
            {
                ChangeTool(1);
            }
            if (inputActions.FindAction("3").WasPressedThisFrame())
            {
                ChangeTool(2);
            }
            if (inputActions.FindAction("4").WasPressedThisFrame())
            {
                ChangeTool(3);
            }
            if (inputActions.FindAction("5").WasPressedThisFrame())
            {
                ChangeTool(4);
            }
            if (inputActions.FindAction("6").WasPressedThisFrame())
            {
                ChangeTool(5);
            }
            if (inputActions.FindAction("ToolScroll").ReadValue<float>() > 0)
            {
                if(currentToolIndex - 1 < 0)
                {
                    ChangeTool(tools.Count -1);
                } else
                {
                    ChangeTool(currentToolIndex - 1);
                }
            }
            if (inputActions.FindAction("ToolScroll").ReadValue<float>() < 0)
            {
                if(currentToolIndex + 1 >= tools.Count)
                {
                    ChangeTool(0);
                } else
                {
                    ChangeTool(currentToolIndex + 1);
                }
            }
        }
    }

    public void ChangeTool(int whichTool)
    {
        if(tools[whichTool] != null)
        {
            if(currentTool != null)
            {
                
                currentTool.OnUnequip();
            }
            currentToolIndex = whichTool;
            foreach (GameObject tool in tools)
            {
                tool.SetActive(false);
            }
            foreach (ToolImageInstance imageInstace in toolHud.toolImageInstances)
            {
                imageInstace.ChangeState(false);
            }


            tools[whichTool].SetActive(true);
            if (tools[whichTool].TryGetComponent<ITool>(out currentTool))
            {
                currentTool.OnEquip();
                toolHud.toolImageInstances[whichTool].ChangeState(true);
                print("tool gotten");
            }
        }
    }

    public void AddTool(GameObject tool,Quaternion rotation)
    {
        tools.Add(tool);
        int index = tools.IndexOf(tool);
        ChangeTool(index);
        tools[index].transform.parent = transform;
        tools[index].transform.localPosition = Vector3.zero;
        tools[index].transform.localRotation = rotation;
        UpdateHud();
    }

    public void UpdateHud()
    {
        if(toolHud != null)
        {
            List<Sprite> imageList = new List<Sprite>();
            foreach (GameObject tool in tools)
            {
                if(tool.TryGetComponent<ITool>(out ITool toolForImage))
                {
                    imageList.Add(toolForImage.GetImage());
                }
            }
            int index = 0;
            foreach (ToolImageInstance image in toolHud.toolImageInstances)
            {
                print("index:" + index);
                if (index < imageList.Count)
                {
                    image.gameObject.SetActive(true);
                    image.SetImage(imageList[index]);
                } else
                {
                    image.gameObject.SetActive(false);
                }

                index++;
            }
        }
    }
}
