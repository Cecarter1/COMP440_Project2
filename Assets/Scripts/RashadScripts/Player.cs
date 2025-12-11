using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement; // Added SceneManagement for Game Over

public class Player : MonoBehaviour
{
    // MOVEMENT/ORBIT (Your Responsibility)
    public float moveSpeed = 500f;
    private float movement;
    private float currentOrbitRadius = 5f; // P4 Required: Tracks the current orbit size

    // MANAGER REFERENCES (Unified)
    // NOTE: This now refers to the main GameManager class.
    public GameManager gameManager;
    public ScoreManager scoreManager;

    // SCORING/COMBO (Teammate's Logic)
    public bool combo = false;
    public int numCombo = 0;
    public int perfectValue = 500;
    public int okValue = 300;
    public int badValue = 100;
    public GameObject perfectEffect, okEffect, badEffect;


    private void Update()
    {
        // Continuous Rotation Input (Raw for snappier, less glidy movement)
        movement = Input.GetAxisRaw("Horizontal");

        // P4: Ensure the player always faces away from the center of the shape
        transform.up = transform.position.normalized;
    }

    private void FixedUpdate()
    {
        // Continuous Rotation Execution
        transform.RotateAround(Vector3.zero, Vector3.forward, movement * Time.fixedDeltaTime * -moveSpeed);
    }

    /// <summary>
    /// PUBLIC METHOD (P4 Requirement): Called by the GameManager to dynamically resize the player's orbit.
    /// This fixes the CS1061 error.
    /// </summary>
    /// <param name="newRadius">The new distance from the center.</param>
    public void SetOrbitRadius(float newRadius)
    {
        currentOrbitRadius = newRadius;

        // Get the current direction of the player relative to the center
        Vector3 direction = transform.position.normalized;

        // Immediately set the player's new position at the updated radius
        transform.position = direction * currentOrbitRadius;
    }


    // IMPORTANT: Adds scoring behavior to collisions (Teammate's original logic)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if managers are set (important for team safety)
        if (gameManager == null || scoreManager == null)
        {
            Debug.LogError("Player collision logic failed: GameManager or ScoreManager reference is missing!");
            return;
        }

        // NOTE: The GameManager fields gameActive/gameOver need to be public in your GameManager.cs for this to work!
        if (gameManager.gameActive == true && gameManager.gameOver == false)
        {
            // Deletes Perfect/Ok/Bad colliders (Logic Preserved)
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

            // Decides behavior depending on what has been hit (Logic Preserved)
            if (collision.tag == "Hexagon")
            {
                // gameManager.gameActive = false; // Need public fields in GameManager
                // gameManager.gameOver = true;   // Need public fields in GameManager
                gameObject.SetActive(false);
                scoreManager.GameOver(); // Assumes this handles scene load
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
                // ... (Ok Logic Preserved)
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
                // ... (Bad Logic Preserved)
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