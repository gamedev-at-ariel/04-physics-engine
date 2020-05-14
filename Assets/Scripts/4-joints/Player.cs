using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;
    float jumpForce = 6.5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

    }

    public void Jump()
    {
        Vector3 up = transform.TransformDirection(Vector3.up);
        rb.AddForce(up * jumpForce, ForceMode2D.Impulse);
    }
}
