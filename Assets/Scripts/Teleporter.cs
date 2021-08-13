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
    /// <summary>
    /// The Transform of the GameObject where the player will teleport to
    /// </summary>
    public Transform teleportTarget;

    /// <summary>
    /// The Animator of the FadeIn and FadeOut effect
    /// </summary>
    public Animator transition;

    /// <summary>
    /// Boolean to check if the Teleporter is placed outdoor or indoor
    /// </summary>
    public bool outdoor;

    /// <summary>
    /// To be initialized in Start() function using FindObjectOfType<>()
    /// </summary>
    private GameManager gameManager;
    private UIManager UI;
    private QuestManager questManager;
    private Player player;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        UI = FindObjectOfType<UIManager>();
        questManager = FindObjectOfType<QuestManager>();
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // If player has the jewel, victory
        if (player.hasJewel)
        {
            StartCoroutine(UI.Victory());
        }
        else
        {
            // Transition animation
            StartCoroutine(Transition());

            // To check if the teleporter is placed outdoor or indoor to perform the necessary stuff
            if (outdoor)
            {
                StartCoroutine(gameManager.ChangeEnvironment(outdoor));
                questManager.OnValueChange();

            }
            else
            {
                StartCoroutine(gameManager.ChangeEnvironment(outdoor));
            }
        }
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
