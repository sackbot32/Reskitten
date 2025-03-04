using DG.Tweening;
using UnityEngine;

public class OutLineGrowth : MonoBehaviour
{
    public float startValue;
    public float finishValue;
    public float duration;

    private MeshOutline outline;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        outline = GetComponent<MeshOutline>();
        outline.enabled = false;
        outline.OutlineWidth = startValue;
        DOTween.To(() => outline.OutlineWidth, x=> outline.OutlineWidth = x, finishValue,duration).SetLoops(-1,LoopType.Yoyo);
        
    }

    
}
