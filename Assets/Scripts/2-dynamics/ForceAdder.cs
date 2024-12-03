using UnityEngine;

/**
 *  This component automatically adds force and torque to its object.
 */
[RequireComponent(typeof(Rigidbody))]
public class ForceAdder: MonoBehaviour {
    [Tooltip("Constant force that acts on the object, in Newtons (kg*m/s^2)")]
    [SerializeField] float forceSize = 10f;
    [SerializeField] ForceMode forceMode = ForceMode.Force;

    [Tooltip("Constant torque (rotational force) that acts on the object")]
    [SerializeField] float torqueSize = 10f;
    [SerializeField] ForceMode torqueMode = ForceMode.Force;

    private Rigidbody rb;
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        // NOTE: When you work with a physical rigid body,
        //       all changes to the rigid body should be in FixedUpdate!
        rb.AddForce (new Vector3(0,0,forceSize), forceMode);
        rb.AddTorque(new Vector3(0,0,torqueSize), torqueMode);
    }
}
