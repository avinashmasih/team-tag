using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDetection : MonoBehaviour
{
    public static bool hasPlayerCrossed = false;
    public static bool hasAICrossed = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasPlayerCrossed = true;
            Destroy(other.gameObject);
        }

    }

    private void Update()
    {
        if(!GameObject.FindGameObjectWithTag("EnemyAI"))
        {
            hasAICrossed = true;
        }
    }

}
