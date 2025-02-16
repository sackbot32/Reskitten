using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioMixer mixer;
    public float musicVolumeMultiplier = 1f;
    public float sfxVolumeMultiplier = 1f;
    public Slider music;
    public Slider sfx;
    void Start()
    {
        if (PlayerPrefs.HasKey("MusicVol"))
        {
            ChangeMusicVolume(Mathf.Pow(10, (PlayerPrefs.GetFloat("MusicVol") / 20)));
        }
        if (PlayerPrefs.HasKey("SfxVol"))
        {
            ChangeSfxVolume(Mathf.Pow(10, (PlayerPrefs.GetFloat("SFXVol") / 20)));
        }
        if (PlayerPrefs.HasKey("MusicVol"))
        {
            music.value = Mathf.Pow(10, (PlayerPrefs.GetFloat("MusicVol") / 20)) / musicVolumeMultiplier;
        }
        else 
        {
            if (mixer.GetFloat("MusicVol", out float musicVol))
            {
                music.value = Mathf.Pow(10, (musicVol / 20));
                PlayerPrefs.SetFloat("MusicVol", musicVol);
            }
        }

        if (PlayerPrefs.HasKey("SfxVol"))
        {
            sfx.value = Mathf.Pow(10, (PlayerPrefs.GetFloat("SFXVol") / 20)) / sfxVolumeMultiplier;
        }
        else 
        {
            if (mixer.GetFloat("SFXVol", out float sfxVol))
            {
                sfx.value = Mathf.Pow(10, (sfxVol / 20));
                PlayerPrefs.SetFloat("SfxVol", sfxVol);
            }
        }
    }

    public void ChangeMusicVol(float vol)
    {
        
        ChangeMusicVolume(music.value);
        
    }
    public void ChangeSfxVol(float vol)
    {
        ChangeSfxVolume(sfx.value);
    }


    private void ChangeMusicVolume(float volume)
    {
        float trueVolume = Mathf.Log10(volume) * 20;
        trueVolume *= musicVolumeMultiplier;
        print(trueVolume);
        if (float.IsInfinity(trueVolume))
        {
            trueVolume = -80;
        }
        PlayerPrefs.SetFloat("MusicVol", trueVolume);
        mixer.SetFloat("MusicVol", trueVolume);
    }

    private void ChangeSfxVolume(float volume)
    {
        float trueVolume = Mathf.Log10(volume) * 20;
        trueVolume *= sfxVolumeMultiplier;
        if (float.IsInfinity(trueVolume))
        {
            trueVolume = -80;
        }
        PlayerPrefs.SetFloat("SFXVol", trueVolume);
        mixer.SetFloat("SFXVol", trueVolume);
    }
}
