using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GlowStickTool : MonoBehaviour, ITool
{
    public Transform shootPoint;
    public Sprite toolSprite;
    private CrowdInstance selectedInstance;
    private bool used;
    private GameObject currentOutlined;
    [SerializeField]
    private MeshRenderer stickMesh;
    public float intensity;
    public Color[] colorModes;
    //0 None
    //1 Has public
    [Header("Sound source")]
    [SerializeField]
    private AudioSource userSource;
    [SerializeField]
    private AudioSource equipSource;
    public List<AudioClip> soundList = new List<AudioClip>();
    //0 fail sound
    //1 selectCrowd sound
    //2 selectPos sound
    //3 equip sound
    public Sprite GetImage()
    {
        return toolSprite;
    }
    private void Start()
    {
        stickMesh.materials[2].SetColor("_EmissionColor", colorModes[0]*intensity);
    }

    private void Update()
    {

        Passive();
        
    }
    public void Main()
    {
        if (!used) { 
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if(selectedInstance == null)
                {
                    if (hit.collider.gameObject.GetComponent<CrowdInstance>() != null) 
                    {
                        used = true;
                        if(userSource != null)
                        {
                            SFXPlayer.StaticPlaySound(userSource, soundList[1], true);
                        }
                        selectedInstance = hit.collider.gameObject.GetComponent<CrowdInstance>();
                        stickMesh.materials[2].SetColor("_EmissionColor", colorModes[1] * intensity);
                    } else
                    {
                        if (userSource != null)
                        { 
                            SFXPlayer.StaticPlaySound(userSource, soundList[0], true);
                        }
                    }
                } else
                {
                    if (hit.collider.gameObject.GetComponent<CrowdPosition>() != null)
                    {
                        used = true;
                        if (userSource != null)
                        { 
                            SFXPlayer.StaticPlaySound(userSource, soundList[2], true);
                        }
                        selectedInstance.GetComponent<MeshOutline>().enabled = false;
                        hit.collider.gameObject.GetComponent<CrowdPosition>().SetCrowd(selectedInstance.gameObject);
                        stickMesh.materials[2].SetColor("_EmissionColor", colorModes[0] * intensity);
                        selectedInstance = null;
                    } else
                    {
                        if (userSource != null)
                        { 
                            SFXPlayer.StaticPlaySound(userSource, soundList[0], true);
                        }
                    }
                }
            } else
            {
                if (userSource != null)
                { 
                    SFXPlayer.StaticPlaySound(userSource, soundList[0], true);
                }
            }
        }
        
    }

    public void UpMain()
    {
        used = false;
    }

    public void Passive()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (selectedInstance == null)
            {
                if (hit.collider.gameObject.GetComponent<CrowdInstance>() != null)
                {
                    if (currentOutlined != null && currentOutlined != hit.collider.gameObject)
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
                    if(currentOutlined != null)
                    {
                        currentOutlined.GetComponent<MeshOutline>().enabled = false;
                        currentOutlined = null;
                    }
                }
            } else
            {
                if (hit.collider.gameObject.GetComponent<CrowdPosition>() != null)
                {
                    if (currentOutlined != null && currentOutlined != hit.collider.gameObject)
                    {
                        print("set false before going away");
                        currentOutlined.GetComponent<MeshOutline>().enabled = false;
                        currentOutlined = hit.collider.gameObject;
                        currentOutlined.GetComponent<MeshOutline>().enabled = true;
                    }
                    else
                    {
                        currentOutlined = hit.collider.gameObject;
                        currentOutlined.GetComponent<MeshOutline>().enabled = true;
                    }
                }
                else
                {
                    if (currentOutlined != null)
                    {
                        currentOutlined.GetComponent<MeshOutline>().enabled = false;
                        currentOutlined = null;
                    }
                }
            }

        } else
        {
            if (currentOutlined != null)
            {
                currentOutlined.GetComponent<MeshOutline>().enabled = false;
                currentOutlined = null;
            }
        }

        if(selectedInstance != null)
        {
            selectedInstance.GetComponent<MeshOutline>().enabled = true;
        }
    }

    public void Secondary()
    {
        if (selectedInstance != null) 
        {
            stickMesh.materials[2].SetColor("_EmissionColor", colorModes[0] * intensity);
            selectedInstance.GetComponent<MeshOutline>().enabled = false;
            selectedInstance = null;
        }
    }



    public void UpSecondary()
    {

    }

    public void OnEquip()
    {
        if (currentOutlined != null)
        {
            currentOutlined.GetComponent<MeshOutline>().enabled = false;
            currentOutlined = null;
        }
        if (equipSource != null)
        {
            SFXPlayer.StaticPlaySound(equipSource,soundList[3],true);
        }
    }
    public void OnUnequip()
    {
        if (currentOutlined != null)
        {
            currentOutlined.GetComponent<MeshOutline>().enabled = false;
            currentOutlined = null;
        }
        if (equipSource != null)
        {
            SFXPlayer.StaticPlaySound(equipSource, soundList[3], true);
        }
    }

}
