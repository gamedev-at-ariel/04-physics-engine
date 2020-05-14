using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This component measures the number of calls to Update and FixedUpdate per second.
 */
public class UpdateCounter: MonoBehaviour {
    private float updateCount = 0;
    private float fixedUpdateCount = 0;
    private float updateUpdateCountPerSecond;
    private float updateFixedUpdateCountPerSecond;
    private GUIStyle fontSize;

    void Awake() {
        // Uncommenting this will cause framerate to drop to 10 frames per second.
        // This will mean that FixedUpdate is called more often than Update.
        //Application.targetFrameRate = 10;
        StartCoroutine(Loop());

        fontSize = new GUIStyle(GUI.skin.GetStyle("label"));
        fontSize.fontSize = 24;
        fontSize.normal.textColor = Color.black;
    }

    // Count the number of calls to Update.
    void Update() {
        updateCount += 1;
    }

    // Count the number of calls to FixedUpdate.
    void FixedUpdate() {
        fixedUpdateCount += 1;
    }

    // Show the number of calls to both messages.
    void OnGUI() {
        GUI.Label(new Rect(100, 0, 200, 50), "Update: " + updateUpdateCountPerSecond.ToString(), fontSize);
        GUI.Label(new Rect(100, 50, 200, 50), "FixedUpdate: " + updateFixedUpdateCountPerSecond.ToString(), fontSize);
    }

    // Update both CountsPerSecond values every second.
    IEnumerator Loop() {
        while (true) {
            yield return new WaitForSeconds(1);
            updateUpdateCountPerSecond = updateCount;
            updateFixedUpdateCountPerSecond = fixedUpdateCount;
            updateCount = 0;
            fixedUpdateCount = 0;
        }
    }
}
