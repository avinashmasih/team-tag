using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PaintLevel : MonoBehaviour
{
    public float Min = 21;

    public float Max = 85;

    RectTransform rTransform;

    public void Start()
    {
        rTransform = (RectTransform)gameObject.transform;
    }

    public float FitPercentage(float percentage)
    {
        float range = Max - Min;
        var offset = range * percentage;
        return offset + Min;
    }

    public void SetMaskHeight(float percentage)
    {
        var oRect = rTransform.sizeDelta;
        oRect.y = FitPercentage(percentage);
        rTransform.sizeDelta = oRect;
    }
}
