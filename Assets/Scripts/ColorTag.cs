using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class ColorTag : MonoBehaviour
{
    public Color tagColor;

    private Button button;

    public void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeColor);
        GetComponent<Image>().color = tagColor;
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
