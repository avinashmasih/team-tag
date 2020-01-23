using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuHover : MonoBehaviour
{
    public bool isQuit = false;
    public Animator anim;
    public StartMenu startMenu;

    private void Update()
    {
        Vector3 pointerPosition = Input.mousePosition;
        Vector2 sprayScreenPos  = new Vector2(pointerPosition.x, pointerPosition.y);
        bool sprayed            = Input.GetButtonDown("Spray");
        

        RectTransform rectTransform = GetComponent<RectTransform>();
        bool isHovering             = RectTransformUtility.RectangleContainsScreenPoint(rectTransform, sprayScreenPos);

        Highlight(isHovering);

        if (isHovering && sprayed)
        {

            SelectUI();

        }


        if (startMenu.StartExitTransition)
        {

            anim.SetBool("doTransition", true);

        }
    }



    private void Highlight(bool i_highlight)
    {
        anim.SetBool("isHighlighted", i_highlight);
        anim.SetBool("isMirrored", !i_highlight);
    }

    private void SelectUI()
    {
        if (isQuit)
        {
            Application.Quit();
        }
        else
        {
            startMenu.StartExitTransition = true;
        }
    }

}
