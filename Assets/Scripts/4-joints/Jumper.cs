using UnityEngine;

/**
 * This component allows the player to jump by clicking Space.
 */
public class Jumper: MonoBehaviour {
    [SerializeField] float jumpForceX = 0;
    [SerializeField] float jumpForceY = 6.5f;

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
        Vector3 up = new Vector3(jumpForceX, jumpForceY, 0);
        rb.AddForce(up, ForceMode2D.Impulse);
    }
}
