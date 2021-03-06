﻿#if DEBUG
#define SPRAY_DEBUG
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SprayCan))]
public class SprayPaint : MonoBehaviour
{
    public Vector3         SprayLocation { get => _sprayLocation; }
    private float           SprayDistance = 15;

    private Camera          _mainCamera;
    private SprayCan        _sprayCan;
    private Vector3         _sprayLocation;
    private Color[]         _colorArray;
    private bool            _newColArray = false;
    private Mesh            _mesh;

    private List<int> indices;

    // Start is called before the first frame update
    void Start()
    {
        transform.position    = Vector3.zero;
        _mainCamera            = Camera.main;
        _sprayCan              = GetComponent<SprayCan>();

        indices = new List<int>();
        //ClearWall();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 sprayScreenPos;

        if (WiimoteInput.isConnected)
        {
            Vector2 wiiMotePointerPos = WiimoteInput.GetPointerPosition();
            sprayScreenPos = new Vector3(wiiMotePointerPos.x, wiiMotePointerPos.y, 0f);
        }
        else
        {
            sprayScreenPos = Input.mousePosition;
        }
        

        // Get the final location of the spray if the button is pressed
        if (_sprayCan.Spraying)
        {
            CastSpray(sprayScreenPos, out _sprayLocation);
        }
    }

    // Get the final world space location of the spray
    private void CastSpray(Vector3 i_sprayScreenPos, out Vector3 o_canvasPoint)
    {
        Ray         sprayLine = _mainCamera.ScreenPointToRay(i_sprayScreenPos);
        RaycastHit  sprayHit;

        if (Physics.Raycast(sprayLine.origin, sprayLine.direction.normalized, out sprayHit, SprayDistance))
        {

#if SPRAY_DEBUG
            Debug.DrawRay(sprayLine.origin, sprayLine.direction.normalized * sprayHit.distance, Color.red);
#endif
            
            o_canvasPoint = sprayHit.point;

            // Find if something is hit
            Renderer rend = sprayHit.transform.GetComponent<Renderer>();
            MeshCollider meshCollider = sprayHit.collider as MeshCollider;

            //Debug.Log(meshCollider.gameObject.name);

            if (rend == null || rend.sharedMaterial == null || meshCollider == null)
                return;

            //Debug.Log(rend.sharedMaterial);

            //Find the game object being hit by spray
            _mesh = meshCollider.gameObject.GetComponent<MeshFilter>().sharedMesh;
            int[] triangles = _mesh.triangles;

            //Debug.Log(_mesh.name);
            //Clear the paint at the play
            if (!_newColArray)
            {
                _newColArray = true;
                _colorArray = new Color[_mesh.vertices.Length];
            }

            //Find the face hit by cast
            int vert1 = triangles[sprayHit.triangleIndex * 3 + 0];
            int vert2 = triangles[sprayHit.triangleIndex * 3 + 1];
            int vert3 = triangles[sprayHit.triangleIndex * 3 + 2];

            //color the face
            if (vert1 < _colorArray.Length &&
                vert2 < _colorArray.Length &&
                vert3 < _colorArray.Length)
            {
                _colorArray[vert1] = _sprayCan.color;
                _colorArray[vert2] = _sprayCan.color;
                _colorArray[vert3] = _sprayCan.color;

                if (_mesh.colors.Length == _colorArray.Length)
                    _mesh.colors = _colorArray;
            }
        }
        else
        {
            o_canvasPoint = Vector3.zero;
        }
    }
    private void OnApplicationQuit()
    {
        ClearWall();
    }

    public void ClearWall()
    {
        if (_newColArray)
            _colorArray = new Color[_mesh.vertices.Length];
        if (_mesh)
            _mesh.colors = _colorArray;
    }
}
