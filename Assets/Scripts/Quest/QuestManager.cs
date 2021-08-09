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

    /// <summary>
    /// The current and required value of the Quest
    /// </summary>
    public int curr;
    public int req;

    /// <summary>
    /// Current index
    /// </summary>
    private int currIndex;

    /// <summary>
    /// Stores the string that is to be sorted
    /// Stores the sorted string in an array that has been split apart
    /// </summary>
    private string toSort;
    private string[] sortedQuest;
    private void Start()
    {
        SortQuest();
    }

    private void Update()
    {
        if (curr == req) // If current value is equals to the required value, do this
        {
            StopAllCoroutines();
            StartCoroutine(QuestComplete());
            curr = 0; //Breaks the loop.
        }
    }

    /// <summary>
    /// Function that sorts the quests in the Quest class.
    /// </summary>
    private void SortQuest()
    {
        if (currIndex <= quest.questList.Length)
        {
            toSort = quest.questList[currIndex];
            sortedQuest = toSort.Split(';'); // Splits the string with ';' being the separator
            curr = int.Parse(sortedQuest[2]);
            req = int.Parse(sortedQuest[3]);
            ++currIndex; // Increment current index by 1
        }

        UpdateUI(true);
    }

    /// <summary>
    /// Function to update the UI in the UIManager with the appropriate wordings.
    /// </summary>
    /// <param name="all"></param>
    public void UpdateUI(bool all)
    {
        if (all)
        {
            StartCoroutine(TypewriterText(sortedQuest[0], $"{sortedQuest[1]}.", $"({sortedQuest[2]}/{sortedQuest[3]})"));
        }
        else
        {
            StartCoroutine(TypewriterText("", "", "Completed!"));
        }
        
    }

    /// <summary>
    /// This updates the QuestUI (weird method, can't think of a better way to do it)
    /// Also increments the current value which is then checked by the Update() function
    /// </summary>
    public void OnValueChange()
    {
        ++curr;
        uIManager.questVal.text = $"({curr}/{req})";
    }

    /// <summary>
    /// Show texts in a typewriter kind of effect instead of just popping out.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="desc"></param>
    /// <returns></returns>
    IEnumerator TypewriterText(string title, string desc, string val)
    {
        if (title != "" && desc != "")
        {
            uIManager.questTitle.text = "";
            uIManager.questDesc.text = "";
            uIManager.questVal.text = "";
            uIManager.questVal.color = Color.white;

            foreach (char letter in title.ToCharArray())
            {
                uIManager.questTitle.text += letter;
                yield return new WaitForSeconds(0.03f);
            }

            yield return new WaitForSeconds(1);

            foreach (char letter in desc.ToCharArray())
            {
                uIManager.questDesc.text += letter;
                yield return new WaitForSeconds(0.03f);
            }

            yield return new WaitForSeconds(0.5f);

            foreach (char letter in val.ToCharArray())
            {
                uIManager.questVal.text += letter;
                yield return new WaitForSeconds(0.03f);
            }
        }
        else
        {
            uIManager.questVal.text = "";
            uIManager.questVal.color = Color.green;

            foreach (char letter in val.ToCharArray())
            {
                uIManager.questVal.text += letter;
                yield return new WaitForSeconds(0.03f);
            }
        }
    }

    /// <summary>
    /// When quest is completed, this coroutine will run the UpdateUI function
    /// and then display the next quest in line using the SortQuest function.
    /// </summary>
    /// <returns></returns>
    IEnumerator QuestComplete()
    {
        UpdateUI(false);

        yield return new WaitForSeconds(3);

        SortQuest();
    }
}
