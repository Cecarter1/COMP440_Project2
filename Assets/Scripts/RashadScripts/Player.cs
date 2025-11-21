using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Angular speed of rotation around the center (degrees per second).")]
    public float moveSpeed = 600f;

    private float movementInput = 0f;

    void Update()
    {
        // 1. Input Collection: Get input every frame.
        movementInput = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        // FixedUpdate is where orbital movement calculations happen.
        if (movementInput != 0)
        {
            // 2. The core orbital movement function.
            // Center: Vector3.zero (the BaseShape's position)
            // Axis: Vector3.forward (the Z-axis, correct for a 2D rotation plane)
            // Angle: The calculated rotation amount

            float rotationAmount = movementInput * Time.fixedDeltaTime * moveSpeed * -1f;

            transform.RotateAround(Vector3.zero, Vector3.forward, rotationAmount);
        }
    }

    /// <summary>
    /// Collision detection using 2D colliders set to Is Trigger.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // NOTE: Obstacles must have a Collider2D set to Is Trigger.
        Debug.Log("Collision detected! Game Over.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}