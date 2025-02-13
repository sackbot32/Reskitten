using UnityEngine;

public class CatStickToStick : MonoBehaviour
{
    public CatStickTool tool;
    private void OnTriggerEnter(Collider other)
    {
        CatController controller = other.gameObject.GetComponent<CatController>();
        if (controller != null)
        {
            if (controller.enabled)
            {
                tool.StickCatToTool(controller, transform);
            }

        }
    }
}
