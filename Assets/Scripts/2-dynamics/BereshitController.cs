using System.Collections;
using UnityEngine;


/**
 * This component simulates the controls of a spaceship that should land on the moon without exploding.
 */
[RequireComponent(typeof(Rigidbody2D))]
public class BereshitController: MonoBehaviour {
    [SerializeField] float distanceToStartSlowdown = 0;
    [SerializeField] float dragForceForSlowdown = 0;

    [Header("This field is for display only")]

    [SerializeField] float distanceToMoon = 0;

    #pragma warning disable 0414
    [SerializeField] bool isSlowingDown = false;
    #pragma warning restore 0414

    private Rigidbody2D rb;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    /* To prevent explosion - send a ray to the moon and "drag" the spaceship when the moon is nearby: */
    private void FixedUpdate() {
        RaycastHit2D hit = Physics2D.Raycast(origin:  transform.position, direction: Vector2.down, distance: Mathf.Infinity);
        distanceToMoon = hit.distance;
        if (hit.collider != null && distanceToMoon <= distanceToStartSlowdown) {  // If there is an object sufficiently close to the spaceship -
            isSlowingDown = true;
            rb.linearDamping = dragForceForSlowdown;      // Add drag, to slow down the spaceship.
        }

        // To see the Gizmo of the ray in the Scene view:
        Debug.DrawRay(transform.position, Vector2.down * distanceToStartSlowdown, Color.red);
    }
}
