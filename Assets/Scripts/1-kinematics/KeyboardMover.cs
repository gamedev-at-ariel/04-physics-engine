using UnityEngine;

/**
 * This component allows the player to accelerate its object horizontally and jump vertically.
 * It also automatically accelerates its object downwards by the given gravity acceleration.
 */
[RequireComponent(typeof(CharacterController))]
public class KeyboardMover: MonoBehaviour {
    CharacterController controller;

    [Header("Horizontal movement")]

    [SerializeField] float maxSpeed = 10.0f;

    [Tooltip("Horizontal acceleration when clicking the arrows, in meters per second^2")]
    [SerializeField] float feetAcceleration = 100.0f;

    [Tooltip("Horizontal acceleration of friction, against the direction of movement, in meters per second^2")]
    [SerializeField] float frictionAcceleration = 20.0f;

    [Header("Vertical movement")]

    [Tooltip("Vertical acceleration when free-falling, in meters per second^2")]
    [SerializeField] float gravityAcceleration = -10.0f;

    [Tooltip("Vertical speed immediately after jumping, in meters per second")]
    [SerializeField] float jumpSpeed = 20.0f;

    [Range(0, 1f)]
    [SerializeField] float slowDownAtJump = 0.5f;

    [Header("These fields are for display only")]
    [SerializeField] Vector3 velocity;
    [SerializeField] bool isGrounded;

    void Start() {
        controller = GetComponent<CharacterController>();
        velocity = new Vector3(0, 0, 0);    // in meters per second
    }

    private float DeltaVelocityWalking() {
        float accelerationX = Input.GetAxis("Horizontal") * feetAcceleration;
        accelerationX -= Mathf.Sign(velocity.x) * frictionAcceleration;
        float deltaVelocityX = accelerationX * Time.deltaTime;
        if (Mathf.Abs(velocity.x + deltaVelocityX) > maxSpeed)
            deltaVelocityX = 0;
        return deltaVelocityX;
    }

    void Update() {
        if (!controller.enabled) return;
        isGrounded = (controller.collisionFlags & CollisionFlags.Below) != 0;

        if (isGrounded) {  // character is touching the ground - allow to walk and jump.
            velocity.x += DeltaVelocityWalking();

            // Jumping:
            if (Input.GetKeyDown(KeyCode.Space)) {
                velocity.y = jumpSpeed;
                velocity.x *= slowDownAtJump;   // decrease horizontal velocity when jumping - for better control
            } else {
                velocity.y = -0.0001f;  // a small negative velocity, to keep the character grounded 
            }

        } else {  // Character is above the ground - accelerate downwards.
            float deltaVelocityY = gravityAcceleration * Time.deltaTime;
            velocity.y += deltaVelocityY;
        }

        controller.Move(velocity * Time.deltaTime);
    }
}