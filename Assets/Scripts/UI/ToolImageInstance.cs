using UnityEngine;
using UnityEngine.UI;

public class ToolImageInstance : MonoBehaviour
{

    public Image backGroundImage;
    public Image toolImage;

    private Color ogColor;

    private void Awake()
    {
        ogColor = backGroundImage.color;
    }

    public void SetImage(Sprite newImage)
    {
        toolImage.sprite = newImage;
    }

    public void ChangeState(bool selected)
    {
        if (selected)
        {
            backGroundImage.color = new Color(ogColor.r - ogColor.r/2f, 
                ogColor.g - ogColor.g / 2f, 
                ogColor.b - ogColor.b / 2f);
            toolImage.color = new Color(Color.white.r - Color.white.r/2f, 
                Color.white.g - Color.white.g / 2f, 
                Color.white.b - Color.white.b / 2f);
        } else
        {
            backGroundImage.color = ogColor;
            toolImage.color = Color.white;
        }
    }
}
