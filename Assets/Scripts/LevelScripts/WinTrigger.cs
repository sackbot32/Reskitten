using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (!ToolUser.instance.canChange)
            {
                print("Win");
            }
        }
    }
}
