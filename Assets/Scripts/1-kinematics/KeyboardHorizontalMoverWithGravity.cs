using UnityEngine;
using UnityEngine.InputSystem;


/**
 * This component allows the player to move its object horizontally using the keyboard.
 * It uses CharacterController.Move, which can climb stairs and ramps.
 */
[RequireComponent(typeof(CharacterController))]
public class KeyboardHorizontalMoverWithGravity: MonoBehaviour {
    [Tooltip("Horizontal speed when clicking the arrows, in meters per second")]
    [SerializeField] float speed = 10.0f;

    //[Tooltip("A speed of a standing character towards Earth, in order to activate the colliders")]
    //[SerializeField] float standSpeed = -0.0001f;

    [Tooltip("Vertical acceleration when free-falling, in meters per second^2")]
    [SerializeField] float gravityAcceleration = -10.0f;

    [SerializeField] InputAction moveHorizontal;

    [Header("These fields are for display only")]
    [SerializeField] bool controllerIsGrounded;

    void OnValidate() {
        // Provide default bindings for the input actions.
        // Based on answer by DMGregory: https://gamedev.stackexchange.com/a/205345/18261
        if (moveHorizontal == null)
            moveHorizontal = new InputAction(type: InputActionType.Button);
        if (moveHorizontal.bindings.Count == 0)
            moveHorizontal.AddCompositeBinding("1DAxis")
                .With("Positive", "<Keyboard>/rightArrow")
                .With("Negative", "<Keyboard>/leftArrow");
    }

    private CharacterController controller;
    private Vector3 velocity;

    private void OnEnable() {
        moveHorizontal.Enable();
    }

    private void OnDisable() {
        moveHorizontal.Disable();
    }


    void Start() {
        controller = GetComponent<CharacterController>();
        velocity = new Vector3(0, 0, 0); 
    }

    void Update() {
        if (!controller.enabled) return;

        controllerIsGrounded = controller.isGrounded;
        if (controllerIsGrounded)
        {  // character is touching the ground: allow to walk
           //velocity.y = standSpeed;  // a small negative velocity, to keep the character grounded 
            velocity.x = speed * moveHorizontal.ReadValue<float>();
        }
        else
        {  // Character is above the ground - accelerate downwards.
            velocity.y += gravityAcceleration * Time.deltaTime;
        }

        var deltaPosition = velocity * Time.deltaTime;
        controller.Move(deltaPosition);
    }
}