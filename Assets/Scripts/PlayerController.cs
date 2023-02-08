using UnityEngine;
using System;
using UnityEngine.InputSystem;

/// <summary>
/// PlayerController.cs governs the Player GameObject, as well as taking orders from the PlayerInput system to move, jump, etc.
/// </summary>
public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    Vector3 direction;

    int   desiredLane = 1; // 0 left, 1 center, 2 right - This is the new lane after player inputs a button
    float lerpBoundary = .01f; // After we get this close to destination, just set player there

    public static bool IsInputEnabled = true;
    public        bool isJumping = false;

    [SerializeField] float laneDistance = 4;  // distance between our lanes in Unity measurements

    [Header("Set in Inspector")]
    [SerializeField] float forwardSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] float gravityValue;

    [SerializeField] GameObject mainCanvas;

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
    /// <summary>
    /// Checks if testValue is between bound1 and bound2, and returns the result in true/false.
    /// </summary>
    public bool IsBetween(float testValue, float bound1, float bound2)
    {
        return (testValue >= Math.Min(bound1, bound2) && testValue <= Math.Max(bound1, bound2));
    }

    /// <summary>
    /// Checks our PlayerInput system for left or right inputs.  Will not go out of bounds
    /// </summary>
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

    /// <summary>
    /// Checks our PlayerInput system for jump inputs.  Can not double jump.
    /// </summary>
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
    /// When another GameObject with a special "Trigger" collider touches us, we will hit gameOver state
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
