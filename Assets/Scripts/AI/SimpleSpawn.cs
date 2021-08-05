/******************************************************************************
Author: 
Name of Class: SimpleSpawn
Description of Class: Spawning of the enemy at different position
Date Created: 05 Aug 2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpawn : MonoBehaviour
{
    /// <summary>
    /// Spawnpoint transform to where enemy spawn from 
    /// </summary>
    public Transform[] spawnPoints;
    
    /// <summary>
    /// The Enemy gameobject
    /// </summary>
    public GameObject Enemy;

    /// <summary>
    /// The Enemy  start to spawn for the length
    /// </summary>
    public int startSpawnTime = 10;
   
    /// <summary>
    /// The amount of time it spawn
    /// </summary>
    public int spawnTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn",startSpawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// Spawning of the enemy at the spawn points at random 
    /// Instante of enemy at selected random spawn point
    /// </summary>
    
    void Spawn()
    {
        // Find a random index between zero and one less than the number of spawn points.
        int spawnPoints = Random.Range(0, 3);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(Enemy, this.spawnPoints[spawnPoints].position, this.spawnPoints[spawnPoints].rotation);
    }
}
