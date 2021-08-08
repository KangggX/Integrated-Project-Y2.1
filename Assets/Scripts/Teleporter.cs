using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Player player;
    public Transform teleportTarget;
    public Animator transition;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        gameManager.PlayerLock();
        transition.SetBool("isEnabled", true);

        yield return new WaitForSeconds(3);

        player.transform.position = teleportTarget.position;

        yield return new WaitForSeconds(1);

        gameManager.PlayerUnlock();
        transition.SetBool("isEnabled", false);
    }
}
