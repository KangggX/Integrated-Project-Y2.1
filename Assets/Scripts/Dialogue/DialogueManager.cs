/******************************************************************************
Author: Kang Xuan
Name of Class: DialogueManager
Description of Class: description
Date Created: 22/07/2021
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    /// <summary>
    /// UI Manager, must be dragged in from Inspector.
    /// </summary>
    public UIManager uIManager;

    /// <summary>
    /// Player, must be dragged in from Inspector.
    /// </summary>
    public Player player;
    
    private Queue<string> sentences;

    // Start is called before the first frame update
    private void Start()
    {
        sentences = new Queue<string>();
    }

    /// <summary>
    /// Enqueuing(adding) every sentence from dialogue to a Queue sentences.
    /// Enable the Dialogue UI.
    /// </summary>
    /// <param name="dialogue"></param>
    public void StartDialogue(Dialogue dialogue)
    {
        //Debug.Log("Staring conversation with " + dialogue.name);
        uIManager.dialogueUI.SetActive(true);
        sentences.Clear(); //Clear sentences from previous dialogue.

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    /// <summary>
    /// Function to display the next line of sentence of the dialogue.
    /// If the number of sentences left == 0, then call the EndDialogue function
    /// </summary>
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();

            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypewriterText(sentence));
        ///Debug.Log(sentence);
    }


    /// <summary>
    /// Function to end the current dialogue when called.
    /// Set boolean 'inDialogue' to false.
    /// Disables the Dialogue UI.
    /// </summary>
    private void EndDialogue()
    {
        Debug.Log("End of dialogue");
        player.inDialogue = false;
        uIManager.dialogueUI.SetActive(false);
    }

    IEnumerator TypewriterText(string sentence)
    {
        uIManager.dialogueSentence.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            uIManager.dialogueSentence.text += letter;
            yield return null;
        }
    }
}
