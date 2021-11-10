using UnityEngine;


/**
 *  This component checks whether its object touches a collider of a given kind.
 *  Works with a 3D RigidBody.
 */
[RequireComponent(typeof(Rigidbody))]
public class TouchDetector : MonoBehaviour {
    [SerializeField] LayerMask layerMask;

    [Header("This field is for display only")]
    [SerializeField] private int touchingColliders = 0;

    private Rigidbody rb;
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("Entering collision with " + collision.gameObject.name + " in layer " + collision.gameObject.layer+ ". layerMask.value="+ layerMask.value);
        if ((layerMask.value & (1 << collision.gameObject.layer)) > 0)
            touchingColliders++;
    }

    private void OnCollisionExit(Collision collision) {
        Debug.Log("Exiting collision with " + collision.gameObject.name + " in layer " + collision.gameObject.layer + ". layerMask.value=" + layerMask.value);
        if ((layerMask.value & (1 << collision.gameObject.layer)) > 0)
            touchingColliders--;
    }

    public bool IsTouching() {
        // See https://answers.unity.com/questions/196381/how-do-i-check-if-my-rigidbody-player-is-grounded.html
        // and https://gamedev.stackexchange.com/questions/105399/how-to-check-if-grounded-with-rigidbody
        return touchingColliders>0;
    }
}
