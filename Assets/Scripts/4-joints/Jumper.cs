using UnityEngine;
using UnityEngine.InputSystem;

/**
 * This component allows the player to jump by clicking Space.
 */
public class Jumper: MonoBehaviour {
    [SerializeField] float jumpForceX = 0;
    [SerializeField] float jumpForceY = 6.5f;

    [SerializeField] InputAction jump;
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

    private Rigidbody2D rb;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (jump.WasPressedThisFrame()) {
            Jump();
        }
    }

    public void Jump() {
        Vector3 up = new Vector3(jumpForceX, jumpForceY, 0);
        rb.AddForce(up, ForceMode2D.Impulse);
    }
}
