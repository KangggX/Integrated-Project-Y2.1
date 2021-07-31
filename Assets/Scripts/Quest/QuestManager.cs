/******************************************************************************
Author: Kang Xuan
Name of Class: QuestManager
Description of Class: Handles the questing system of the game.
Date Created: 31/07/2021
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public UIManager uIManager;
    public Quest quest;
    public string[] sortedQuest;

    private int currIndex;
    private string toSort;
    private char[] separators = new char[] { ';' };
    private void Start()
    {
        Debug.Log(quest.questList[0]);

        SortQuest();
    }

    private void SortQuest()
    {
        if (currIndex <= quest.questList.Length)
        {
            toSort = quest.questList[currIndex];
            sortedQuest = toSort.Split(separators);
            ++currIndex;

            UpdateUI(sortedQuest[0], sortedQuest[1], sortedQuest[2], sortedQuest[3]);
        }
    }

    public void UpdateUI(string title, string objective, string currValue, string requiredValue)
    {
        string desc = $"{objective}. {currValue}/{requiredValue}";
        string[] text = { title, desc };

        StartCoroutine(TypewriterText(text[0], text[1]));
    }

    private void TriggerNextQuest()
    {
        if (sortedQuest[2] == sortedQuest[3])
        {
            SortQuest();
        }
    }

    IEnumerator TypewriterText(string title, string desc)
    {
        uIManager.questTitle.text = "";
        uIManager.questDesc.text = "";

        foreach (char letter in title.ToCharArray())
        {
            uIManager.questTitle.text += letter;
            yield return null;
        }

        yield return new WaitForSeconds(1);

        foreach (char letter in desc.ToCharArray())
        {
            uIManager.questDesc.text += letter;
            yield return null;
        }
    }
}
