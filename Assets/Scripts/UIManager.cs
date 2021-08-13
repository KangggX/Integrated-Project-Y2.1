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

    /// <summary>
    /// Player UI crosshair settings
    /// </summary>
    [Header("Player UI - Crosshair")]
    public GameObject crosshairTextObject;
    public GameObject crosshairIcon1;
    public GameObject crosshairIcon2;
    public TextMeshProUGUI crosshairText;

    /// <summary>
    /// Player UI related to the Quest UI
    /// </summary>
    [Header("Player UI - Quest")]
    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI questDesc;
    public TextMeshProUGUI questVal;

    /// <summary>
    /// Pause menu UI
    /// </summary>
    [Header("Player UI - Pause Menu")]
    public GameObject pauseUI;
    public GameObject optionsUI;

    /// <summary>
    /// Adjustable Dialogue UI components, must be dragged in from Inspector.
    /// </summary>
    [Header("Dialogue UI")]
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueName;
    public TextMeshProUGUI dialogueSentence;

    [Header("Misc")]
    /// <summary>
    /// The Animator of the FadeIn and FadeOut effect
    /// </summary>
    public Animator transition;
    public Animator damaged;
    public GameObject stun;
    public TextMeshProUGUI deathText;
    public TextMeshProUGUI victoryText;
    private GameManager gameManager;

    private void Awake()
    {
        crosshairTextObject.SetActive(false);
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
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
    public void SetCrosshairText(bool state, string text, int type)
    {
        if (state)
        {
            crosshairTextObject.SetActive(true);
            crosshairText.text = text;

            if (type == 1)
            {
                crosshairIcon1.SetActive(true);
                crosshairIcon2.SetActive(false);
            }
            else if (type == 2)
            {
                crosshairIcon1.SetActive(false);
                crosshairIcon2.SetActive(true);
            }
        }
        else
        {
            crosshairTextObject.SetActive(false);
            crosshairIcon1.SetActive(false);
            crosshairIcon2.SetActive(false);
        }
    }

    /// <summary>
    /// To show or hide the Pause Menu
    /// </summary>
    /// <param name="isPaused"></param>
    public void SetPauseUI(bool isPaused)
    {
        if (isPaused && !pauseUI.activeInHierarchy)
        {
            pauseUI.SetActive(true);
        }
        else
        {
            pauseUI.SetActive(false);
        }
    }

    /// <summary>
    /// Function to open the options menu
    /// </summary>
    public void OpenOptions()
    {
        optionsUI.SetActive(true);
        pauseUI.SetActive(false);
    }

    /// <summary>
    /// Function to close the options menu
    /// </summary>
    public void CloseOptions()
    {
        optionsUI.SetActive(false);
        pauseUI.SetActive(true);
    }

    /// <summary>
    /// Coroutine when player is victorious
    /// </summary>
    /// <returns></returns>
    public IEnumerator Victory()
    {
        transition.SetBool("isEnabled", true);

        yield return new WaitForSeconds(3);

        victoryText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2);

        gameManager.BackToMenu();
    }

    /// <summary>
    /// Coroutine when player dies
    /// </summary>
    /// <returns></returns>
    public IEnumerator Die()
    {
        transition.SetBool("isEnabled", true);

        yield return new WaitForSeconds(3);

        deathText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2);

        gameManager.BackToMenu();
    }
}
