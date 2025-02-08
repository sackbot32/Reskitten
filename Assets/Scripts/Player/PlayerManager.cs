using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public GameObject player;
    public PlayerControl playerControl;
    public Rigidbody playerRb;
    public bool diferentGravity = false;
    public float gravityModifier;
    public bool stopDiferentGravity = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerControl = player.GetComponent<PlayerControl>();
        playerRb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (playerRb != null)
        {
            if (diferentGravity && !stopDiferentGravity)
            {
                playerRb.AddForce(player.transform.up * gravityModifier);
            }
        }
    }
}
