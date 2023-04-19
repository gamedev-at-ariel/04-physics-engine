using UnityEngine;


/**
 * This component prints the properties of its RigidBody, for debugging purposes.
 */
[RequireComponent(typeof(Rigidbody))]
public class RigidBodyDebugger: MonoBehaviour {

    [SerializeField] int fontSize = 16;
    [SerializeField] Color textColor = Color.black;

    private int xLeft = 25;
    private int widthLetters = 20;

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
        int width = fontSize * widthLetters;
        int lineHeight = (int)(fontSize * 1.5);
        GUI.Label(new Rect(xLeft, 0,           width, lineHeight), "Is sleeping?: " + rb.IsSleeping(), fontStyle);
        GUI.Label(new Rect(xLeft, 1* lineHeight, width, lineHeight), "Is kinematic?: " + rb.isKinematic, fontStyle);
        GUI.Label(new Rect(xLeft, 2* lineHeight, width, lineHeight), "Use Gravity?: " + rb.useGravity, fontStyle);

        GUI.Label(new Rect(xLeft, 3* lineHeight, width, lineHeight), "LinearSpeed: " + rb.velocity.ToString("F2"), fontStyle);
        GUI.Label(new Rect(xLeft, 4* lineHeight, width, lineHeight), "AngularSpeed: " + rb.angularVelocity.ToString("F2"), fontStyle);
        GUI.Label(new Rect(xLeft, 5* lineHeight, width, lineHeight), "Time: " + (Time.time - startTime).ToString("F2"), fontStyle);
    }

}

