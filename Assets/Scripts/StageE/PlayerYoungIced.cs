using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerYoungIced : MonoBehaviour
{
    public int breakpoint;
    Animator animator;
    private enum InputState
    {
        PressA,
        PressD,
        None
    }

    private InputState currentInput = InputState.None;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        breakpoint = 20;
    }

    void Update()
    {
        BreakIce();
    }

    private void BreakIce()
    {
        if (breakpoint > 0)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (currentInput == InputState.None)
                {
                    breakpoint--;
                    currentInput = InputState.PressA;
                }
                else if (currentInput == InputState.PressD)
                {
                    breakpoint--;
                    currentInput = InputState.None;
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (currentInput == InputState.None)
                {
                    breakpoint--;
                    currentInput = InputState.PressD;
                }
                else if (currentInput == InputState.PressA)
                {
                    breakpoint--;
                    currentInput = InputState.None;
                }
            }
        }
        else if (breakpoint <= 0)
        {
            animator.SetBool("isBreak", true);
        }
    }

    public void OnAnimationEnd()
    {
        GameManager.Instance.BreakingIce();
    }
}
