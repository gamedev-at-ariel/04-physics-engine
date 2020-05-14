using UnityEngine;

/**
 * This component lets the user move its rigid body (e.g. a ball) using the keyboard, by adding force to the body.
 * It moves in the x-z plane.
 */
public class KeyboardForceAdderXZ : MonoBehaviour {
    [Tooltip("How much force to add in the direction of movement")]
    [SerializeField] float forceSize = 5.0f;

    private Rigidbody rb;
    //private Vector3 _starting_point;
    void Start() {
        rb = GetComponent<Rigidbody>();
        //_starting_point = gameObject.transform.position;
    }

    private void FixedUpdate() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement_direction = new Vector3(x, 0, z);
        rb.AddForce(movement_direction * forceSize);
    }

    /*
    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag=="Bound") {
            gameObject.transform.position = _starting_point;
        }
    }
    */
}
