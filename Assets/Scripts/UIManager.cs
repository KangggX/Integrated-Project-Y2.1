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

    public void SetCrosshairText(bool state, string text)
    {
        if (state)
        {
            crosshairTextObject.SetActive(true);
            crosshairText.text = text;
        }
        else
        {
            crosshairTextObject.SetActive(false);
        }
    }
}
