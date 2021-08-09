/******************************************************************************
Author: Kang Xuan
Name of Class: Teleporter
Description of Class: Teleports the player to a specified location when triggered
Date Created: 29/07/2021
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Player player;
    public Transform teleportTarget;
    public Animator transition;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        gameManager.PlayerLock();
        transition.SetBool("isEnabled", true);

        yield return new WaitForSeconds(3);

        player.transform.position = teleportTarget.position;

        yield return new WaitForSeconds(1);

        gameManager.PlayerUnlock();
        transition.SetBool("isEnabled", false);
    }
}
