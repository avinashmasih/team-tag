using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureToSprite : MonoBehaviour
{
    public Camera renderCamera;
    public GameObject matObject;

    private Texture2D targetSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            targetSprite = RTImage(renderCamera);
            var temp = matObject.GetComponent<MeshRenderer>().material.GetTexture("_BaseMap");
            Debug.Log(temp);
            matObject.GetComponent<MeshRenderer>().material.SetTexture("_BaseMap", targetSprite);
        }
            
    }

    // Take a "screenshot" of a camera's Render Texture.
    Texture2D RTImage(Camera camera)
    {
        // The Render Texture in RenderTexture.active is the one
        // that will be read by ReadPixels.
        var currentRT = RenderTexture.active;
        RenderTexture.active = camera.targetTexture;

        // Render the camera's view.
        camera.Render();

        // Make a new texture and read the active Render Texture into it.
        Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        image.Apply();

        // Replace the original active Render Texture.
        RenderTexture.active = currentRT;
        return image;
    }
}
