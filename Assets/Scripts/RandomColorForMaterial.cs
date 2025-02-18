using System.Collections;
using UnityEngine;

public class RandomColorForMaterial : MonoBehaviour
{

    public string colorProperty;
    public float intensity = 1f;
    public float min;
    public MeshRenderer meshObject;
    public int materialIndex;
    public float timeToChange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        Color newColor = new Color(Random.Range(min, 1f), Random.Range(min, 1f), Random.Range(min, 1f), 1f);
        meshObject.materials[materialIndex].SetColor(colorProperty, newColor * intensity);
        StartCoroutine(ChangeLight());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ChangeLight()
    {
        Color ogColor = meshObject.materials[materialIndex].GetColor(colorProperty);
        Color newColor = new Color(Random.Range(min, 1f), Random.Range(min, 1f), Random.Range(min, 1f), 1f);
        float time = 0;
        while (time < timeToChange)
        {
            time += Time.deltaTime;
            meshObject.materials[materialIndex].SetColor(colorProperty, Color.Lerp(ogColor, (newColor * intensity), time / timeToChange));
            yield return null;
        }
        StartCoroutine(ChangeLight());
    }
}
