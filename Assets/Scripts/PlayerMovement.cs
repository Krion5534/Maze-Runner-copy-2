using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;

    private Vector3 lastPosition = Vector3.zero;
    public bool isMoving;

    void Start()
    {
        // Getting the reference to our Character Controller component
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Ground Check: Checking if the player is standing on the ground layer
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        Debug.Log("Is Player Grounded? " + isGrounded);

        if (isGrounded && velocity.y < 0)
        {
            // Resetting the velocity when landing so it doesn't build up over time
            velocity.y = -2f;
        }

        // Getting the inputs for WASD / Arrow keys
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Storing the movement input relative to the direction the player is facing
        Vector3 move = transform.right * x + transform.forward * z;

        // Actually moving the player
        controller.Move(move * speed * Time.deltaTime);

        // Checking if the player can jump (space bar pressed and touching the ground)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Physics equation for a controlled jump height
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Applying constant gravity over time
        velocity.y += gravity * Time.deltaTime;

        // Applying the falling / jumping velocity to the character controller
        controller.Move(velocity * Time.deltaTime);

        // Checking if the player is currently moving (for future features like footsteps/bobbing)
        if (lastPosition != transform.position && isGrounded)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        // Setting the last position to the current position at the end of the frame
        lastPosition = transform.position;
    }
}