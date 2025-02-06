using UnityEngine;
using UnityEngine.UIElements;

public class HookCollision : MonoBehaviour
{
    public FishingRodTool rodTool;
    RaycastHit hit;


    private void OnCollisionEnter(Collision collision)
    {
        if (Physics.Raycast(transform.position, (PlayerManager.instance.player.transform.position - transform.position).normalized,out hit,
            Vector3.Distance(PlayerManager.instance.player.transform.position, transform.position)))
        {
            if(hit.collider.gameObject == PlayerManager.instance.player)
            {
                rodTool.TowardHook();
            } else
            {

                rodTool.lRenderer.enabled = false;
                Destroy(gameObject);
            }
        } else
        {
            rodTool.TowardHook();
        }
    }
}
