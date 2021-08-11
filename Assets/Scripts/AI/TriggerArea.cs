/******************************************************************************
Author: Kang Xuan
Name of Class: TriggerArea
Description of Class: Manages the trigger area of the AI.
Date Created: 09/08/21
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    private Anubis anubis;

    private void Awake()
    {
        anubis = transform.GetComponentInParent<Anubis>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anubis.playerInRange = !anubis.playerInRange;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anubis.playerInRange = !anubis.playerInRange;
        }
    }
}
