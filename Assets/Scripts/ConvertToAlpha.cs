using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertToAlpha : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D Converter(Texture2D initialTex)
    {
        int x = initialTex.width;
        int y = initialTex.height;        
        Color[] pixels = initialTex.GetPixels(0, 0, x, y);
        Texture2D finaltex = new Texture2D(x, y);
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                if (initialTex.GetPixel(i, j) == Color.white)
                {
                    //Debug.Log("true");
                    finaltex.SetPixel(i, j, new Color(0, 0, 0, 0));
                }
                else
                {
                    //Debug.Log("false");
                    finaltex.SetPixel(i, j, initialTex.GetPixel(i, j));
                }
            }
        }
        finaltex.Apply();
        return finaltex;
    }
}
