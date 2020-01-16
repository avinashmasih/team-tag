using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChooser : MonoBehaviour
{
    /// <summary>
    /// The colors that the player can choose.
    /// </summary>
    [Tooltip("The colors that the player can choose.")]
    public Color[] colors;

    /// <summary>
    /// The object that will allow the player to select a specific color.
    /// </summary>
    [Tooltip("The object that will allow the player to select a specific color.")]
    public ColorTag tagInstance;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<SprayCan>().color = colors[0];
        foreach (var color in colors)
        {
            ColorTag tag = Instantiate(tagInstance, transform);
            tag.tagColor = color;
        }
    }
}
