using UnityEngine;


/**
 * This component allows the player to drag its object using the mouse.
 */
[RequireComponent(typeof(Rigidbody2D))]
public class DragAndDropper2D: MonoBehaviour {
    [SerializeField] Rigidbody2D hook = null;
    [SerializeField] float maxDragDistance = 2f;

    private bool isPressed = false;
    private Rigidbody2D rb;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown() {
        isPressed = true;
        rb.isKinematic = true;  // make the rigid body non-dynamic 
    }

    void Update() {
        if (isPressed) {
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(mouseWorldPosition, hook.position) <= maxDragDistance)
                rb.position = mouseWorldPosition;
            else
                rb.position = hook.position + (mouseWorldPosition - hook.position).normalized * maxDragDistance;
        }
    }

    private void OnMouseUp() {
        isPressed = false;
        rb.isKinematic = false; // make the rigid body dynamic again
    }
}
