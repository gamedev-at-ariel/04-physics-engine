using UnityEngine;

/**
 * This component allows the player to jump by clicking Space.
 */
public class Jumper: MonoBehaviour {
    [SerializeField] float jumpForce = 6.5f;

    private Rigidbody2D rb;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }
    }

    public void Jump() {
        Vector3 up = new Vector3(0, 1f, 0);//transform.TransformDirection(Vector3.up);
        rb.AddForce(up * jumpForce, ForceMode2D.Impulse);
    }
}
