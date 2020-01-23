using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardBehaviour : MonoBehaviour
{
    public bool isActivated = false;
    public float speed = 10.0f;
    void Update()
    {
        if (isActivated)
        {
            transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime),
                transform.position.y,
                transform.position.z);
        }
    }
}
