/******************************************************************************
Author: Kang Xuan
Name of Class: Chest
Description of Class: Controls the opening animation of the chest
Date Created: 11/08/21
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator animator; // Gets the GameObject Animator component

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        gameObject.layer = 0; // To ensure that the raycast does not detect it anymore once opened

        animator.SetBool("isOpened", true); // Plays the chest opening animation
    }
}
