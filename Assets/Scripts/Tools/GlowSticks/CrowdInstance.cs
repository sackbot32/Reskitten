using UnityEngine;

public class CrowdInstance : MonoBehaviour
{

    public CrowdPosition currentPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(currentPosition != null)
        {
            currentPosition.SetCrowd(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
