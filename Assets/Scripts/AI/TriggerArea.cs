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
    private Mummy mummy;

    private void Awake()
    {
        if (GetComponentInParent<Mummy>() == null)
        {
            anubis = GetComponentInParent<Anubis>();
        }
        else
        {
            mummy = GetComponentInParent<Mummy>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (mummy == null)
            {
                anubis.playerInRange = !anubis.playerInRange;
            }
            else
            {
                mummy.playerInRange = !mummy.playerInRange;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (mummy == null)
            {
                anubis.playerInRange = !anubis.playerInRange;
            }
            else
            {
                mummy.playerInRange = !mummy.playerInRange;
            }
        }
    }
}
