using UnityEngine;

public class CatManager : MonoBehaviour
{
    public static CatManager instance;
    public CatController currentCat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
