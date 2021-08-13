/******************************************************************************
Author: Kang Xuan
Name of Class: GameManager
Description of Class: Handles the overall game such as pausing.
Date Created: 29/07/2021
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Game's main lighting
    /// </summary>
    public Light directionalLight;

    /// <summary>
    /// To be initialized in Start() function
    /// </summary>
    private UIManager UI;
    private Player player;

    /// <summary>
    /// Skybox Material
    /// </summary>
    private Material skybox;

    /// <summary>
    /// Boolean to check if game is paused or not
    /// </summary>
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
        skybox = RenderSettings.skybox;
    }

    private void Start()
    {
        // Initializing UI and player variable
        UI = FindObjectOfType<UIManager>();
        player = FindObjectOfType<Player>();

        // To store the player default Raycast, MoveSpeed, RotationSpeed, and MoveSpeedMultiplier value.
        storedRaycastLength = player.raycastLength;
        storedMoveSpeed = player.moveSpeed;
        storedRotationSpeed = player.rotationSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
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

    /// <summary>
    /// Brings the player back to the main menu
    /// </summary>
    public void BackToMenu()
    {
        CursorUnlock(); // Unlocks the cursor
        PlayerUnlock(); // Unlocks the player
        Time.timeScale = 1f; // Set time back to normal
        SceneManager.LoadScene(0); // Load main menu
    }

    /// <summary>
    /// Change the Environment of the game
    /// </summary>
    public IEnumerator ChangeEnvironment(bool dark)
    {
        if (dark)
        {
            yield return new WaitForSeconds(3); // Wait for 3 seconds before changing, synchronizing with the player teleporting

            RenderSettings.skybox = null; // Change skybox to none
            RenderSettings.sun = null; // Disable the sun
            directionalLight.gameObject.SetActive(false); // Disable the sun
        }
        else
        {
            yield return new WaitForSeconds(3); // Wait for 3 seconds before changing, synchronizing with the player teleporting

            RenderSettings.skybox = skybox; // Change skybox back to default
            RenderSettings.sun = directionalLight; // Enable the sun
            directionalLight.gameObject.SetActive(true); // Enable the sun
        }
    }
}
