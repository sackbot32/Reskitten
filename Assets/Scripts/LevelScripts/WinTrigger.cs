using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private bool hasWon;
    public int levelIndex;
    public GameObject lightOnWin;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (!ToolUser.instance.canChange && !hasWon)
            {
                if(PlayerPrefs.GetInt(LevelSelectHud.LEVELKEY) < levelIndex)
                {
                    PlayerPrefs.SetInt(LevelSelectHud.LEVELKEY,levelIndex);
                }
                hasWon = true;
                PauseHud.instance.WinScreen();
            }
        }
    }
}
