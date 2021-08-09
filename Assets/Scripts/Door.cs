/******************************************************************************
Author: Kang Xuan
Name of Class: QuestManager
Description of Class: Script that controls the door animation
Date Created: 09/08/2021
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        animator.SetBool("isActivated", true);
    }
}
