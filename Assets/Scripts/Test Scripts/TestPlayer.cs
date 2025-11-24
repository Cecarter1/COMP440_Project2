using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public float moveSpeed = 500f;
    private float movement;

    // Important variables
    public bool combo = false; // Combo currently happening
    public int numCombo = 0; // Current number of run
    public ScoreManager scoreManager;
    public TestGameManager gameManager;

    private void Update()
    {
        movement = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, movement * Time.fixedDeltaTime * -moveSpeed);
    }

    // IMPORTANT: Adds scoring behavior to collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameManager.gameActive == true && gameManager.gameOver == false)
        {
            // Deletes Perfect/Ok/Bad colliders
            if (collision.gameObject.layer == 6)
            {
                foreach (Transform child in collision.transform.parent)
                {
                    Destroy(child.gameObject);
                }
            }
            else
            {
                foreach (Transform child in collision.transform)
                {
                    Destroy(child.gameObject);
                }
            }

            // Decides behavior depending on what has been hit
            if (collision.tag == "Hexagon")
            {
                gameManager.gameActive = false;
                gameManager.gameOver = true;
                scoreManager.GameOver();
            }
            else if (collision.tag == "Perfect")
            {
                scoreManager.AddScore(500);
                scoreManager.numPerfect++;
                numCombo += 1;

                if (numCombo > 1)
                {
                    combo = true;
                }
            }
            else if (collision.tag == "Ok")
            {
                scoreManager.AddScore(300);
                scoreManager.numOk++;

                if (combo)
                {
                    scoreManager.CalculateCombo(numCombo);
                }

                combo = false;
                numCombo = 0;
            }
            else if (collision.tag == "Bad")
            {
                scoreManager.AddScore(100);
                scoreManager.numBad++;

                if (combo)
                {
                    scoreManager.CalculateCombo(numCombo);
                }

                combo = false;
                numCombo = 0;
            }
        }
    }
}
