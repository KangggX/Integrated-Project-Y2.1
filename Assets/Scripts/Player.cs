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
    public float moveSpeed;
    public float moveSpeedMultiplier;
    public float stamina;
    public float staminaRegen;
    public int health;
    public int raycastLength;

    /// <summary>
    /// The camera attached to the player model.
    /// Should be dragged in from Inspector.
    /// Settings can be played around.
    /// </summary>
    [Header("Camera Settings")]
    public Camera playerCamera;
    public float rotationSpeed;

    [Space(10)]
    public HealthBar healthBar;
    public UIManager uIManager;

    [HideInInspector]
    public bool inDialogue;

    /// <summary>
    /// Coroutine variable for player movement
    /// </summary>
    private string currentState;
    private string nextState;

    /// <summary>
    /// Stored variables from the player class.
    /// To be used to revert modifications made to these variables.
    /// </summary>
    private int storedRaycastLength;
    private float storedMoveSpeed;
    private float storedRotationSpeed;

    private void Awake()
    {
        // To store the player default Raycast, MoveSpeed and RotationSpeed value.
        storedRaycastLength = raycastLength;
        storedMoveSpeed = moveSpeed;
        storedRotationSpeed = rotationSpeed;
    }

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        nextState = "Idle";
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    private void Update()
    {
        if (nextState != currentState)
        {
            SwitchState();
        }

        healthBar.SetHealth(health);

        CheckRotation();
        CheckSprint();
        CheckDialogue();
        Raycast();
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

            transform.position = newPos;
            return true;
        }
        else
        {
            return false;
        }

    }

    private void CheckSprint()
    {
        float multiplier = moveSpeedMultiplier;

        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            moveSpeedMultiplier = multiplier;
        }
        else
        {
            multiplier = 1;
        }
    }

    /// <summary>
    /// To check if player is currently in a Dialogue.
    /// If true, player won't be able to move, rotate camera and interact with objects.
    /// If true and player press 'E', display next Dialogue sentence.
    /// </summary>
    private void CheckDialogue()
    {
        if (inDialogue)
        {
            raycastLength = 0;
            moveSpeed = 0;
            rotationSpeed = 0;

            if (Input.GetKeyDown(KeyCode.E))
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
            }
        }
        else
        {
            raycastLength = storedRaycastLength;
            moveSpeed = storedMoveSpeed;
            rotationSpeed = storedRotationSpeed;
        }
    }

    /// <summary>
    /// Raycast for player interaction.
    /// </summary>
    private void Raycast()
    {
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * raycastLength);

        RaycastHit hit;
        int layerMask = 1 << 3; //LayerMask for "Interactable" layer

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, raycastLength, layerMask))
        {
            if (hit.collider.CompareTag("Dialogue")) //If collider has a tag called "Dialogue"
            {
                uIManager.SetCrosshairText(true, "Press 'E' to interact");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.GetComponent<DialogueTrigger>().TriggerDialogue();
                    inDialogue = true;
                }
            }
            else if (hit.collider.CompareTag("Collectible")) //If collider has a tag called "Collectible"
            {
                uIManager.SetCrosshairText(true, "Press 'E' to collect");
            }
        }
        else
        {
            uIManager.SetCrosshairText(false, "");
        }
    }
}
