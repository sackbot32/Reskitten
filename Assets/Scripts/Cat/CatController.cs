using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CatController : MonoBehaviour
{
    public NavMeshAgent agent;
    public float blockSightLenght;


    private void Start()
    {

    }

    public void GoToPoint(Vector3 point,bool testCanSee = false)
    {
        if (!testCanSee)
        {
            agent.SetDestination(point);
        } else
        {
            Vector3 dir = (point - transform.position ).normalized;
            if (!Physics.Raycast(transform.position,dir,blockSightLenght))
            {
                agent.SetDestination(point);
            }
        }
    }
}
