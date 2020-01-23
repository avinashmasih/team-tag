using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySystemManager : MonoBehaviour
{
    public bool enabledByDefault = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(enabledByDefault);
    }

    public void ActivateGameplaySystem()
    {
        Invoke("MakeActive", 5);
    }

    void MakeActive()
    {
        gameObject.SetActive(true);
    }
}
