using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerSprites"))
        {
            if (!transform.parent.GetComponent<AIMovement>().targets.Contains(other.transform))
            {
                transform.parent.GetComponent<AIMovement>().targets.Add(other.transform);
                transform.parent.GetComponent<AIMovement>().hasDetected = true;
            }
        }

    }
}
