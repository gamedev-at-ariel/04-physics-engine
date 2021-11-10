using UnityEngine;

/**
 * This component allows the player to accelerate its object horizontally and jump vertically.
 * It also automatically accelerates its object downwards by the given gravity acceleration.
 */
[RequireComponent(typeof(CharacterController))]
public class KeyboardMover: MonoBehaviour {
    CharacterController controller;

    [Header("Horizontal movement")]

    [Tooltip("Horizontal acceleration when clicking the arrows, in meters per second^2")]
    [SerializeField] float feetAcceleration = 100.0f;

    [Tooltip("Largest horizontal speed that the character can attain, in meters per second.")]
    [SerializeField] float maxSpeed = 10.0f;

    [Tooltip("If the speed is at least this value, the character will be slowed down by friction.")]
    [SerializeField] float minSpeedForFriction = 1f;

    [Tooltip("Horizontal acceleration of friction, against the direction of movement, in meters per second^2")]
    [SerializeField] float frictionAcceleration = 20.0f;

    [Header("Vertical movement")]

    [Tooltip("Vertical acceleration when free-falling, in meters per second^2")]
    [SerializeField] float gravityAcceleration = -10.0f;

    [Tooltip("Vertical speed immediately after jumping, in meters per second")]
    [SerializeField] float jumpSpeed = 20.0f;

    [Tooltip("A speed of a standing character towards Earth, in order to activate the colliders")]
    [SerializeField] float standSpeed = -0.0001f;


    [Tooltip("The factor by which the character's horizontal speed is decreased when it jumps.")]
    [Range(0, 1f)]
    [SerializeField] float slowDownAtJump = 0.5f;

    [SerializeField] KeyCode keyToJump = KeyCode.Space;

    [Header("These fields are for display only")]
    [SerializeField] Vector3 velocity;
    [SerializeField] bool isGrounded;


    void Start() {
        controller = GetComponent<CharacterController>();
        velocity = new Vector3(0, 0, 0);    // in meters per second
    }

    private float DeltaVelocityWalking() {
        float accelerationX = Input.GetAxis("Horizontal") * feetAcceleration;
        if (velocity.x>minSpeedForFriction)
            accelerationX -= frictionAcceleration;
        else if (velocity.x < -minSpeedForFriction)
            accelerationX += frictionAcceleration;
        float deltaVelocityX = accelerationX * Time.deltaTime;
        if (Mathf.Abs(velocity.x + deltaVelocityX) > maxSpeed)
            deltaVelocityX = 0;
        return deltaVelocityX;
    }

    private bool playerWantsToJump;
    void Update() {
        if (!controller.enabled) return;

        if (Input.GetKeyDown(keyToJump))
            playerWantsToJump = true;

        if (controller.isGrounded) {  // character is touching the ground - allow to walk and jump.
            velocity.x += DeltaVelocityWalking();
            if (Mathf.Abs(velocity.x) < minSpeedForFriction)
                velocity.x = 0;
            if (playerWantsToJump) {
                velocity.y = jumpSpeed;
                velocity.x *= slowDownAtJump;   // decrease horizontal velocity when jumping - for better control
                playerWantsToJump = false;
            } else {
                velocity.y = standSpeed;  // a small negative velocity, to keep the character grounded 
            }
        } else {  // Character is above the ground - accelerate downwards.
            float deltaVelocityY = gravityAcceleration * Time.deltaTime;
            velocity.y += deltaVelocityY;
        }

        var deltaPosition = velocity * Time.deltaTime;
        controller.Move(deltaPosition);
    }
}