using UnityEngine;


/**
 * This component lets the player drag its object while clicking the left mouse button,
 * and drop it by releasing the mouse.
 */
[RequireComponent(typeof(Rigidbody))]
public class DragAndDropper3D: MonoBehaviour {

    [Header("These fields are for display only")]
    [SerializeField] private Vector3 positionMinusMouse;
    [SerializeField] private float screenZCoordinate;
    
    private Rigidbody rb;
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }
    
    // This function is called when the player clicks the collider of this object.
    void OnMouseDown() {
        if (!rb.IsSleeping()) return;  // Do not allow the player to drag the object when it is moving.
        rb.isKinematic = true; // ignore forces, since the player now moves the object.
        screenZCoordinate = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        positionMinusMouse = transform.position - MousePositionOnWorld();
    }

    // This function is called when the player drags the mouse.
    void OnMouseDrag() {
        if (!rb.IsSleeping()) return;  // Do not allow the player to drag the object when it is moving.
        transform.position = positionMinusMouse + MousePositionOnWorld();
    }

    // This function is called when the player releases the mouse button.
    void OnMouseUp() {
        rb.isKinematic = false;
    }
        
    private Vector3 MousePositionOnWorld() {
        Vector3 mouseOnScreen = Input.mousePosition;    // Screen coordinates of mouse (x,y)
        mouseOnScreen.z = screenZCoordinate;            // z coordinate of game object on screen
        Vector3 mouseOnWorld = Camera.main.ScreenToWorldPoint(mouseOnScreen);
        return mouseOnWorld;
    }
}

