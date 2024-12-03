using UnityEditor.PackageManager;
using UnityEngine;
using System;

/**
 *  This component moves its object in a fixed speed back and forth between two points in space.
 */
public class MovingPlatform : MonoBehaviour {
    Transform startPoint = null, endPoint = null;

    [SerializeField] float speed = 1f;

    bool moveFromStartToEnd = true;

    private void Start() {
        startPoint = transform.parent.Find("StartPoint");
        if (!startPoint) throw new Exception("No child with name StartPoint!");
        endPoint = transform.parent.Find("EndPoint");
        if (!endPoint) throw new Exception("No child with name EndPoint!");
        transform.position = startPoint.position;
    }

    void Update() {
        // If Update is used, the player does not move with the platform.
        float deltaX = speed * Time.deltaTime;
        Transform targetPoint = (moveFromStartToEnd ? endPoint : startPoint);
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, deltaX);
    
        if (transform.position == startPoint.position) {
            moveFromStartToEnd = true;
        } else if (transform.position == endPoint.position) {
            moveFromStartToEnd = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<KeyboardMover>()) {
            other.transform.SetParent(this.transform);
        }
    }
    
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.GetComponent<KeyboardMover>()) {
            other.transform.SetParent(null);
        }
    }


}
