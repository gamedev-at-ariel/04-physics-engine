using UnityEngine;

/**
 * This component allows the player to drag its object using the mouse.
 */
[RequireComponent(typeof(Rigidbody2D))]
public class DragAndDropper2DAlternative: MonoBehaviour {
    private bool drag;                  // True if is being dragged

    private bool wasKinematic;          // Flag indicating whether or not the Ridigbody
    private Rigidbody2D rb;    // Reference to the GameObject's Rigidbody2D
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        wasKinematic = rb.isKinematic;
    }

    void Update() {
        if (drag == true) {
            DragFunction();
        }
    }

    void OnMouseDown() {
        drag = true;
        rb.isKinematic = true;
    }

    // Checks if the mouse button is released
    void OnMouseUp() {
        // Update flags
        if (drag == true)
            rb.isKinematic = wasKinematic;
        drag = false;
    }

    void DragFunction() {
        // We are converting a 2D mouse coordinate to 3D
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
        // Update GameObject position
        transform.position = new Vector3(pos_move.x, pos_move.y, pos_move.z);
    }

}
