using UnityEngine;

public class Collectible : MonoBehaviour{
    private void OnTriggerEnter(Collider other) {
        ScoreCounter player = other.GetComponent<ScoreCounter>();
        if (player) {
            player.CollectCoin();
            Destroy(gameObject);
        }
    }
}
