using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTransitionOut : MonoBehaviour
{
    public StartMenu startMenu;
    public float AnimSpeed = 1;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startMenu.StartExitTransition)
        {
            anim.SetBool("doTransition", true);
        }

        anim.speed = AnimSpeed;
    }
}
