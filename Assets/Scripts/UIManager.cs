/******************************************************************************
Author: Kang Xuan

Name of Class: UIManager

Description of Class: Handles the overall UI of the game.

Date Created: 27/07/2021
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// Adjustable Player UI components, must be dragged in from Inspector.
    /// </summary>
    [Header("Player UI Settings")]
    public GameObject crosshairTextObject;
    public TextMeshProUGUI crosshairText;

    /// <summary>
    /// Adjustable Dialogue UI components, must be dragged in from Inspector.
    /// </summary>
    [Header("Dialogue UI Settings")]
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueName;
    public TextMeshProUGUI dialogueSentence;

    private void Awake()
    {
        crosshairTextObject.SetActive(false);
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
}
