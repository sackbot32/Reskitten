using DG.Tweening;
using UnityEngine;

public class BackGroundMusicPlayer : MonoBehaviour
{
    public static BackGroundMusicPlayer instance;
    public float transitionTime;
    public AudioClip currentSong;
    public AudioSource source;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            source.clip = currentSong;
            if(source.clip != null)
            {
                source.Play();
            }

        } else
        {
            if(currentSong != BackGroundMusicPlayer.instance.currentSong)
            {
                BackGroundMusicPlayer.instance.ChangeSong(currentSong,transitionTime);
            }
        }
    }

    public void ChangeSong(AudioClip newSong,float transitionTime)
    {
        if(newSong == null)
        {
            source.DOFade(0f, transitionTime).OnComplete(() =>
            {
                source.Stop();
                currentSong = newSong;
                source.clip = newSong;
            });
        } else
        {
            source.DOFade(0f, transitionTime).OnComplete(() =>
            {
                source.Stop();
                currentSong = newSong;
                source.clip = newSong;
                source.DOFade(1f, transitionTime);
                source.Play();
            });
        }
    }
}
