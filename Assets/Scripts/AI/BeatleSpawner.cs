/******************************************************************************
Author: Kang Xuan
Name of Class: BeatleSpawner
Description of Class: Spawns beatles at gameObject transform position
Date Created: 09/08/2021
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatleSpawner : MonoBehaviour
{
    public GameObject beatle;
    public bool isActivated;

    private int currIndex;

    private void Update()
    {
        Spawn();
    }

    private void Spawn()
    {
        while (isActivated && currIndex < 10)
        {
            Instantiate(beatle, this.transform);
            ++currIndex;
        }
    }
}
