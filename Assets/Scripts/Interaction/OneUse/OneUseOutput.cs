using UnityEngine;

public class OneUseOutput : MonoBehaviour
{
    public GameObject oneUseObject;
    private IOneUseInput oneUse;
    public bool used;
    private void Start()
    {
        oneUseObject.TryGetComponent<IOneUseInput>(out oneUse);
    }
    public void OneUse()
    {
        if (!used && oneUse != null) 
        { 
            used = true;
            oneUse.DoOnce();
        }
    }
}
