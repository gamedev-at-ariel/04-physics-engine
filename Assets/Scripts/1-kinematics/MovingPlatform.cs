using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  This component moves its object back and forth between two points in space.
 */
public class MovingPlatform : MonoBehaviour {
    [Tooltip("The points between which the platform moves")] [SerializeField] Transform startPoint=null, endPoint = null;
    [SerializeField] float speed = 1f;

    bool moveFromStartToEnd = true;

    void FixedUpdate() {
        if (moveFromStartToEnd) {
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);
        } else {  // move from end to start
            transform.position = Vector3.MoveTowards(transform.position, startPoint.position, speed * Time.deltaTime);
        }

        if (transform.position == startPoint.position) {
            moveFromStartToEnd = true;
        } else if (transform.position == endPoint.position) {
            moveFromStartToEnd = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<KeyboardMover>()) {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.GetComponent<KeyboardMover>()) {
            other.transform.parent = null;
        }
    }


}
