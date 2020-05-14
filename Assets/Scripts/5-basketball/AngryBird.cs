using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


/**
 * This component lets the player pull the ball and release it.
 */
public class AngryBird: MonoBehaviour {
    [SerializeField] Rigidbody2D hook = null;
    [SerializeField] float releaseTime = .15f;
    [SerializeField] float maxDragDistance = 2f;

    static public int HIGH_SCORE = 0;
    static public int SCORE_FROM_PREV_ROUND = 0;

    private bool isMousePressed = false;
    private bool isScore = false;
    private int score = 0;

    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        score = HIGH_SCORE = PlayerPrefs.GetInt("BasketballScore");
    }

    void Update() {
        if (isMousePressed) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(mousePos, hook.position) > maxDragDistance)
                rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance;
            else
                rb.position = mousePos;
        }
    }

    private void OnMouseDown() {
        isMousePressed = true;
        rb.isKinematic = true;
    }

    private void OnMouseUp() {
        isMousePressed = false;
        rb.isKinematic = false;
        StartCoroutine(ReleaseBall());
    }

    IEnumerator ReleaseBall() {
        // Wait a short time, to let the physics engine operate the spring and give some initial speed to the ball.
        yield return new WaitForSeconds(releaseTime); 
        GetComponent<SpringJoint2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "boundery") {
            if (!isScore)
                SetScore(0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restart game
        } else if (other.tag == "Score") {
            if (!isScore) {
                isScore = true;
                SetScore(score + 1);
            }
        }
    }

    private void SetScore(int newScore) {
        score = newScore;
        PlayerPrefs.SetInt("BasketballScore", score);
        print("Score: " + score);
    }

    void OnGUI() {
        GUIStyle fontSize = new GUIStyle(GUI.skin.GetStyle("label"));
        fontSize.fontSize = 16;
        fontSize.normal.textColor = Color.black;
        GUI.Label(new Rect(70, 0, 150, 50), "Score: " + score, fontSize);
    }
}
