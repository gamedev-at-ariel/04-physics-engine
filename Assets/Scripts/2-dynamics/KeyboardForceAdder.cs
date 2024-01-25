using UnityEngine;
using UnityEngine.InputSystem;

/**
 *  This component allows the player to add force to its object using the arrow keys.
 *  Works with a 3D RigidBody.
 */
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TouchDetector))]
public class KeyboardForceAdder : MonoBehaviour {
    [Tooltip("The horizontal force that the player's feet use for walking, in newtons.")]
    [SerializeField] float walkForce = 5f;
    [SerializeField] InputAction moveHorizontal;

    [Tooltip("The vertical force that the player's feet use for jumping, in newtons.")]
    [SerializeField] float jumpImpulse = 5f;
    [SerializeField] InputAction jump;

    [Range(0,1f)]
    [SerializeField] float slowDownAtJump = 0.5f;


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

    private Rigidbody rb;
    private TouchDetector td;
    void Start() {
        rb = GetComponent<Rigidbody>();
        td = GetComponent<TouchDetector>();
    }

    private ForceMode walkForceMode = ForceMode.Force;
    private ForceMode jumpForceMode = ForceMode.Impulse;
    private bool playerWantsToJump = false;
    private void OnEnable() {
        moveHorizontal.Enable();
        jump.Enable();
    }

    private void OnDisable() {
        moveHorizontal.Disable();
        jump.Disable();
    }

    private void Update() {
        // Keyboard events are checked each frame, so we should check them in Update.
        if (jump.WasPressedThisFrame() && td.IsTouching())
            playerWantsToJump = true;
    }

    /*
     * Note that updates related to the physics engine should be done in FixedUpdate and not in Update!
     */
    private void FixedUpdate() {
        if (td.IsTouching()) {  // allow to walk and jump 
            float horizontal = moveHorizontal.ReadValue<float>();
            rb.AddForce(new Vector3(horizontal* walkForce, 0, 0), walkForceMode);
            if (playerWantsToJump) {            // Since it is active only once per frame, and FixedUpdate may not run in that frame!
                rb.velocity = new Vector3(rb.velocity.x * slowDownAtJump, rb.velocity.y, rb.velocity.z);
                rb.AddForce(new Vector3(0, jumpImpulse, 0), jumpForceMode);
                playerWantsToJump = false;
            }
        }
    }
}
