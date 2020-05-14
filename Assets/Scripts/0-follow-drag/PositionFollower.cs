using UnityEngine;

/**
 * This component follows the position of a given object, but not its rotation.
 * Especially useful for cameras.
 */ 
public class PositionFollower: MonoBehaviour{
    [SerializeField] private GameObject objectToFollow = null;

    // NOTE: FixedUpdate should be used for all updates related to rigid bodies or the physics engine.
    private void FixedUpdate() {
        transform.position = objectToFollow.transform.position;
    }
}
