using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class WeightedAudio
{
    public AudioClip clip;
    public int weight;
}
public class CatController : MonoBehaviour
{
    public NavMeshAgent agent;
    public float blockSightLenght;
    public bool canJump = false;
    public Animator anim;
    private bool hasMeowed;
    public AudioSource source;
    public List<WeightedAudio> idleMeows = new List<WeightedAudio>();
    public List<AudioClip> goingMeows = new List<AudioClip>();
    public float minTimeTillMeow;
    public float maxTimeTillMeow;

    private void Start()
    {
        if(anim == null)
        {
            anim = transform.GetChild(0).gameObject.GetComponent<Animator>();
        }
        StartCoroutine(CatRandomMeow(UnityEngine.Random.Range(minTimeTillMeow, maxTimeTillMeow)));
    }

    public void GoToPoint(Vector3 point,bool testCanSee = false)
    {
        if (!testCanSee)
        {
            if (agent.enabled)
            {
                if (!hasMeowed)
                {
                    hasMeowed = true;
                    SFXPlayer.StaticPlaySound(source, goingMeows[UnityEngine.Random.Range(0,goingMeows.Count)], true);
                }
                agent.SetDestination(point);
            }
        } else
        {
            Vector3 dir = (point - transform.position ).normalized;
            if (!Physics.Raycast(transform.position,dir,blockSightLenght))
            {
                if (agent.enabled)
                {
                    if (!hasMeowed)
                    {
                        hasMeowed = true;
                        SFXPlayer.StaticPlaySound(source, goingMeows[UnityEngine.Random.Range(0, goingMeows.Count)], true);
                    }
                    agent.SetDestination(point);
                }
            }
        }
    }

    private void Update()
    {
        if(agent.isOnOffMeshLink && canJump)
        {
            agent.autoTraverseOffMeshLink = false;
            StartCoroutine(Parabola(agent, 5, 1));
        }
        if (agent.enabled)
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        hasMeowed = false;
                    }
                }
            }
        }
        


        anim.SetFloat("Speed",agent.velocity.magnitude/agent.speed);
    }

    IEnumerator Parabola(NavMeshAgent agent, float height, float duration)
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
        float normalizedTime = 0.0f;
        while (normalizedTime < 1.0f)
        {
            float yOffset = height * 4.0f * (normalizedTime - normalizedTime * normalizedTime);
            agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        agent.CompleteOffMeshLink();
    }

    IEnumerator CatRandomMeow(float timeTillMeow)
    {
        yield return new WaitForSeconds(timeTillMeow);
        AudioClip clipToPlay = null;
        int total = 0;
        foreach (WeightedAudio forTotal in idleMeows)
        {
            total += forTotal.weight;
        }

        int random = UnityEngine.Random.Range(0, total);
        foreach (WeightedAudio forSelection in idleMeows)
        {
            if(random < forSelection.weight)
            {
                clipToPlay = forSelection.clip;
                break;
            }
            random -= forSelection.weight;
        }
        SFXPlayer.StaticPlaySound(source, clipToPlay, true);
        StartCoroutine(CatRandomMeow(UnityEngine.Random.Range(minTimeTillMeow, maxTimeTillMeow)));
    }
}
