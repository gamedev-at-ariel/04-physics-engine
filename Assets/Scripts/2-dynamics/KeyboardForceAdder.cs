using UnityEngine;


/**
 *  This component allows the player to add force to its object using the arrow keys.
 *  Works with a 3D RigidBody.
 */
[RequireComponent(typeof(Rigidbody))]
public class KeyboardForceAdder : MonoBehaviour {
    [Tooltip("The horizontal force that the player's feet use for walking, in newtons.")]
    [SerializeField] float walkForce = 5f;

    [Tooltip("The vertical force that the player's feet use for jumping, in newtons.")]
    [SerializeField] float jumpForce = 5f;

    [Range(0,1f)]
    [SerializeField] float slowDownAtJump = 0.5f;

    [Header("These fields are for display only")]
    [SerializeField] int touchingColliders = 0;

    private Rigidbody rb;
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private ForceMode walkForceMode = ForceMode.Force;
    private ForceMode jumpForceMode = ForceMode.Impulse;

    private void OnCollisionEnter(Collision collision) {
        touchingColliders++;
    }

    private void OnCollisionExit(Collision collision) {
        touchingColliders--;
    }

    private bool IsGrounded() {
        // See https://answers.unity.com/questions/196381/how-do-i-check-if-my-rigidbody-player-is-grounded.html
        // and https://gamedev.stackexchange.com/questions/105399/how-to-check-if-grounded-with-rigidbody
        /*
        Vector3 topCenter = collider.bounds.center;
        topCenter.y = collider.bounds.max.y - collider.radius;
        Vector3 bottomCenter = collider.bounds.center;
        bottomCenter.y = collider.bounds.min.y + collider.radius;
        Debug.Log("topCenter " + topCenter + "  bottomCenter " + bottomCenter + "  radius " + collider.radius*2);
        return Physics.CheckCapsule(topCenter, bottomCenter, collider.radius, Physics.AllLayers);
        //float groundMargin = 0.1f;
        //return Physics.Raycast(transform.position, Vector3.down, collider.bounds.extents.y+groundMargin);
        */
        return touchingColliders>0;
    }


    /*
     * Note that updates related to the physics engine should be done in FixedUpdate and not in Update!
     */
    private void FixedUpdate() {
        if (IsGrounded()) {  // allow to walk and jump 
            float horizontal = Input.GetAxis("Horizontal");
            rb.AddForce(new Vector3(horizontal* walkForce, 0, 0), walkForceMode);
            bool playerWantsToJump = Input.GetKey(KeyCode.Space);
            // WARNING: Do not use GetKeyDown:
            //bool playerWantsToJump = Input.GetKeyDown(KeyCode.Space);
            if (playerWantsToJump) {            // Since it is active only once per frame, and FixedUpdate may not run in that frame!


                rb.velocity *= slowDownAtJump;  
                rb.AddForce(new Vector3(0, jumpForce, 0), jumpForceMode);
            }
        }
    }
}
