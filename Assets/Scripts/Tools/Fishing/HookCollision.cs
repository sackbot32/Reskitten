using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HookCollision : MonoBehaviour
{
    public FishingRodTool rodTool;
    RaycastHit hit;
    [SerializeField]
    private AudioSource touchSource;
    public List<AudioClip> soundList = new List<AudioClip>();
    //0 goodHit sound
    //1 badHit sound

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "HookPoint")
        {
            if (Physics.Raycast(transform.position, (PlayerManager.instance.player.transform.position - transform.position).normalized, out hit,
            Vector3.Distance(PlayerManager.instance.player.transform.position, transform.position)))
            {
                if (hit.collider.gameObject == PlayerManager.instance.player)
                {
                    SFXPlayer.StaticPlaySound(touchSource, soundList[0], true);
                    rodTool.TowardHook();
                }
                else
                {
                    SFXPlayer.StaticPlaySound(touchSource, soundList[1], true);
                    rodTool.lRenderer.enabled = false;
                    Destroy(gameObject);
                }
            }
            else
            {
                SFXPlayer.StaticPlaySound(touchSource, soundList[0], true);
                rodTool.TowardHook();
            }
        } else
        {
            SFXPlayer.StaticPlaySound(touchSource, soundList[1], true);
            rodTool.lRenderer.enabled = false;
            Destroy(gameObject);
        }
        
    }
}
