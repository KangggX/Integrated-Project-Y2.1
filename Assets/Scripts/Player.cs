/******************************************************************************
Author: Kang Xuan
Name of Class: Player
Description of Class: This class will control the movement and actions of a 
                      player avatar based on user input.
Date Created: 23/07/2021
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Player settings to adjust speed, stamina and rate of stamina regen, 
    /// as well as the length of the raycast.
    /// </summary>
    [Header("Player Settings")]
    public int damage;
    public float moveSpeed;
    public float storedMoveSpeed;
    public float moveSpeedMultiplier;
    public float maxHealth;
    public float maxStamina;
    public float staminaRegenRate;
    public float staminaDepleteRate;
    public float staminaTimeToRegen;
    public float healthTimeToRegen;
    public int raycastLength;

    /// <summary>
    /// To store the Player's current health and stamina
    /// </summary>
    private float health;
    private float stamina;

    /// <summary>
    /// To store the time for the health and stamina regen
    /// </summary>
    private float healthRegenTimer;
    private float staminaRegenTimer;

    /// <summary>
    /// Player animator
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Player inventory settings (shortcut)
    /// </summary>
    [Header("Player Inventory")]
    public GameObject handTorch;
    public GameObject weapon;
    public bool hasKey;

    /// <summary>
    /// The camera attached to the player model.
    /// Should be dragged in from Inspector.
    /// Settings can be played around.
    /// </summary>
    [Header("Camera Settings")]
    public Camera playerCamera;
    public float rotationSpeed;

    /// <summary>
    /// UI Settings that must be dragged in from Inspector.
    /// </summary>
    [Header("Manager Settings")]
    private UIManager uIManager;
    private GameManager gameManager;

    /// <summary>
    /// Coroutine variable for player movement
    /// </summary>
    private string currentState;
    private string nextState;
    private float storedMoveSpeedMultiplier;

    private void Awake()
    {
        // Set the health and stamina of the player based on the max values of each variable.
        health = maxHealth;
        stamina = maxStamina;

        // Store the movement speed and movement speed multiplier
        storedMoveSpeed = moveSpeed;
        storedMoveSpeedMultiplier = moveSpeedMultiplier;
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Initializing GameManager and UIManager using FindObjectOfType<>()
        gameManager = FindObjectOfType<GameManager>();
        uIManager = FindObjectOfType<UIManager>();

        // Getting the Player animator component
        animator = GetComponent<Animator>();

        // To ensure that the cursor is not visible when player is playing the game.
        gameManager.CursorLock();

        // Set the UI of the max health and max stamina of the player.
        uIManager.SetMaxHealth(health);
        uIManager.SetMaxStamina(stamina);

        // So that player will start the game in the Idle state.
        nextState = "Idle";
    }

    // Update is called once per frame
    private void Update()
    {
        if (nextState != currentState)
        {
            SwitchState();
        }

        // Change the UI of the health and stamina over time based on their current values.
        uIManager.SetHealth(health);
        uIManager.SetStamina(stamina);

        // Functions to check if player is rotating, sprinting or in a dialogue
        CheckRotation();
        CheckHealth();
        CheckSprint();
        CheckWeapon();

        // Handles the raycast of the player
        Raycast();
    }

    /// <summary>
    /// Public function to enable player to adjust rotation in settings
    /// </summary>
    /// <param name="speed"></param>
    public void Rotation(float speed)
    {
        rotationSpeed = speed;
    }

    /// <summary>
    /// Function called from AIs to deal damage to player
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        uIManager.damaged.SetTrigger("isDamaged");
        health -= damage;
    }

    /// <summary>
    /// Sets the current state of the player
    /// and starts the correct coroutine.
    /// </summary>
    private void SwitchState()
    {
        StopCoroutine(currentState);

        currentState = nextState;
        StartCoroutine(currentState);
    }

    private IEnumerator Idle()
    {
        while (currentState == "Idle")
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                nextState = "Moving";
            }
            yield return null;
        }
    }

    private IEnumerator Moving()
    {
        while (currentState == "Moving")
        {
            if (!CheckMovement())
            {
                nextState = "Idle";
            }
            yield return null;
        }
    }

    /// <summary>
    /// Checks the player rotation based on Mouse Axis input
    /// </summary>
    private void CheckRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        Vector3 playerRotation = transform.rotation.eulerAngles;
        playerRotation.y += mouseX;

        transform.rotation = Quaternion.Euler(playerRotation);

        Vector3 cameraRotation = playerCamera.transform.rotation.eulerAngles;
        cameraRotation.x -= mouseY;

        playerCamera.transform.rotation = Quaternion.Euler(cameraRotation);
    }

    /// <summary>
    /// Checks and handles movement of the player
    /// </summary>
    /// <returns>True if user input is detected and player is moved.</returns>
    private bool CheckMovement()
    {
        Vector3 newPos = transform.position;

        Vector3 xMovement = transform.right * Input.GetAxis("Horizontal");
        Vector3 zMovement = transform.forward * Input.GetAxis("Vertical");

        Vector3 movementVector = xMovement + zMovement;
        //Debug.Log(movementVector);
        //Vector3 movementVector = xMovement;

        if (movementVector.sqrMagnitude > 0)
        {
            movementVector *= moveSpeed * moveSpeedMultiplier * Time.deltaTime;
            newPos += movementVector;
            //Debug.Log(movementVector);

            transform.position = newPos;
            return true;
        }
        else
        {
            return false;
        }

    }

    /// <summary>
    /// Checks the player health in the Update() function
    /// </summary>
    private void CheckHealth()
    {
        if (health <= 0) // Once health reach 0 or below, player die
        {
            Die();
        }
        else if (health > 0 && health < 100) // Regen health over time
        {
            healthRegenTimer += Time.deltaTime;

            if (healthRegenTimer >= healthTimeToRegen)
            {
                health += 5;
                healthRegenTimer = 0;
            }
        }
        else if (health > maxHealth) // To check if health regenerates to the maximum value
        {
            health = maxHealth;
        }
    }

    /// <summary>
    /// Checks whether the player is sprinting and perform the necessary value changes when player is sprinting.
    /// </summary>
    private void CheckSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 && CheckMovement())
        {
            moveSpeedMultiplier = storedMoveSpeedMultiplier;
            StaminaDeplete();
        }
        else
        {
            moveSpeedMultiplier = 1;
            StaminaReplenish();
        }
    }

    /// <summary>
    /// Checks if the weapon is active
    /// </summary>
    private void CheckWeapon()
    {
        if (weapon.activeInHierarchy)
        {
            damage = 20;
        }
    }

    /// <summary>
    /// Raycast for player interaction.
    /// </summary>
    private void Raycast()
    {
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * raycastLength);

        RaycastHit hit;
        int layerMask = 1 << 3; // LayerMask for "Interactable" layer

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, raycastLength, layerMask))
        {
            if (hit.collider.CompareTag("Collectible")) // If collider has a tag called "Collectible"
            {
                uIManager.SetCrosshairText(true, "Collect", 1);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.GetComponent<Collectible>().PickUp();
                }
            }
            else if (hit.collider.CompareTag("Chest")) // If collider has a tag called "Collectible"
            {
                uIManager.SetCrosshairText(true, "Open the Chest", 1);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.GetComponent<Chest>().Open();
                }
            }
            else if (hit.collider.CompareTag("Door")) // If collider has a tag called "Collectible"
            {
                uIManager.SetCrosshairText(true, "Open the Door", 1);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.GetComponent<Door>().OpenAlt();
                }
            }
            else if (hit.collider.CompareTag("Puzzle")) // If collider has a tag called "Puzzle"
            {
                uIManager.SetCrosshairText(true, "Rotate puzzle piece", 2);

                if (Input.GetButtonDown("Fire1"))
                {
                    hit.transform.GetComponent<PuzzlePiece>().RotatePiece();
                }
            }
            else if (hit.collider.CompareTag("Enemy")) // If collider has a tag called "Enemy"
            {
                uIManager.SetCrosshairText(true, "Attack", 2);

                if (Input.GetButtonDown("Fire1"))
                {
                    Attack(hit.collider.gameObject);
                }
            }
        }
        else
        {
            uIManager.SetCrosshairText(false, "", 0); // Disables the text below crosshair
        }
    }

    /// <summary>
    /// Function used to deal damage to any enemy AI
    /// </summary>
    /// <param name="enemy"></param>
    private void Attack(GameObject enemy)
    {
        enemy.GetComponent<Enemy>().TakeDamage(damage);
    }

    /// <summary>
    /// Function to replenish player's stamina
    /// </summary>
    private void StaminaReplenish()
    {
        // Increase the value per frame
        staminaRegenTimer += Time.deltaTime;

        if (stamina < maxStamina && staminaRegenTimer >= staminaTimeToRegen)
        {
            stamina += staminaRegenRate * Time.deltaTime;

            if (stamina > maxStamina)
            {
                stamina = maxStamina;
            }
        }
    }

    /// <summary>
    /// Function to deplete the player's stamina
    /// </summary>
    private void StaminaDeplete()
    {
        staminaRegenTimer = 0;
        stamina -= staminaDepleteRate * Time.deltaTime;

        if (stamina < 0)
        {
            stamina = 0;
        }
    }

    private void Die()
    {
        animator.SetTrigger("isDead");
        gameManager.PlayerLock();
        StartCoroutine(uIManager.Die());
    }
}
