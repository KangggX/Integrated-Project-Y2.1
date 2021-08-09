/******************************************************************************
Author: Elyas Chua-Aziz
Name of Class: Anubis
Description of Class: Controls the behaviour of the Anubis AI.
Date Created: 17/07/21
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Anubis : MonoBehaviour
{
    // Have an Idle and a Patrolling state
    // In Idle state, it will stand still for a few seconds, before changing to Patrolling
    // In Patrolling state, it will move towards a defined checkpoint.
    // After it reaches the checkpoint, go back to Idle state.

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

    /// <summary>
    /// These variables controls the maxinum health, as well as controlling
    /// the healthbar position and the necessary prefab for the healthbar
    /// </summary>
    public float maxHealth;
    public Canvas HealthPrefab;
    public float healthBarPosition;

    

    public int damage;

    private float health;
    public bool playerInRange;
    private bool canAttack = true;
    private bool isActivated;
    private bool isEnraged;
    private Canvas theCanvas;
    private Slider healthBar;
    private Player player;
    private Animator animator;

    private void Awake()
    {
        // Get the attached NavMeshAgent and store it in agentComponent
        agentComponent = GetComponent<NavMeshAgent>();

        // Get the attached Animator and store it in animator
        animator = GetComponent<Animator>();

        health = maxHealth;

        // Stores the movement speed to storedMoveSpeed
        storedMoveSpeed = moveSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set the starting state as Idle
        nextState = "Idle";

        // Find the player object
        player = FindObjectOfType<Player>();

        theCanvas = Instantiate(HealthPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + healthBarPosition, gameObject.transform.position.z), transform.rotation, gameObject.transform);
        healthBar = theCanvas.GetComponentInChildren<Slider>();
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = health;

        if (health <= 0)
        {
            nextState = "Die";
        }

        if (health <= 180 && !isEnraged)
        {
            isEnraged = true;
            nextState = "Enraged";
        }

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

    /// <summary>
    /// Function that can be called in another class to deal damage to THIS specific AI
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
        }
    }

    /// <summary>
    /// The behaviour of the AI when in the Idle state
    /// </summary>
    /// <returns></returns>
    IEnumerator Idle()
    {
        while (currentState == "Idle")
        {
            // This while loop will contain the Idle behaviour
            animator.SetBool("isWalking", false);
            animator.SetBool("isAttacking", false);

            // To 100% ensure that the AI can't move (bugfix)
            moveSpeed = 0;

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
        // Change the movement speed back to its default value
        moveSpeed = storedMoveSpeed;

        // Set the checkpoint that this AI should move towards
        agentComponent.SetDestination(checkpoints[currentCheckpoint].position);
        bool hasReached = false;


        while (currentState == "Patrolling")
        {
            // This while loop will contain the Patrolling behaviour


            yield return null;
            if (!hasReached)
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
                    if (currentCheckpoint >= checkpoints.Length)
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
        // Change the movement speed back to its default value
        moveSpeed = storedMoveSpeed;

        while (currentState == "ChasingPlayer")
        {
            // This while loop will contain the ChasingPlayer behaviour

            yield return null;

            // If there is a player to chase, keep chasing the player
            if (playerToChase != null)
            {
                agentComponent.SetDestination(playerToChase.position);

                if (agentComponent.remainingDistance > agentComponent.stoppingDistance)
                {
                    animator.SetBool("isWalking", true);
                }
                else if (agentComponent.remainingDistance <= agentComponent.stoppingDistance)
                {
                    Debug.Log("Hi");
                    animator.SetBool("isWalking", false);
                }
            }
            // If not, move back to the Idle state
            else
            {
                nextState = "Idle";
            }
        }
    }

    IEnumerator Enraged()
    {
        while (currentState == "Enraged")
        {
            animator.SetBool("isEnraged", true);
            animator.SetTrigger("isActivated");

            yield return new WaitForSeconds(3.5f);

            nextState = "Idle";
        }
    }

    IEnumerator Die()
    {
        while (currentState == "Die")
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isAttacking", false);
            animator.SetTrigger("isDead");

            yield return new WaitForSeconds(4);

            Destroy(gameObject);
        }
    }

    IEnumerator AttackCooldown()
    {
        animator.SetBool("isAttacking", true);
        moveSpeed = 0;
        canAttack = false;

        yield return new WaitForSeconds(1.1f);

        player.TakeDamage(damage);
        moveSpeed = storedMoveSpeed;
        canAttack = true;
    }
}
