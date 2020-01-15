using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour
{
    public Spray spray;
    void Update() { transform.position = spray.SprayLocation; }
}
