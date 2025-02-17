using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class SFXPlayer : MonoBehaviour
{

    public List<AudioClip> audioClipList = new List<AudioClip>();
    private AudioSource audioSource;
    private bool hasSceneChanged;
    private bool firstTime;

    private void Awake()
    {
        SceneManager.activeSceneChanged += CallOnSceneLoaded;
        audioSource = GetComponent<AudioSource>();
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
    }

    private void CallOnSceneLoaded(Scene arg0, Scene arg1)
    {
        if (firstTime)
        {
            hasSceneChanged = true;
        } else
        {
            firstTime = true;
        }
    }

    private void Update()
    {
        if(!audioSource.isPlaying && hasSceneChanged)
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundOnIndex(int index)
    {
        if (audioClipList.Count > index)
        {
            audioSource.pitch = 1f;
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(audioClipList[index]);
            }
            
        }
    }

    public void PlaySoundOnIndexNotPlaying(int index)
    {
        if (audioClipList.Count > index)
        {
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(audioClipList[index]);
        }
    }

    public void PlaySoundOnIndexRandomPitch(int index)
    {
        if (audioClipList.Count > index)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(audioClipList[index]);
        }
    }

    public static void StaticPlaySound(AudioSource audioSource, AudioClip clip, bool random)
    {
        if (random)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
        } else
        {
            audioSource.pitch = 1f;
        }
        audioSource.clip = clip;
        audioSource.Play();
    }
}
