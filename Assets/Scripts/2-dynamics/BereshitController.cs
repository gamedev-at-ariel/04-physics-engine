using System.Collections;
using UnityEngine;


/**
 * This component simulates the controls of a spaceship that should land on the moon without exploding.
 */
[RequireComponent(typeof(Rigidbody2D))]
public class BereshitController: MonoBehaviour {
    [SerializeField] float distanceToStartSlowdown = 0;
    [SerializeField] float dragForceForSlowdown = 0;
    
    private Rigidbody2D rb;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    /* To prevent explosion - send a ray to the moon and "drag" the spaceship when the moon is nearby: */
    private void FixedUpdate() {
        RaycastHit2D hit = Physics2D.Raycast(origin:  transform.position, direction: Vector2.down, distance: Mathf.Infinity);
        if (hit.collider != null && hit.distance <= distanceToStartSlowdown) {  // If there is an object sufficiently close to the spaceship -
            rb.drag = dragForceForSlowdown;      // Add drag, to slow down the spaceship.
        }

        // To see the Gizmo of the ray in the Scene view:
        Debug.DrawRay(transform.position, Vector2.down * distanceToStartSlowdown, Color.red);
    }
}
