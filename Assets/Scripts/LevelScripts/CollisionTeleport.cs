using System.Collections;
using UnityEngine;

public class CollisionTeleport : MonoBehaviour
{
    public Transform teleportlocation;
    public float transitionDuration = 0.05f;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == PlayerManager.instance.player)
        {
            StartCoroutine(ProperTeleport(transitionDuration));
        }
    }

    private IEnumerator ProperTeleport(float duration)
    {
        LoadingManager.instance.ShowLoadScreen(true, duration);
        yield return new WaitForSeconds(duration);
        PlayerManager.instance.player.transform.position = teleportlocation.position;
        LoadingManager.instance.ShowLoadScreen(false, duration/2);

    }
}
