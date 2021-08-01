using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public string pieceTag;
    public QuestManager questManager;

    public void PickUp()
    {
        ++questManager.curr;
        questManager.OnValueChange();
        Destroy(gameObject);
    }
}
