using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Camera : MonoBehaviour
{
    public MoveForwardBehaviour moveForward;
    public StartMenu startMenu;
    private Animator anim;
    public bool moveForwardActive = false;
    public PlayerMove playerMove;
    public bool endGame = false;

    public bool movePlayer = false;

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

        if (startMenu.StartExitSequence)
        {
            anim.SetTrigger("startFollow");
        }

        if (DoorDetection.hasAICrossed)
        {
            anim.SetBool("backToPlayer", true);
        }

        moveForward.isActivated = moveForwardActive;
        playerMove.isActivated = movePlayer;
        startMenu.EndGame = endGame;
    }
}
