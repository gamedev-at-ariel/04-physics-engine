using UnityEngine;
using UnityEngine.InputSystem;


/**
 * This component allows the player to move its object horizontally using the keyboard.
 * It uses CharacterController.Move, which can climb stairs and ramps.
 */
[RequireComponent(typeof(CharacterController))]
public class KeyboardHorizontalMover: MonoBehaviour {
    [Tooltip("Horizontal speed when clicking the arrows, in meters per second")]
    [SerializeField] float speed = 10.0f;

    [SerializeField] InputAction moveHorizontal;

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
        velocity.x = speed * moveHorizontal.ReadValue<float>();
        controller.Move(velocity * Time.deltaTime);  // deltaX
   }
}