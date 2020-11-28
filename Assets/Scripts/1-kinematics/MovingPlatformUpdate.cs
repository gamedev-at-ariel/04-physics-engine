using UnityEngine;

/**
 *  This component moves its object back and forth between two points in space.
 */
public class MovingPlatformUpdate : MonoBehaviour {
    [Tooltip("The points between which the platform moves")] [SerializeField] Transform endPoint = null;
    [SerializeField] float speed = 1f;
    void FixedUpdate() {
        float deltaX = speed * Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, endPoint.position, deltaX);
    }
}
