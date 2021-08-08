using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public Transform[] puzzleArray;

    public int curr;
    public bool[] stateChecker = new bool[4];

    private void Start()
    {
    }

    private void Update()
    {
        for (int i = 0; i < puzzleArray.Length; ++i)
        {
            if (puzzleArray[i].eulerAngles.z == 0)
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
