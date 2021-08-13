/******************************************************************************
Author: Kang Xuan
Name of Class: Collectible
Description of Class: To be placed in each collectibles, mainly the Hand Torch and the Weapon.
                      Updates the Quest and Player stats (if applicable) when picked up
Date Created: 09/08/2021
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    /// <summary>
    /// Variable to store the Player and UIManager class
    /// Values to be inserted at the Start() function
    /// </summary>
    private Player player;
    private QuestManager questManager;
    private BeatleSpawner beatleSpawner;

    /// <summary>
    /// The item type of THIS collectible, either handTorch or weapon
    /// </summary>
    private string itemType;

    private void Awake()
    {
        itemType = gameObject.name;
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        questManager = FindObjectOfType<QuestManager>();
        beatleSpawner = FindObjectOfType<BeatleSpawner>();
    }

    public void PickUp()
    {
        if (itemType == "handTorch")
        {
            player.handTorch.SetActive(true);
        }
        else if (itemType == "weapon")
        {
            player.weapon.SetActive(true);
        }
        else if (itemType == "Keystone")
        {
            player.hasKey = true;
        }
        else if (itemType == "jewel")
        {
            player.hasJewel = true;
            beatleSpawner.isActivated = true;
        }

        questManager.OnValueChange();
        Destroy(gameObject);
    }
}
