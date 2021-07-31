/******************************************************************************
Author: Kang Xuan
Name of Class: UIManager
Description of Class: Handles the overall UI of the game.
Date Created: 27/07/2021
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// Adjustable Player UI components, must be dragged in from Inspector.
    /// </summary>
    [Header("Player UI - Statistics")]
    public Slider healthSlider;
    public Slider staminaSlider;

    [Header("Player UI - Crosshair")]
    public GameObject crosshairTextObject;
    public TextMeshProUGUI crosshairText;

    [Header("Player UI - Quest")]
    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI questDesc;

    [Header("Player UI - Pause Menu")]
    public GameObject pauseUI;

    /// <summary>
    /// Adjustable Dialogue UI components, must be dragged in from Inspector.
    /// </summary>
    [Header("Dialogue UI")]
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueName;
    public TextMeshProUGUI dialogueSentence;

    private void Awake()
    {
        crosshairTextObject.SetActive(false);
    }

    // Function to be used to update the slider value of the health UI based on player health.
    public void SetHealth(float health)
    {
        healthSlider.value = health;
    }

    // Function to set the max slider and slider value of the health UI based on the allocated max player health.
    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    // Function to be used to update the slider value of the stamina UI based on player stamina.
    public void SetStamina(float stamina)
    {
        staminaSlider.value = stamina;
    }

    // Function to set the max slider and slider value of the stamina UI based on the allocated max player stamina.
    public void SetMaxStamina(float stamina)
    {
        staminaSlider.maxValue = stamina;
        staminaSlider.value = stamina;
    }

    /// <summary>
    /// When hovering over interactable object, players will need to be notified what they can do with it.
    /// Thus, this function would show the text below the crosshair with the necessary message such as "Interact" or "Collect".
    /// </summary>
    /// <param name="state"></param>
    /// <param name="text"></param>
    public void SetCrosshairText(bool state, string text)
    {
        if (state)
        {
            crosshairTextObject.SetActive(true);
            crosshairText.text = text;
            //crosshairText.alpha = 255;
        }
        else
        {
            crosshairTextObject.SetActive(false);
            //crosshairText.alpha = 0;
        }
    }

    public void SetPauseUI(bool isPaused)
    {
        if (isPaused)
        {
            pauseUI.SetActive(true);
        }
        else
        {
            pauseUI.SetActive(false);
        }
    }
}
