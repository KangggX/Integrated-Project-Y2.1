/******************************************************************************
Author: Elyas Chua-Aziz
Name of Class: VisionField.cs
Description of Class: Controls the behaviour of the vision field attached to the AI
Date Created: 17/07/21
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionField : MonoBehaviour
{
    // When player enters the trigger, tell AI to chase
    // When player leaves the trigger, tell AI to sop chasing

    /// <summary>
    /// Stores the AI that this VisionField should update
    /// </summary>
    [SerializeField]
    private PatrolAI connectedAI;

    private void OnTriggerEnter(Collider other)
    {
        // tell AI to chase
        if(other.tag == "Player")
        {
            // Passes the seen player to the AI via the SeePlayer function
            connectedAI.SeePlayer(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // tell AI to stop
        if (other.tag == "Player")
        {
            // Tells the AI that the player was lost
            connectedAI.LostPlayer();
        }
    }
}
