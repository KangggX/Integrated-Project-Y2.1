/******************************************************************************
Author: Kang Xuan
Name of Class: PuzzlePiece
Description of Class: For each individual puzzle pieces, rotates the piece when interacted
Date Created: 03/08/2021
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    private void Awake()
    {
        gameObject.transform.Rotate(Random.Range(0, 4) * 90, 0, 0); // Randomize the rotation of this piece to 0, 90, 180, or 270 degrees upon starting
    }

    /// <summary>
    /// Rotates the puzzle piece by 90 degrees
    /// </summary>
    public void RotatePiece()
    {
        gameObject.transform.Rotate(90, 0, 0);
    }
}
