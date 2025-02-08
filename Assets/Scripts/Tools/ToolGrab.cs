using UnityEngine;

public class ToolGrab : MonoBehaviour
{
    public GameObject toolToAdd;
    public Mesh itemMesh;
    private GameObject realTool;

    private void Start()
    {
        if (itemMesh != null)
        {
            transform.GetChild(0).gameObject.GetComponent<MeshFilter>().mesh = itemMesh;
        }
        realTool = Instantiate(toolToAdd,transform.position,Quaternion.identity,transform);
        realTool.SetActive(false);
    }

    private void OnValidate()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ToolUser.instance.AddTool(realTool,toolToAdd.transform.rotation);
            Destroy(gameObject);
        }
    }
}
