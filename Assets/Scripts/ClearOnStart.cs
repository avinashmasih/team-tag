using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
        Color[] colArray = new Color[mesh.vertices.Length];
        mesh.colors = colArray;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
