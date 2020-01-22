using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuHover : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IPointerExitHandler
{
    public bool isQuit = false;
    public Animator anim;
    public StartMenu startMenu;

    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetBool("isHighlighted", true);
        anim.SetBool("isMirrored", false);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetBool("isHighlighted", false);
        anim.SetBool("isMirrored", true);
    }

    public void OnSelect(BaseEventData eventData)
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

    private void Update()
    {
        if (startMenu.StartExitTransition)
        {
            anim.SetBool("doTransition", true);
        }
    }


}
