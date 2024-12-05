using UnityEngine;
using UnityEngine.InputSystem;

/**
 * This component lets the user move its rigid body (e.g. a ball) using the keyboard, by adding force to the body.
 * It moves in the x-z plane.
 */
public class KeyboardForceAdderXZ : MonoBehaviour {
    [Tooltip("How much force to add in the direction of movement")]
    [SerializeField] float forceSize = 5.0f;

    [SerializeField] InputAction move = new InputAction(type: InputActionType.Value, expectedControlType: nameof(Vector2));

    void OnValidate()
    {
        // Provide default bindings for the input actions.
        // Based on answer by DMGregory: https://gamedev.stackexchange.com/a/205345/18261
        if (move.bindings.Count == 0)
            move.AddCompositeBinding("2DVector")
                .With("Right", "<Keyboard>/rightArrow")
                .With("Left", "<Keyboard>/leftArrow")
                .With("Up", "<Keyboard>/upArrow")
                .With("Down", "<Keyboard>/downArrow")
                ;
    }

    private CharacterController controller;
    private Vector3 velocity;

    private void OnEnable()
    {
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }


    private Rigidbody rb;
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        //float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");
        //Vector3 moveDirection3D = new Vector3(x, 0, z);
        Vector2 moveDirection2D = move.ReadValue<Vector2>();
        //Debug.Log("moveDirection2D = " + moveDirection2D);
        Vector3 moveDirection3D = new Vector3(moveDirection2D.x, 0, moveDirection2D.y);
        rb.AddForce(moveDirection3D * forceSize);
    }
}
