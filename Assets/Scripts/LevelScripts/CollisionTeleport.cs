using UnityEngine;

public class CollisionTeleport : MonoBehaviour
{
    public Transform teleportlocation;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == PlayerManager.instance.player)
        {
            PlayerManager.instance.player.transform.position = teleportlocation.position;
        }
    }
}
