using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public string pieceTag;

    public void PickUp()
    {
        Destroy(gameObject);
    }
}
