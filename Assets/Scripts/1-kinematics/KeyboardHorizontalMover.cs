using UnityEngine;

/**
 * This component allows the player to move its object horizontally using the keyboard.
 * It uses CharacterController.Move, which can climb stairs and ramps.
 */
[RequireComponent(typeof(CharacterController))]
public class KeyboardHorizontalMover: MonoBehaviour {
    [Tooltip("Horizontal speed when clicking the arrows, in meters per second")]
    [SerializeField] float speed = 10.0f;

    private CharacterController controller;
    private Vector3 velocity;
    void Start() {
        controller = GetComponent<CharacterController>();
        velocity = new Vector3(0, 0, 0); 
    }

    void Update() {
        if (!controller.enabled) return;
        velocity.x = speed * Input.GetAxis("Horizontal");
        controller.Move(velocity * Time.deltaTime);
   }
}