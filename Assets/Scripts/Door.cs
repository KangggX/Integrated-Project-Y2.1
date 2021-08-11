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
    /// <summary>
    /// The Door Animator component that is initialized in Awake() function
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Player component that is initialized in Awake() function
    /// </summary>
    private Player player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    public void Open()
    {
        animator.SetBool("isActivated", true);
    }

    public void OpenAlt()
    {
        if (player.hasKey)
        {
            animator.SetBool("isActivated", true);
        }
    }
}
