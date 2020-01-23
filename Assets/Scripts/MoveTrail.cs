using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour
{
    public SprayPaint spray;
    void Update() { transform.position = spray.SprayLocation; }
}
