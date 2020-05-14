using UnityEngine;

/**
 * This component follows the X position of a given object.
 * Especially useful for cameras.
 */ 
public class PositionXFollower: MonoBehaviour{
    [SerializeField] private GameObject objectToFollow = null;

    // NOTE: FixedUpdate should be used for all updates related to rigid bodies or the physics engine.
    private void FixedUpdate() {
        Vector3 newPosition = transform.position;
        newPosition.x = objectToFollow.transform.position.x;
        transform.position = newPosition;
    }
}
