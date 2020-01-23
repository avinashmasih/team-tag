using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextCanvas : MonoBehaviour
{
    public GameObject gameplayUI;
    public StartMenu startMenu;
    public DeltaQueueDisplay deltaQueueDisplay;
    public DeltaQueue deltaQueue;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pointerPosition;
        bool sprayed;

        if (WiimoteInput.isConnected)
        {
            Vector2 wiiPointerPos = WiimoteInput.GetPointerPosition();
            pointerPosition = new Vector3(wiiPointerPos.x, wiiPointerPos.y, 0f);
            sprayed = WiimoteInput.isSprayButtonDownThisFrame();

        }
        else
        {
            pointerPosition = Input.mousePosition;
            sprayed = Input.GetButtonDown("Spray");

        }

        Vector2 sprayScreenPos = new Vector2(pointerPosition.x, pointerPosition.y);


        RectTransform rectTransform = GetComponent<RectTransform>();
        bool isHovering = RectTransformUtility.RectangleContainsScreenPoint(rectTransform, sprayScreenPos);

        Highlight(isHovering);

        if (isHovering && sprayed)
        {

            SelectUI();

        }
    }

    private void Highlight(bool i_highlight)
    {
        anim.SetBool("isHighlighted", i_highlight);
        anim.SetBool("isMirrored", !i_highlight);
    }

    private void SelectUI()
    {

        if (deltaQueue.queueCount == 0 && deltaQueueDisplay.isDone)
        {
            startMenu.StartExitSequence = true;
            gameplayUI.SetActive(false);
        }
        else
        {
            deltaQueue.MoveOn();
        }




    }
}
