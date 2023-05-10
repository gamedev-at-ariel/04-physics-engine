using UnityEngine;
using UnityEngine.InputSystem;

public class KinematicsExperiment : MonoBehaviour
{
    [SerializeField] InputAction jump;
    [SerializeField] Vector3 velocity = Vector3.zero;
    [SerializeField] Vector3 acceleration  = new Vector3(0,-10,0);
    [SerializeField] bool isTouchingTheGround = false;

    void OnValidate() {
        // Provide default bindings for the input actions.
        // Based on answer by DMGregory: https://gamedev.stackexchange.com/a/205345/18261
        if (jump == null)
            jump = new InputAction(type: InputActionType.Button);
        if (jump.bindings.Count == 0)
            jump.AddBinding("<Keyboard>/space");
    }

    private void OnEnable() {
        jump.Enable();
    }

    private void OnDisable() {
        jump.Disable();
    }

    /*
     * bool IsTouchingTheGround() {
        RaycastHit hit;
        float maxDistanceToGround = 0.1f;
        Vector3 legs = GetComponent<Collider>().bounds.min;
        bool isTouchingTheGround = Physics.Raycast(
            origin: legs,
            direction: Vector3.down,
            out hit,
            layerMask: 1 << LayerMask.NameToLayer("Ground"),
            maxDistance: maxDistanceToGround);
        if (isTouchingTheGround && velocity.y <= 0) {
            Debug.Log("Touching the ground: " + hit.collider.name);
            Debug.DrawRay(legs, Vector3.down * hit.distance, Color.white);
        } else {
            Debug.Log("Not touching the ground");
            Debug.DrawRay(legs, Vector3.down * maxDistanceToGround, Color.red);
        }
        return isTouchingTheGround;
    }
    */


    private void OnCollisionEnter(Collision c) {
        if (c.collider.name == "Ground") {
            isTouchingTheGround = true;
        }
    }

    private void OnCollisionExit(Collision c) {
        if (c.collider.name == "Ground") {
            isTouchingTheGround = false;
        }
    }


    bool playerWantsToJump = false;
    void Update() {
        // Check for user input:
        if (isTouchingTheGround && jump.WasPressedThisFrame()) {
            Debug.Log("Player wants to jump");
            playerWantsToJump = true;
        }
        // Debug.Log(Time.deltaTime);
    }

    private void FixedUpdate() {
        // Update velocity and position:
        if (playerWantsToJump) {
            Debug.Log("Jumping!");
            velocity = 20 * Vector3.up;
            playerWantsToJump = false;
        }
        if (isTouchingTheGround && velocity.y <= 0) {
            velocity = Vector3.zero;
        } else {
            Vector3 deltaV = acceleration * Time.fixedDeltaTime;
            velocity += deltaV;
        }
        Vector3 detlaX = velocity * Time.fixedDeltaTime;
        transform.position += detlaX;
    }
}
