using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TestPlayer : MonoBehaviour
{
    public float moveSpeed = 500f;
    private float movement;
    public bool combo = false;
    public int numCombo = 0;
    public ScoreManager scoreManager;
    public int maxPoints = 0;

    private void Update()
    {
        movement = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, movement * Time.fixedDeltaTime * -moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            foreach (Transform child in collision.transform.parent)
            {
                Destroy(child.gameObject);
            }
            maxPoints += 500;
        }

        if (collision.tag == "Hexagon")
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Hit");
        }
        else if (collision.tag == "Perfect")
        {
            Debug.Log("Perfect");
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
            Debug.Log("Ok");
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
            Debug.Log("Bad");
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
