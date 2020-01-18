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
    private Color[]         colorArray;
    private Mesh            mesh;
    private bool            newColArray = false;
    private GameObject      meshObject;

    private List<int> indices;

    // Start is called before the first frame update
    void Start()
    {
        transform.position    = Vector3.zero;
        mainCamera            = Camera.main;
        Cursor.visible        = false;
        sprayCan              = GetComponent<SprayCan>();
        sprayReticleTransform = GetComponentInChildren<Transform>();

        indices = new List<int>();
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

            
            Mesh mesh = meshCollider.gameObject.GetComponent<MeshFilter>().sharedMesh;
            int[] triangles = mesh.triangles;

            if (!newColArray)
            {
                newColArray = true;
                colorArray = new Color[mesh.vertices.Length];
            }

            int vert1 = triangles[sprayHit.triangleIndex * 3 + 0];
            int vert2 = triangles[sprayHit.triangleIndex * 3 + 1];
            int vert3 = triangles[sprayHit.triangleIndex * 3 + 2];

            Debug.Log($"{mesh.colors[vert1]}, {mesh.colors[vert2]}, {mesh.colors[vert3]}");

            /*for (int i = 0; i < indices.Count; i++)
            {
                Debug.Log(mesh.colors[indices[i]]);
            }

            Debug.Log(meshObject.GetComponent<Renderer>().material);*/

            colorArray[vert1] = Color.green;
            colorArray[vert2] = Color.green;
            colorArray[vert3] = Color.green;

            mesh.colors = colorArray;

            //Debug.Log($"{colorArray[vert1]}, {colorArray[vert2]}, {colorArray[vert3]}");

            /*int p0 = triangles[sprayHit.triangleIndex * 3 + 0];
            int p1 = triangles[sprayHit.triangleIndex * 3 + 1];
            int p2 = triangles[sprayHit.triangleIndex * 3 + 2];


            colorArray[0] = Color.red;
            colorArray[1] = Color.red;
            colorArray[2] = Color.red;

            mesh.colors = colorArray;


            /*Mesh mesh = meshCollider.sharedMesh;
            Vector3[] vertices = mesh.vertices;

            for (int i = 0; i < vertices.Length; i++)
            {
                Debug.Log(vertices[i]);
            }

            /*Texture2D tex = rend.material.mainTexture as Texture2D;
            Vector2 pixelUV = sprayHit.textureCoord;

            pixelUV.x *= tex.width;
            pixelUV.y *= tex.height;

            tex.SetPixel((int)pixelUV.x , (int)pixelUV.y , Color.red);
            tex.Apply();

            /*for(int i = 0; i < paintRadius; i++)
            {
                tex.SetPixel((int)pixelUV.x + i, (int)pixelUV.y + i, Color.red);
                tex.SetPixel((int)pixelUV.x + i, (int)pixelUV.y - i, Color.red);
                tex.SetPixel((int)pixelUV.x - i, (int)pixelUV.y + i, Color.red);
                tex.SetPixel((int)pixelUV.x - i, (int)pixelUV.y - i, Color.red);
                tex.Apply();
            }*/
        }
        else
        {
            o_canvasPoint = Vector3.zero;
        }
    }
}
