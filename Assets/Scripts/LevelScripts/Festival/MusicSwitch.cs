using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.Events;

public class MusicSwitch : MonoBehaviour,ISwitchInput
{
    public List<AudioSource> musicSources = new List<AudioSource>();
    public float fadeDuration;
    private AudioSource currentAudio;

    public int conditionFullfilledPos;
    public int conditionListPos;
    public UnityEvent conditionEvent;
    private int currentIndex;

    public int CurrentPos()
    {
        return currentIndex;
    }

    private void Start()
    {
        foreach (AudioSource audioSource in musicSources)
        {
            audioSource.loop = true;
            audioSource.volume = 0;
            audioSource.Play();
        }
    }

    public int GetLength()
    {
        return musicSources.Count;
    }

    public bool SwitchInput(int input)
    {
        if (input < 0)
        {
            return false;
        }
        else if (input >= musicSources.Count)
        {
            return false;
        }
        else
        {
            currentIndex = input;
            if(currentAudio != null)
            {
                currentAudio.DOFade(0,fadeDuration);
            }
            currentAudio = musicSources[input];
            currentAudio.DOFade(1, fadeDuration);

            if (conditionFullfilledPos == input)
            {
                ConditionList.instance.SetConditionTrue(conditionListPos);
                conditionEvent.Invoke();
            }
            else
            {
                ConditionList.instance.SetConditionFalse(conditionListPos);
            }


            return true;
        }
    }
}
