using UnityEngine;


/**
 * This component prints the properties of its RigidBody, for debugging purposes.
 */
[RequireComponent(typeof(Rigidbody))]
public class RigidBodyDebugger: MonoBehaviour {

    [SerializeField] int fontSize = 16;
    [SerializeField] Color textColor = Color.black;

    private Rigidbody rb;
    private float startTime;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        startTime = Time.time;
    }

void OnGUI()  {
        GUIStyle fontStyle = new GUIStyle(GUI.skin.GetStyle("label"));
        fontStyle.fontSize = fontSize;
        fontStyle.normal.textColor = textColor;
        GUI.Label(new Rect(25, 0, 200, 50), "Is sleeping?: " + rb.IsSleeping(), fontStyle);
        GUI.Label(new Rect(25, 25, 200, 50), "Is kinematic?: " + rb.isKinematic, fontStyle);
        GUI.Label(new Rect(25, 50, 200, 50), "Use Gravity?: " + rb.useGravity, fontStyle);

        GUI.Label(new Rect(25, 75, 300, 50), "LinearSpeed: " + rb.velocity.ToString("F2"), fontStyle);
        GUI.Label(new Rect(25, 100, 300, 50), "AngularSpeed: " + rb.angularVelocity.ToString("F2"), fontStyle);
        GUI.Label(new Rect(25, 125, 300, 50), "Time: " + (Time.time - startTime).ToString("F2"), fontStyle);
    }

}

