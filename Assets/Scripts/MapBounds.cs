/******************************************************************************
Author: Kang Xuan
Name of Class: MapBounds
Description of Class: This class will kill the player immediately when triggered
Date Created: 23/07/2021
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBounds : MonoBehaviour
{
    /// <summary>
    /// Player class
    /// </summary>
    private Player player;

    private void Start()
    {
        // To initialize the player
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // To kill the player
        player.TakeDamage(100);
    }
}
