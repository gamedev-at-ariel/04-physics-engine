using UnityEngine;


/**
 * This component allows a character to move and jump, but only when touching the ground.
 * 
 * SOURCE: Unity examples:
 * https://github.com/Unity-Technologies/PhysicsExamples2D/blob/master/Assets/Scripts/SceneSpecific/Miscellaneous/SimpleGroundedController.cs
 */
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TouchDetector))]
public class SimpleGroundedController3D : MonoBehaviour {
    [SerializeField] float JumpImpulse = 7f;
    [SerializeField] float SideSpeed = 2f;

    private Rigidbody rigidBody;
    private TouchDetector touchDetector;
    private bool playerWantsToJump;
    private float horizontalSpeed;

    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        touchDetector = GetComponent<TouchDetector>();
    }

    void Update()  {
        // Keyboard events are tested each frame, so we should check them here.
        if (Input.GetKeyDown(KeyCode.Space))   
            playerWantsToJump = true;

        horizontalSpeed = Input.GetAxis("Horizontal") * SideSpeed;
    }

    void FixedUpdate()  {
        // Handle jump.
        // NOTE: If instructed to jump, we'll check if we're grounded.
        if (playerWantsToJump && touchDetector.IsTouching())
            rigidBody.AddForce(Vector3.up * JumpImpulse, ForceMode.Impulse);

        // Set horizontal velocity.
        rigidBody.velocity = new Vector3(horizontalSpeed, rigidBody.velocity.y, rigidBody.velocity.z);

        // Reset movement.
        playerWantsToJump = false;
        horizontalSpeed = 0f;
    }
}
