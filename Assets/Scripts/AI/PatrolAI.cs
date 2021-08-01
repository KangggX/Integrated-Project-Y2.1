/******************************************************************************
Author: Elyas Chua-Aziz
Name of Class: PatrolAI.cs
Description of Class: Controls the behaviour of the patrolling AI.
Date Created: 17/07/21
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAI : MonoBehaviour
{
    // Have an Idle and a Patrolling state
    // In Idle state, it will stand still for a few seconds, before changing to Patrolling
    // In Patrolling state, it will move towards a defined checkpoint.
    // After it reaches the checkpoint, go back to Idle state.

    /// <summary>
    /// This stores the current state that the AI is in
    /// </summary>
    public string currentState;

    /// <summary>
    /// This stores the next state that the AI should transition to
    /// </summary>
    public string nextState;

    public float moveSpeed;

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
    /// The array holding the checkpoints
    /// </summary>
    [SerializeField]
    private Transform[] checkpoints;

    /// <summary>
    /// Used as the index to access from the checkpoints array
    /// </summary>
    private int currentCheckpoint;

    /// <summary>
    /// The current player that is being seen by the AI
    /// </summary>
    private Transform playerToChase;

    public Animator animator;

    private void Awake()
    {
        // Get the attached NavMeshAgent and store it in agentComponent
        agentComponent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // Set the starting state as Idle
        nextState = "Idle";
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the AI should change to a new state
        if(nextState != currentState)
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

    /// <summary>
    /// The behaviour of the AI when in the Idle state
    /// </summary>
    /// <returns></returns>
    IEnumerator Idle()
    {
        //animator.SetBool("isWalking", false);
        //animator.SetBool("isAttacking", false);

        while (currentState == "Idle")
        {
            // This while loop will contain the Idle behaviour

            
            
            // The AI will wait for a few seconds before continuing.
            yield return new WaitForSeconds(idleTime);

            // Change to Patrolling state.
            nextState = "Patrolling";
        }
    }

    /// <summary>
    /// The behaviour of the AI when in the Patrolling state
    /// </summary>
    /// <returns></returns>
    IEnumerator Patrolling()
    {
        // Set the speed of the AI
        agentComponent.speed = moveSpeed;

        // Set the checkpoint that this AI should move towards
        agentComponent.SetDestination(checkpoints[currentCheckpoint].position);
        bool hasReached = false;
       

        while (currentState == "Patrolling")
        {
            // This while loop will contain the Patrolling behaviour
            

            yield return null;
            if(!hasReached)
            {
                // If agent has not reached destination, do the following code
                animator.SetBool("isWalking", true);


                // Check that the agent is at an acceptable stopping distance from the destination
                if (agentComponent.remainingDistance <= agentComponent.stoppingDistance)
                {
                    // We want to make sure this only happens once.
                    hasReached = true;
                    // Change back to Idle state.
                    nextState = "Idle";
                    // Increase the index to retrieve from the checkpoints array
                    ++currentCheckpoint;

                    // A check so that the index does not exceed the length of the checkpoints array
                    if(currentCheckpoint >= checkpoints.Length)
                    {
                        currentCheckpoint = 0;
                    }
                }
            }
        }
    }

    /// <summary>
    /// The behaviour of the AI when in the ChasingPlayer state
    /// </summary>
    /// <returns></returns>
    IEnumerator ChasingPlayer()
    {
        while(currentState == "ChasingPlayer")
        {
            // This while loop will contain the ChasingPlayer behaviour

            yield return null;
               
            // If there is a player to chase, keep chasing the player
            if(playerToChase != null)
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
}
