using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    CharacterController characterController;
    Vector3 direction;

    int desiredLane = 1; // 0 left, 1 center, 2 right
    float lerpBoundary = .01f;
    public static bool IsInputEnabled = true;
    public bool isJumping = false;
    bool isGrounded = true;
    [SerializeField] float laneDistance = 4;  // distance between 2 lanes
    [Header("Set in Inspector")]
    [SerializeField] float forwardSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] float gravityValue;

    [SerializeField] GameObject mainCanvas;

    Vector3 playerVelocity;

    PlayerInput playerInput;
    

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInputEnabled)
        {
            // Inputs for the Lane
            JumpMovement();
            LaneMovement();

            // calculate where we should be in the future
            Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

            // Add proper force depending on which direction we are to go
            if (desiredLane == 0)
            {
                targetPosition += Vector3.left * laneDistance;
            }
            else if (desiredLane == 2)
            {
                targetPosition += Vector3.right * laneDistance;
            }

            characterController.Move(direction * Time.deltaTime);
            // If we are very very close to the target position, just snap it to there
            if (IsBetween(transform.position.x, targetPosition.x + (targetPosition.x * lerpBoundary), targetPosition.x - (targetPosition.x * lerpBoundary)))
            {
                transform.position = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
            }
            else // otherwise, keep the Lerp moving closer
            {
                transform.position = new Vector3(Vector3.Lerp(transform.position, targetPosition, 10 * Time.fixedDeltaTime).x, transform.position.y, -4f);
            }
        }
    }
    public bool IsBetween(float testValue, float bound1, float bound2)
    {
        return (testValue >= Math.Min(bound1, bound2) && testValue <= Math.Max(bound1, bound2));
    }

    public void LaneMovement() 
    {
        if (playerInput.actions["MoveRight"].WasPressedThisFrame() && desiredLane < 2)
        {
            desiredLane++;
        }
        if (playerInput.actions["MoveLeft"].WasPressedThisFrame() && desiredLane > 0)
        {
            desiredLane--;
        }
    }

    public void JumpMovement()
    {
        if (playerInput.actions["Jump"].WasPressedThisFrame() && characterController.isGrounded)
        {
            Debug.Log("We Pressed the key " + characterController.isGrounded);
            direction.y = jumpHeight;
        }
        else if (playerInput.actions["Jump"].WasReleasedThisFrame())
        {
            Debug.Log("We Released the key " + characterController.isGrounded);
        }
        else if (!characterController.isGrounded)
        {
            direction.y += gravityValue * Time.deltaTime;
        }
    }

    /// <summary>
    /// When another GameObject with a special "Trigger" collider touches us, we will
    /// Take damage, or even gameOver
    /// </summary>
    /// <param name="other">Collider that hit us</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            mainCanvas.SetActive(true);
            IsInputEnabled = false;

            // TODO: Setup Gameover event flag here

            foreach (var spawner in GameObject.FindGameObjectsWithTag("Spawner"))
            {
                spawner.GetComponent<SpawnerController>().TurnOff();
            }
        }
    }
}
