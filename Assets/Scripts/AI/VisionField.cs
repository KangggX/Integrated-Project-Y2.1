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
    private GameObject[] connectedAI;

    private Mummy mummy;
    private Anubis anubis;

    private void Awake()
    {
        if (connectedAI[0].GetComponent<Mummy>() == null)
        {
            anubis = connectedAI[0].GetComponent<Anubis>();
        }
        else
        {
            mummy = connectedAI[0].GetComponent<Mummy>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // tell AI to chase
        if(other.tag == "Player")
        {
            if (mummy == null)
            {
                // Passes the seen player to the AI via the SeePlayer function
                //anubis.SeePlayer(other.transform);

                foreach (GameObject i in connectedAI)
                {
                    i.GetComponent<Anubis>().SeePlayer(other.transform);
                }
            }
            else
            {
                // Passes the seen player to the AI via the SeePlayer function
                //mummy.SeePlayer(other.transform);

                foreach (GameObject i in connectedAI)
                {
                    i.GetComponent<Mummy>().SeePlayer(other.transform);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // tell AI to stop
        if (other.tag == "Player")
        {
            if (mummy == null)
            {
                // Tells the AI that the player was lost
                //anubis.LostPlayer();

                foreach (GameObject i in connectedAI)
                {
                    i.GetComponent<Anubis>().LostPlayer();
                }
            }
            else
            {
                /// Tells the AI that the player was lost
                //mummy.LostPlayer();

                foreach (GameObject i in connectedAI)
                {
                    i.GetComponent<Mummy>().LostPlayer();
                }
            }
        }
    }
}
