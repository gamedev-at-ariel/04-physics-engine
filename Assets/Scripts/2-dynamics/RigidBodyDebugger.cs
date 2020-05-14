using UnityEngine;


/**
 * This component prints the properties of its RigidBody, for debugging purposes.
 */
[RequireComponent(typeof(Rigidbody))]
public class RigidBodyDebugger: MonoBehaviour {
    private Rigidbody rb;
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }
    void OnGUI()  {
        GUIStyle fontSize = new GUIStyle(GUI.skin.GetStyle("label"));
        fontSize.fontSize = 16;
        fontSize.normal.textColor = Color.black;
        GUI.Label(new Rect(25, 0, 200, 50), "Is sleeping?: " + rb.IsSleeping(), fontSize);
        GUI.Label(new Rect(25, 25, 200, 50), "Is kinematic?: " + rb.isKinematic, fontSize);
        GUI.Label(new Rect(25, 50, 200, 50), "Use Gravity?: " + rb.useGravity, fontSize);
        GUI.Label(new Rect(25, 75,300, 50), "Velocity: (x=" + rb.velocity.x.ToString("F2")+" ,y="+rb.velocity.y.ToString("F2")+" ,z="+rb.velocity.z.ToString("F2")+")", fontSize);
    }

}

