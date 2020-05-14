using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  This component automatically adds force and torque to its object.
 */
[RequireComponent(typeof(Rigidbody))]
public class ForceAdder: MonoBehaviour {
    [SerializeField] float forceSize = 10f;
    [SerializeField] ForceMode forceMode = ForceMode.Force;
    [SerializeField] float torqueSize = 10f;
    [SerializeField] ForceMode torqueMode = ForceMode.Force;

    private float LinearVelocity;
    private float AngularVelocity;

    private Rigidbody rb;
    private float startTime;

    void Start() {
        rb = GetComponent<Rigidbody>();
        startTime = Time.time;
    }

    private void FixedUpdate() {
        rb.AddForce (new Vector3(0,0,forceSize), forceMode);
        rb.AddTorque(new Vector3(0, 0, torqueSize), torqueMode);

        LinearVelocity = rb.velocity.magnitude;
        AngularVelocity = rb.angularVelocity.magnitude;
    }

    void OnGUI() {
        GUIStyle fontSize = new GUIStyle(GUI.skin.GetStyle("label"));
        fontSize.fontSize = 22;
        fontSize.normal.textColor = Color.black;
        GUI.Label(new Rect(100, 0, 200, 50),  "LinearSpeed: "  + LinearVelocity.ToString("F2"), fontSize);
        GUI.Label(new Rect(100, 50, 200, 50), "AngularSpeed: " + AngularVelocity.ToString("F2"), fontSize);
        GUI.Label(new Rect(100, 100, 200, 50), "Time: " + (Time.time-startTime).ToString("F2"), fontSize);
    }
}
