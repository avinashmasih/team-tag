using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardBehaviour : MonoBehaviour
{
    public bool isActivated = false;
    public float speed = 10.0f;
    private float time = 0.0f;
    void Update()
    {
        if (isActivated)
        {
            transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime),
                transform.position.y,
                transform.position.z);

            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 7.5f * Mathf.Sin(time* 3.0f));
            time += Time.deltaTime;
        }
    }
}
