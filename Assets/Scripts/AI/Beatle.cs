/******************************************************************************
Author: Elyas Chua-Aziz
Name of Class: Anubis
Description of Class: Controls the behaviour of the Beatle AI.
Date Created: 17/07/21
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Beatle : MonoBehaviour
{
    /// <summary>
    /// This stores the current and next state of the AI
    /// </summary>
    public string currentState;
    public string nextState;

    /// <summary>
    /// Adjustable variable for the AI movement speed and to store the movement speed
    /// </summary>
    public float moveSpeed;
    private float storedMoveSpeed;

    /// <summary>
    /// The time that the AI will idle for before patrolling
    /// </summary>
    [SerializeField]
    private float idleTime;

    /// <summary>
    /// The NavMeshAgent component attached to the gameobject
    /// </summary>
    private NavMeshAgent agentComponent;

    /// <summary>
    /// The current player that is being seen by the AI
    /// </summary>
    private Transform playerToChase;

    /// <summary>
    /// The damage of this AI
    /// </summary>
    public int damage;

    private bool playerInRange;
    private bool canAttack = true;
    private Player player;

    private void Awake()
    {
        // Get the attached NavMeshAgent and store it in agentComponent
        agentComponent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        // Set the starting state as Idle
        nextState = "ChasingPlayer";

        // Find the player object
        player = FindObjectOfType<Player>();
        playerToChase = player.transform;
    }

    private void Update()
    {
        if (playerInRange && canAttack)
        {
            StartCoroutine(AttackCooldown());
        }

        // Set the speed of the AI
        agentComponent.speed = moveSpeed;

        // Check if the AI should change to a new state
        if (nextState != currentState)
        {
            // Stop the current running coroutine first before starting a new one.
            StopCoroutine(currentState);
            currentState = nextState;
            StartCoroutine(currentState);
        }
    }

    /// <summary>
    /// Used to tell the AI that it sees a player
    /// </summary>
    /// <param name="seenPlayer">The player that was seen</param>
    public void SeePlayer(Transform seenPlayer)
    {
        // Store the seen player and change the state of the AI
        playerToChase = seenPlayer;
        nextState = "ChasingPlayer";
    }

    /// <summary>
    /// Used to tell the AI that it lost the player
    /// </summary>
    public void LostPlayer()
    {
        // Set the seen player to null
        playerToChase = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    /// <summary>
    /// The behaviour of the AI when in the ChasingPlayer state
    /// </summary>
    /// <returns></returns>
    IEnumerator ChasingPlayer()
    {
        while (currentState == "ChasingPlayer")
        {
            // This while loop will contain the ChasingPlayer behaviour

            yield return null;

            // If there is a player to chase, keep chasing the player
            if (playerToChase != null)
            {
                agentComponent.SetDestination(playerToChase.position);
            }
            // If not, move back to the Idle state
            else
            {
                nextState = "Idle";
            }
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;

        yield return new WaitForSeconds(2.1f);

        player.TakeDamage(damage);
        canAttack = true;
    }
}
