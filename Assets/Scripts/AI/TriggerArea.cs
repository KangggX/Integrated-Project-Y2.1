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
