/******************************************************************************
Author: Kang Xuan
Name of Class: DialogueTrigger
Description of Class: description
Date Created: 22/07/2021
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private bool hasBeenTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasBeenTriggered)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            hasBeenTriggered = true;
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
