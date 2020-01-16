using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTexture : MonoBehaviour
{
    public Texture2D tex;
    public Color resetColor;

    private Color[] pixel;

    void Start()
    {
        pixel = tex.GetPixels();
    }

    void Update()
    {

    }
    private void OnApplicationQuit()
    {
        for (var i = 0; i < pixel.Length; ++i)
        {
            pixel[i] = resetColor;
        }

        tex.SetPixels(pixel);

        tex.Apply();
    }
}
