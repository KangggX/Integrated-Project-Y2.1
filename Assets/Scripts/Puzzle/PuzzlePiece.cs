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
    private QuestManager questManager;

    private void Awake()
    {
        gameObject.transform.Rotate(Random.Range(0, 4) * 90, 0, 0);
    }

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    public void PickUp()
    {
        ++questManager.curr;
        questManager.OnValueChange();
        Destroy(gameObject);
    }

    public void RotatePiece()
    {
        gameObject.transform.Rotate(90, 0, 0);
    }
}
