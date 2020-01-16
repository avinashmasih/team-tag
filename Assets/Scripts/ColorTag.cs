using UnityEngine;
using UnityEngine.UI;

public class ColorTag : MonoBehaviour
{
    public Color tagColor;

    public Image buttonImage;

    public void Start()
    {
        buttonImage.color = tagColor;
    }

    public void ChangeColor()
    {
        var sprayCan = FindObjectOfType<SprayCan>();
        if (!sprayCan)
        {
            Debug.LogError("Couldn't find the spray can");
            return;
        }

        sprayCan.ChangeColor(tagColor);
        // We don't want to spray paint when selecting a color.
        sprayCan.Refill(sprayCan.SprayRate * Time.deltaTime);
    }
}
