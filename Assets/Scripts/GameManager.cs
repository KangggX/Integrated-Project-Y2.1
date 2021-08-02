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

    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

    public void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void CursorUnlock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        //Debug.Log("Resuming Game");
        CursorLock();
        Time.timeScale = 1f;
        isPaused = false;
        UI.SetPauseUI(isPaused);
    }

    public void PauseGame()
    {
        //Debug.Log("Pausing Game");
        CursorUnlock();
        Time.timeScale = 0f;
        isPaused = true;
        UI.SetPauseUI(isPaused);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
