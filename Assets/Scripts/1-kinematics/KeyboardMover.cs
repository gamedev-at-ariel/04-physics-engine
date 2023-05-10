using UnityEngine;
using UnityEngine.InputSystem;

/**
 * This component allows the player to accelerate its object horizontally and jump vertically.
 * It also automatically accelerates its object downwards by the given gravity acceleration.
 */
[RequireComponent(typeof(CharacterController))]
public class KeyboardMover : MonoBehaviour
{
    CharacterController controller;


    [Header("Horizontal movement")]

    [SerializeField] InputAction moveHorizontal = new InputAction(type: InputActionType.Button);

    [Tooltip("Horizontal acceleration when clicking the arrows, in meters per second^2")]
    [SerializeField] float feetAcceleration = 100.0f;

    [Tooltip("Largest horizontal speed that the character can attain, in meters per second.")]
    [SerializeField] float maxSpeed = 10.0f;

    [Tooltip("If the speed is at least this value, the character will be slowed down by friction.")]
    [SerializeField] float minSpeedForFriction = 0.01f;

    [Tooltip("Horizontal acceleration of friction, against the direction of movement, in meters per second^2")]
    [SerializeField] float frictionAcceleration = 20.0f;


    [Header("Vertical movement")]

    [SerializeField] InputAction jump;

    [Tooltip("Vertical acceleration when free-falling, in meters per second^2")]
    [SerializeField] float gravityAcceleration = -10.0f;

    [Tooltip("Vertical speed immediately after jumping, in meters per second")]
    [SerializeField] float jumpSpeed = 10.0f;

    [Tooltip("A speed of a standing character towards Earth, in order to activate the colliders")]
    [SerializeField] float standSpeed = -0.0001f;


    [Tooltip("The factor by which the character's horizontal speed is decreased when it jumps.")]
    [Range(0, 1f)]
    [SerializeField] float slowDownAtJump = 0.5f;

    [Header("These fields are for display only")]
    [SerializeField] Vector3 velocity = Vector3.zero;
    [SerializeField] bool controllerIsGrounded;

    void OnValidate() {
        // Provide default bindings for the input actions.
        // Based on answer by DMGregory: https://gamedev.stackexchange.com/a/205345/18261
        if (jump == null)
            jump = new InputAction(type: InputActionType.Button);
        if (jump.bindings.Count == 0)
            jump.AddBinding("<Keyboard>/space");

        if (moveHorizontal == null)
            moveHorizontal = new InputAction(type: InputActionType.Button);
        if (moveHorizontal.bindings.Count == 0)
            moveHorizontal.AddCompositeBinding("1DAxis")
                .With("Positive", "<Keyboard>/rightArrow")
                .With("Negative", "<Keyboard>/leftArrow");
    }

    void Start() {
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable() {
        moveHorizontal.Enable();
        jump.Enable();
    }

    private void OnDisable() {
        moveHorizontal.Disable();
        jump.Disable();
    }


    private float DeltaVelocityWalking() {
        //Debug.Log("moveHorizontal.ReadValue<float>()=" + moveHorizontal.ReadValue<float>());
        float accelerationX = moveHorizontal.ReadValue<float>() * feetAcceleration;
        if (velocity.x > minSpeedForFriction)
            accelerationX -= frictionAcceleration;
        else if (velocity.x < -minSpeedForFriction)
            accelerationX += frictionAcceleration;
        float deltaVelocityX = accelerationX * Time.deltaTime;
        if (Mathf.Abs(velocity.x + deltaVelocityX) > maxSpeed)
            deltaVelocityX = 0;
        //Debug.Log("deltaVelocityX=" + deltaVelocityX + " velocity.x=" + velocity.x);
        return deltaVelocityX;
    }

    void Update() {
        if (!controller.enabled) return;    
        controllerIsGrounded = controller.isGrounded;
        if (controllerIsGrounded) {  // character is touching the ground - allow to walk and jump.
            velocity.x += DeltaVelocityWalking();
            if (Mathf.Abs(velocity.x) < minSpeedForFriction)
                velocity.x = 0;
            if (jump.WasPressedThisFrame()) {
                velocity.y = jumpSpeed;
                velocity.x *= slowDownAtJump;   // decrease horizontal velocity when jumping - for better control
                //Debug.Log("jumping! velocity.y=" + velocity.y);
            } else {
                velocity.y = standSpeed;  // a small negative velocity, to keep the character grounded 
            }
        } else {  // Character is above the ground - accelerate downwards.
            velocity.y += gravityAcceleration * Time.deltaTime;
        }

        var deltaPosition = velocity * Time.deltaTime;
        controller.Move(deltaPosition);
    }
}