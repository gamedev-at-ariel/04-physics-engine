using System.Collections;
using UnityEngine;

/**
 * This component represents the floor of the game area.
 * When the player hits the floor, it is sent back to the respawn point.
 */
public class Floor : MonoBehaviour {
    [SerializeField] Transform respawnPoint = null;

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<CharacterController>()) {
            var controller = other.GetComponent<CharacterController>();
            if (controller) {
                controller.enabled = false;
            }
            other.transform.position = respawnPoint.position;
            StartCoroutine(EnableController(controller));
        }
    }

    private IEnumerator EnableController(CharacterController controller) {
        yield return new WaitForSeconds(0.5f);
        controller.enabled = true;
    }
}
