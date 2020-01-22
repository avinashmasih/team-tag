using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public int NextSceneIndex = 1;
    public bool StartExitTransition = false;
    public bool ReadyToExit = false;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StartExitTransition)
        {
            timer += Time.deltaTime;
        }

        if (timer >= 2.5f)
        {
            ReadyToExit = true;
        }

        if (ReadyToExit)
        {
            SceneManager.LoadScene(NextSceneIndex);
        }
    }
}
