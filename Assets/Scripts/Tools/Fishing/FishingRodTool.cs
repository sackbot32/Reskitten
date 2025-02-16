using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FishingRodTool : MonoBehaviour,ITool
{
    public GameObject hookPrefab;
    private GameObject currentHook;
    public Transform shootPoint;
    public float travelTime;
    public float shootSpeed;
    public float timeTillSelfDestruct = 5;
    public Sprite toolSprite;
    public LineRenderer lRenderer;
    private GameObject currentOutlined;
    [SerializeField]
    private AudioSource fishingRodSource;
    [SerializeField]
    private AudioSource equipSource;
    public List<AudioClip> soundList = new List<AudioClip>();
    //0 shoot sound
    //1 reel sound
    //2 equip sound

    private void Start()
    {
        lRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if(currentHook != null)
        {
            lRenderer.SetPosition(0, shootPoint.position);
            lRenderer.SetPosition(1, currentHook.transform.position);
        }

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        { 
            if(hit.collider.tag == "HookPoint" && hit.collider.gameObject.GetComponent<MeshOutline>() != null)
            {
                if(currentOutlined != null && currentOutlined != hit.collider.gameObject)
                {
                    currentOutlined.GetComponent<MeshOutline>().enabled = false;
                    currentOutlined = hit.collider.gameObject;
                    currentOutlined.GetComponent<MeshOutline>().enabled = true;
                }
                else
                {
                    currentOutlined = hit.collider.gameObject;
                    currentOutlined.GetComponent<MeshOutline>().enabled = true;
                }
            } else
            {
                if (currentOutlined != null)
                { 
                    currentOutlined.GetComponent<MeshOutline>().enabled = false;
                    currentOutlined = null;
                }
            }
        }
        else
        {
            if(currentOutlined != null)
            {
                currentOutlined.GetComponent<MeshOutline>().enabled = false;
                currentOutlined = null;
            }
        }
         
    }
    public Sprite GetImage()
    {
        return toolSprite;
    }

    public void Main()
    {
        if (currentHook == null) 
        {
            SFXPlayer.StaticPlaySound(fishingRodSource, soundList[0], true);
            lRenderer.enabled = true;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Vector3 dir = (ray.GetPoint(10) - shootPoint.position).normalized;
            currentHook = Instantiate(hookPrefab,shootPoint.position,Quaternion.identity);
            Rigidbody rb = currentHook.GetComponent<Rigidbody>();
            currentHook.GetComponent<HookCollision>().rodTool = gameObject.GetComponent<FishingRodTool>();
            currentHook.transform.forward = ray.direction;
            rb.AddForce(dir * shootSpeed);
            StartCoroutine(DestroyAfterTime(timeTillSelfDestruct,currentHook));
        }
    }

    public void Passive()
    {

    }

    public void Secondary()
    {

    }

    public void UpMain()
    {

    }

    public void UpSecondary()
    {

    }

    public void TowardHook()
    {
        if (currentHook != null)
        {
            fishingRodSource.loop = true;
            SFXPlayer.StaticPlaySound(fishingRodSource, soundList[1], true);
            PlayerManager.instance.playerRb.useGravity = false;
            Rigidbody rb = currentHook.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            
            Vector3 toPlayerDir = (PlayerManager.instance.player.transform.position - currentHook.transform.position).normalized;
            PlayerManager.instance.player.transform.DOMove(currentHook.transform.position + toPlayerDir * 2, travelTime).OnComplete(() => {
                fishingRodSource.loop = false;
                fishingRodSource.Stop();
                lRenderer.enabled = false;
                PlayerManager.instance.playerRb.linearVelocity = Vector3.zero;
                PlayerManager.instance.playerRb.useGravity = true;
                Destroy(currentHook);
                
                });
        }
    }

    IEnumerator DestroyAfterTime(float timeTillDestruction, GameObject objectToDestroy)
    {
        yield return new WaitForSeconds(timeTillDestruction);
        lRenderer.enabled = false;
        Destroy(objectToDestroy);
    }

    public void OnEquip()
    {
        if(equipSource != null)
        {
            SFXPlayer.StaticPlaySound(equipSource, soundList[2], true);
        }
    }

    public void OnUnequip()
    {
        if (equipSource != null)
        {
            SFXPlayer.StaticPlaySound(equipSource, soundList[2], true);
        }
    }
}
