#define SPRAY_DEBUG


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayPaint : MonoBehaviour
{
    public Vector3         SprayLocation { get => sprayLocation; }
    public int             paintRadius = 10;

    private Camera          mainCamera;
    private SprayCan        sprayCan;
    private Vector3         sprayLocation;
    private Transform       sprayReticleTransform;


    // Start is called before the first frame update
    void Start()
    {
        transform.position    = Vector3.zero;
        mainCamera            = Camera.main;
        Cursor.visible        = false;
        sprayCan              = GetComponent<SprayCan>();
        sprayReticleTransform = GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 sprayScreenPos = Input.mousePosition;

        // Aiming
        PositionReticle(sprayScreenPos);

        // Get the final location of the spray if the button is pressed
        if (sprayCan.Spraying)
        {
            CastSpray(sprayScreenPos, out sprayLocation);
        }
    }


    // Position reticle in the world space in front of the camera
    private void PositionReticle(Vector3 i_sprayScreenPosition)
    {

        Vector3 sprayScreenPositionCamera   = new Vector3(i_sprayScreenPosition.x, i_sprayScreenPosition.y, mainCamera.nearClipPlane);
        Vector3 reticlePos                  = mainCamera.ScreenToWorldPoint(sprayScreenPositionCamera);

        sprayReticleTransform.position      = reticlePos;
    }


    // Get the final world space location of the spray
    private void CastSpray(Vector3 i_sprayScreenPos, out Vector3 o_canvasPoint)
    {
        Ray         sprayLine = mainCamera.ScreenPointToRay(i_sprayScreenPos);
        RaycastHit  sprayHit;

        if (Physics.Raycast(sprayLine.origin, sprayLine.direction.normalized, out sprayHit, Mathf.Infinity))
        {

#if SPRAY_DEBUG
            Debug.DrawRay(sprayLine.origin, sprayLine.direction.normalized * sprayHit.distance, Color.red);
#endif

            o_canvasPoint = sprayHit.point;

            Renderer rend = sprayHit.transform.GetComponent<Renderer>();
            MeshCollider meshCollider = sprayHit.collider as MeshCollider;

            if (rend == null || rend.sharedMaterial == null || meshCollider == null)
                return;

            Texture2D tex = rend.material.mainTexture as Texture2D;
            Vector2 pixelUV = sprayHit.textureCoord;

            pixelUV.x *= tex.width;
            pixelUV.y *= tex.height;

            for(int i = 0; i < paintRadius; i++)
            {
                tex.SetPixel((int)pixelUV.x + i, (int)pixelUV.y + i, Color.red);
                tex.SetPixel((int)pixelUV.x + i, (int)pixelUV.y - i, Color.red);
                tex.SetPixel((int)pixelUV.x - i, (int)pixelUV.y + i, Color.red);
                tex.SetPixel((int)pixelUV.x - i, (int)pixelUV.y - i, Color.red);
                tex.Apply();
            } 
        }
        else
        {
            o_canvasPoint = Vector3.zero;
        }
    }
}
