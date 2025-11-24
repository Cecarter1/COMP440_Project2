using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TestPlayer : MonoBehaviour
{
    public float moveSpeed = 500f;
    private float movement;

    // Important variables
    public bool combo = false; // Combo currently happening
    public int numCombo = 0; // Current number of run
    public ScoreManager scoreManager;
    public TestGameManager gameManager;
    public int perfectValue = 500;
    public int okValue = 300;
    public int badValue = 100;
    public GameObject perfectEffect, okEffect, badEffect;

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
                gameObject.SetActive(false);
                scoreManager.GameOver();
            }
            else if (collision.tag == "Perfect")
            {
                scoreManager.AddScore(perfectValue);
                scoreManager.numPerfect++;
                numCombo += 1;

                if (numCombo > 1)
                {
                    combo = true;
                }
                Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
            }
            else if (collision.tag == "Ok")
            {
                scoreManager.AddScore(okValue);
                scoreManager.numOk++;

                if (combo)
                {
                    scoreManager.CalculateCombo(numCombo);
                }

                combo = false;
                numCombo = 0;
                Instantiate(okEffect, transform.position, okEffect.transform.rotation);
            }
            else if (collision.tag == "Bad")
            {
                scoreManager.AddScore(badValue);
                scoreManager.numBad++;

                if (combo)
                {
                    scoreManager.CalculateCombo(numCombo);
                }

                combo = false;
                numCombo = 0;
                Instantiate(badEffect, transform.position, badEffect.transform.rotation);
            }

            //passParticles.Play();
        }
    }
}
