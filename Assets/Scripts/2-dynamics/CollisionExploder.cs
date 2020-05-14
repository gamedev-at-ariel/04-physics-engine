using System.Collections;
using UnityEngine;


/**
 * This component triggers an explosion effect and destroys its object 
 * whenever its object collides with something in a velocity above the threshold.
 */
[RequireComponent(typeof(Rigidbody2D))]
public class CollisionExploder: MonoBehaviour {
    [SerializeField] float minVelocityForExplosion = 1.0f;
    [SerializeField] GameObject explosionEffect = null;
    [SerializeField] float explosionEffectTime = 0.68f;

    private Rigidbody2D rb;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(gameObject.name + " collides with "+ collision.collider.name 
            + " at velocity " + rb.velocity + " [m/s]");
        if (rb.velocity.magnitude > minVelocityForExplosion) {
            StartCoroutine(Explosion());
        }
    }

    IEnumerator Explosion() {
        explosionEffect.SetActive(true);
        yield return new WaitForSeconds(explosionEffectTime);
        Destroy(this.gameObject);
    }
}
