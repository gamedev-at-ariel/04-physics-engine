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

    void Update() {
        if (isPressed) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(mousePos, hook.position) > maxDragDistance)
                rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance;
            else
                rb.position = mousePos;
        }
    }

    private void OnMouseDown() {
        isPressed = true;
        rb.isKinematic = true;
    }

    private void OnMouseUp() {
        isPressed = false;
        rb.isKinematic = false;
    }
}
