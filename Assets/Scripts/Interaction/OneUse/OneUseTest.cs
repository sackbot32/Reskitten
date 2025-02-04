using UnityEngine;

public class OneUseTest : MonoBehaviour, IOneUseInput
{
    public void DoOnce()
    {
        print("Do once test");
    }
}
