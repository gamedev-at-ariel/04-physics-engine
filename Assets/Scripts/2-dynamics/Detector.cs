using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DoodlerBad")
        {
            other.transform.rotation = Quaternion.Euler(Vector3.zero);
            other.transform.position = new Vector2(-0.42f, 0.9450566f);
            var rb = other.gameObject.GetComponent<Rigidbody2D>();
            rb.freezeRotation = true;
        }

        if(other.tag=="Doodler")
        {
            other.transform.rotation = Quaternion.Euler(Vector3.zero);
            other.transform.position = new Vector2(-4.702234f, 0.9450566f);
            var rb = other.gameObject.GetComponent<Rigidbody2D>();
            rb.freezeRotation = true;
        }
    }
}
