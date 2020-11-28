using UnityEngine;

/**
 * This component allows the player to accelerate its object horizontally and jump vertically.
 * It also automatically accelerates its object downwards by the given gravity acceleration.
 */
[RequireComponent(typeof(CharacterController))]
public class KeyboardMoverDebug: MonoBehaviour {
    CharacterController controller;
    void Start() {
        controller = GetComponent<CharacterController>();
    }

    void Update() {
        controller.Move(new Vector3(0, 0, 0));
    }
}