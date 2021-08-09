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

        questManager.OnValueChange();
        Destroy(gameObject);
    }
}
