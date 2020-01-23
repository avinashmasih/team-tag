using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Camera : MonoBehaviour
{
    public StartMenu startMenu;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startMenu.StartLevel1Transition)
        {
            anim.SetTrigger("transition");
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Level1CameraStillBegin"))
        {
            startMenu.StartLevel1 = true;
        }
    }
}
