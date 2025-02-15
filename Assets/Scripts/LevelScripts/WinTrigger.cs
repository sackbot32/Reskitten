using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private bool hasWon;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (!ToolUser.instance.canChange && !hasWon)
            {
                hasWon = true;
                PauseHud.instance.WinScreen();
            }
        }
    }
}
