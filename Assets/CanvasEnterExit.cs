﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasEnterExit : MonoBehaviour
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
        if (startMenu.StartLevel1)
        {
            anim.SetTrigger("canvasEnter");
        }
    }
}
