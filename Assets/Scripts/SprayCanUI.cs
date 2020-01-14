using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprayCanUI : MonoBehaviour
{
    /// <summary>
    /// The spray can that controls this UI element.
    /// </summary>
    [Tooltip("The spray can that controls this UI element.")]
    public SprayCan sprayCan;
    /// <summary>
    /// The image that represents the color and fill level of the spray can.
    /// </summary>
    [Tooltip("The image that represents the color and fill level of the spray can.")]
    public Image fillImage;
    /// <summary>
    /// The maximum height of the image.
    /// </summary>
    private float maxHeight;

    // Start is called before the first frame update
    void Start()
    {
        sprayCan.OnColorChanged += ChangeColor;
        sprayCan.OnFillChanged += ChangeFill;
        maxHeight = fillImage.GetComponent<RectTransform>().sizeDelta.y;
    }

    private void ChangeFill(float newFill)
    {
        var rectTransform = fillImage.GetComponent<RectTransform>();
        var originalSize = rectTransform.sizeDelta;
        
        // We want it to be a percentage of its original height.
        rectTransform.sizeDelta = new Vector2(originalSize.x, newFill / sprayCan.MaxCapacity * maxHeight);
    }

    private void OnDestroy()
    {
        if (sprayCan)
            sprayCan.OnColorChanged -= ChangeColor;
    }

    void ChangeColor(Color oldColor, Color newColor)
    {
        fillImage.color = newColor;
    }
}
