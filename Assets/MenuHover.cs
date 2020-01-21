using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MenuHover : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IPointerExitHandler
{
    public Animator anim;

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

    }


}
