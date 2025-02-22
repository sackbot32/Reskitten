using UnityEngine;

public class CatStickToStick : MonoBehaviour
{
    public CatStickTool tool;
    [SerializeField]
    private AudioSource catchSource;
    private void OnTriggerEnter(Collider other)
    {
        CatController controller = other.gameObject.GetComponent<CatController>();
        if (controller != null)
        {
            if (controller.enabled == true && controller.agent.enabled)
            {
                SFXPlayer.StaticPlaySound(catchSource, catchSource.clip, true);
                tool.StickCatToTool(controller, transform);
            }

        }
    }
}
