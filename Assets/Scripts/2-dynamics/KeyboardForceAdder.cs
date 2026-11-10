using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 *  This component allows the player to add force to its object using the arrow keys,
 *  and to jump using the space key. Works with a 3D Rigidbody.
 *
 *  The player can walk and jump only while standing on the ground.
 *  To check whether the object is on the ground, we sweep a sphere a short distance downwards (see CheckGrounded).
 */
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class KeyboardForceAdder : MonoBehaviour {
    [Header("Walking")]
    [Tooltip("The horizontal force that the player's feet use for walking, in newtons.")]
    [SerializeField] float walkForce = 5f;
    [SerializeField] InputAction moveHorizontal;

    [Header("Jumping")]
    [Tooltip("The vertical impulse that the player's feet use for jumping, in newtons*sec.")]
    [SerializeField] float jumpImpulse = 5f;
    [SerializeField] InputAction jump;

    [Tooltip("The factor by which the horizontal speed is multiplied when jumping - for better control.")]
    [Range(0,1f)]
    [SerializeField] float slowDownAtJump = 0.5f;

    [Header("Ground check")]
    [Tooltip("The layers that count as ground. Do NOT include the player's own layer.")]
    [SerializeField] LayerMask groundLayers;

    [Tooltip("The ground can be at most this far below the bottom of the collider, and still count as 'grounded', in meters.")]
    [SerializeField] float groundCheckDistance = 0.1f;

    [Tooltip("The steepest slope that counts as ground, in degrees. Steeper surfaces, such as walls, do not count.")]
    [Range(0, 90f)]
    [SerializeField] float maxGroundAngle = 45f;

    [Header("This field is for display only")]
    [SerializeField] bool isGrounded;

    void OnValidate() {
        // Provide default bindings for the input actions. Based on https://gamedev.stackexchange.com/a/205345/18261
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

    private Rigidbody myRigidbody;
    private Collider myCollider;
    void Awake() {
        myRigidbody = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
    }

    private void OnEnable() {
        moveHorizontal.Enable();
        jump.Enable();
    }

    private void OnDisable() {
        moveHorizontal.Disable();
        jump.Disable();
    }

    private bool playerWantsToJump = false;

    private void Update() {
        // Keyboard events are checked each frame, so we should check them in Update.
        // WasPressedThisFrame is true only in a single frame, and FixedUpdate might not run in that frame,
        // so we keep the request in a variable until the next FixedUpdate.
        if (jump.WasPressedThisFrame())
            playerWantsToJump = true;
    }

    /*
     * Note that updates related to the physics engine should be done in FixedUpdate and not in Update!
     */
    private void FixedUpdate() {
        isGrounded = CheckGrounded();
        if (isGrounded) {  // allow to walk and jump
            float horizontal = moveHorizontal.ReadValue<float>();
            myRigidbody.AddForce(new Vector3(horizontal * walkForce, 0, 0), ForceMode.Force);
            if (playerWantsToJump) {
                myRigidbody.linearVelocity = new Vector3(myRigidbody.linearVelocity.x * slowDownAtJump, myRigidbody.linearVelocity.y, myRigidbody.linearVelocity.z);
                myRigidbody.AddForce(new Vector3(0, jumpImpulse, 0), ForceMode.Impulse);
            }
        }
        // Forget the jump request even if it was not used (pressed in mid-air),
        // otherwise the player would jump automatically when landing.
        playerWantsToJump = false;
    }


    /**
     * Returns true if the object stands on ground.
     * Works using Physics.SphereCast.
     * 
     * Worse options:
     *  - RayCast: might miss the ground if the player stands on an edge.
     *  - TouchDetector: erroneously detects collisions with walls and ceilings too.
     */
    private bool CheckGrounded() {
        GetGroundCheckSphere(out Vector3 center, out float radius, out float distance);
        bool hitSomething = Physics.SphereCast(center, radius, Vector3.down, out RaycastHit hit, distance,
                                               groundLayers, QueryTriggerInteraction.Ignore);  // triggers (e.g. coins) are not ground
            //  Note: SphereCast ignores colliders that the sphere already overlaps at its starting point, so it does not detect the player's own collider.
        return hitSomething && Vector3.Angle(hit.normal, Vector3.up) <= maxGroundAngle;
    }

    /**
     * Computes the sphere used for the ground check.
     * We use the collider's bounds (an axis-aligned box containing the collider, in world coordinates),
     * so this works with any collider, even when it rotates (e.g. a rolling capsule).
     */
    private void GetGroundCheckSphere(out Vector3 center, out float radius, out float distance) {
        Bounds bounds = myCollider.bounds;
        center = bounds.center;
        radius = 0.9f * Mathf.Min(bounds.extents.x, bounds.extents.y, bounds.extents.z);
        // At the end of the sweep, the bottom of the sphere is groundCheckDistance below the bottom of the collider:
        distance = bounds.extents.y - radius + groundCheckDistance;
    }

    /**
     * When the object is selected in the editor, draw the sphere at the end of the sweep:
     * green if the object is grounded, red otherwise.
     */
    private void OnDrawGizmos() {
        if (myCollider == null) return;
        GetGroundCheckSphere(out Vector3 start, out float radius, out float distance);
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(start + Vector3.down * distance, radius);
    }
}
