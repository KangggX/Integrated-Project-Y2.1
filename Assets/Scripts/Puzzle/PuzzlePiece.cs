using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public string pieceTag;
    public QuestManager questManager;

    private void Start()
    {
        gameObject.transform.Rotate(0, 0, Random.Range(0, 4) * 90);
    }

    public void PickUp()
    {
        ++questManager.curr;
        questManager.OnValueChange();
        Destroy(gameObject);
    }

    public void RotatePiece()
    {
        gameObject.transform.Rotate(0, 0, 90);
    }
}
