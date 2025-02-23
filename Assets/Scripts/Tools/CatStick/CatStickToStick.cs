using UnityEngine;

public class CatStickToStick : MonoBehaviour
{
    public CatStickTool tool;
    [SerializeField]
    private AudioSource catchSource;
    private WinTrigger trigger;
    private void Start()
    {
        trigger = GameObject.FindGameObjectWithTag("Win")?.GetComponent<WinTrigger>();
    }


    private void OnTriggerEnter(Collider other)
    {
        CatController controller = other.gameObject.GetComponent<CatController>();
        if (controller != null)
        {
            if (controller.enabled == true && controller.agent.enabled)
            {
                trigger.lightOnWin.SetActive(true);
                SFXPlayer.StaticPlaySound(catchSource, catchSource.clip, true);
                tool.StickCatToTool(controller, transform);
            }

        }
    }
}
