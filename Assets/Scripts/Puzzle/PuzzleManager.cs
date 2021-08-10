/******************************************************************************
Author: Kang Xuan
Name of Class: PuzzleManager
Description of Class: Manages the puzzle system such as checking whether or not the puzzle is solved
Date Created: 03/08/2021
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    /// <summary>
    /// Arrays to store the puzzle pieces in the object and the state of its completion
    /// </summary>
    public PuzzlePiece[] puzzleArray;
    public bool[] stateChecker = new bool[4];

    /// <summary>
    /// Door variable, to be dragged in from Inspector
    /// </summary>
    public Door door;

    /// <summary>
    /// Variable for the QuestManager class, to be intialized in the Start() function
    /// </summary>
    private QuestManager questManager;

    private void Awake()
    {
        puzzleArray = GetComponentsInChildren<PuzzlePiece>();
    }

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    private void Update()
    {
        for (int i = 0; i < puzzleArray.Length; ++i)
        {
            if (Mathf.Approximately(puzzleArray[i].transform.localEulerAngles.x, 0))
            {
                //Debug.Log(i);
                //Debug.Log(puzzleArray[i].transform.localEulerAngles.x);
                stateChecker[i] = true;
            }
            else
            {
                stateChecker[i] = false;
            }
        }

        if (stateChecker[0] && stateChecker[1] && stateChecker[2] && stateChecker[3]) 
        {
            Complete();
        }
    }

    private void Complete()
    {
        Debug.Log("Puzzle completed");
        questManager.OnValueChange();
        door.Open();
    }
}
