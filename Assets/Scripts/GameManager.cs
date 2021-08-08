/******************************************************************************
Author: Kang Xuan
Name of Class: GameManager
Description of Class: Handles the overall game such as pausing.
Date Created: 29/07/2021
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager UI;
    public Player player;

    private bool isPaused;

    /// <summary>
    /// Stored variables from the player class.
    /// To be used to revert modifications made to default variables.
    /// </summary>
    private int storedRaycastLength;
    private float storedMoveSpeed;
    private float storedRotationSpeed;

    private void Awake()
    {
        // To store the player default Raycast, MoveSpeed, RotationSpeed, and MoveSpeedMultiplier value.
        storedRaycastLength = player.raycastLength;
        storedMoveSpeed = player.moveSpeed;
        storedRotationSpeed = player.rotationSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        UIManager uI = FindObjectOfType<UIManager>();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    /// <summary>
    /// Locks the player cursor.
    /// </summary>
    public void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// Unlocks the player cursor.
    /// </summary>
    public void CursorUnlock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// Locks the player cursor, movement and rotation.
    /// </summary>
    public void PlayerLock()
    {
        // Set player movement and rotation to 0.
        player.raycastLength = 0;
        player.moveSpeed = 0;
        player.rotationSpeed = 0;
        Debug.Log(player.rotationSpeed);
    }

    /// <summary>
    /// Unlocks the player cursor, movement and rotation.
    /// </summary>
    public void PlayerUnlock()
    {
        // Set player movement and rotation to it default value.
        player.raycastLength = storedRaycastLength;
        player.moveSpeed = storedMoveSpeed;
        player.rotationSpeed = storedRotationSpeed;
    }

    /// <summary>
    /// Resumes the game.
    /// </summary>
    public void ResumeGame()
    {
        //Debug.Log("Resuming Game");
        CursorLock();
        Time.timeScale = 1f;
        isPaused = false;
        UI.SetPauseUI(isPaused);
    }

    /// <summary>
    /// Pauses the game.
    /// </summary>
    public void PauseGame()
    {
        //Debug.Log("Pausing Game");
        CursorUnlock();
        Time.timeScale = 0f;
        isPaused = true;
        UI.SetPauseUI(isPaused);
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
