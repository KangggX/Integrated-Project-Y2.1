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
    public PuzzlePiece[] puzzleArray;
    public bool[] stateChecker = new bool[4];

    private void Awake()
    {
        puzzleArray = GetComponentsInChildren<PuzzlePiece>();
    }

    private void Update()
    {
        for (int i = 0; i < puzzleArray.Length; ++i)
        {
            if (puzzleArray[i].transform.eulerAngles.z == 0)
            {
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
    }
}
