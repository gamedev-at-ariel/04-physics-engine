using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDoodle : MonoBehaviour {
    [SerializeField] private float _speed = 2f;

    Collision2D mycol;

    private Rigidbody2D rb;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("ouch!");
        if (collision.gameObject.name == "Doodler") {
            _speed = Mathf.Max(_speed, collision.relativeVelocity.x);
            Debug.Log("Speed " + _speed);
        }
        if (collision.gameObject.tag == "Platform")
            rb.freezeRotation = false;
    }
}
