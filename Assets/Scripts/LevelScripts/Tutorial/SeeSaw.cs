using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class SeeSaw : MonoBehaviour
{
    public float correctAngle = -30;
    public NavMeshSurface surface;
    private bool negative;
    private bool hasItemOn;
    private bool canGenerate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canGenerate = true;
        if(correctAngle < 0)
        {
            negative = true;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<GrabInteract>())
        {
            print("Item on");
            hasItemOn = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<GrabInteract>())
        {
            print("Item off");
            hasItemOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hasItemOn) 
        {
            if (!negative)
            {
                print("Pre-Building pos " + transform.rotation.eulerAngles.x);
                if(transform.rotation.eulerAngles.x >= correctAngle && canGenerate)
                {
                    print("Building pos");
                    canGenerate = false;
                    surface.BuildNavMesh();
                }
            }
            else
            {
                print("Pre-Building neg " + transform.rotation.eulerAngles.x);
                if ((transform.rotation.eulerAngles.x - 360f) <= correctAngle && canGenerate)
                {
                    print("Building neg");
                    canGenerate = false;
                    surface.BuildNavMesh();
                }
            }
        }

        if (!hasItemOn && !canGenerate)
        {
            print("Reset rot " + transform.rotation.eulerAngles.x);
            if (transform.rotation.eulerAngles.x <= 1f || (transform.rotation.eulerAngles.x - 360f) >= -1f)
            {
                print("Reset build");
                surface.BuildNavMesh();
                canGenerate = true;
            }
        }
    }
}
