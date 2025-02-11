using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CatController : MonoBehaviour
{
    public NavMeshAgent agent;
    public float blockSightLenght;
    public bool canJump = false;
    public Animator anim;

    private void Start()
    {
        if(anim == null)
        {
            anim = transform.GetChild(0).gameObject.GetComponent<Animator>();
        }
    }

    public void GoToPoint(Vector3 point,bool testCanSee = false)
    {
        if (!testCanSee)
        {
            if (agent.enabled)
            {
                agent.SetDestination(point);
            }
        } else
        {
            Vector3 dir = (point - transform.position ).normalized;
            if (!Physics.Raycast(transform.position,dir,blockSightLenght))
            {
                if (agent.enabled)
                { 
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
}
