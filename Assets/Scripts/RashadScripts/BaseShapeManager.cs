using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BaseShapeManager : MonoBehaviour
{
    [Header("Shape Properties")]
    public float shapeRadius = 5f;
    public float rotationSpeed = 100f; // Degrees per second

    private LineRenderer lineRenderer;
    private int currentSides = 0;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        // Set up LineRenderer for a continuous loop
        lineRenderer.loop = true;
    }

    void Update()
    {
        // Continuous rotation (3b)
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Generates the vertices for a regular N-sided polygon.
    /// </summary>
    public void GenerateNewShape(int sides)
    {
        if (sides < 3) return;
        currentSides = sides;

        // The number of points is equal to the number of sides (N) plus one more 
        // to complete the loop, but since LineRenderer.loop is true, we just need N points.
        lineRenderer.positionCount = sides;

        Vector3[] vertices = new Vector3[sides];
        float angleStep = 360f / sides;

        // Offset the starting angle so a flat side is at the bottom (optional aesthetic choice)
        float startAngle = 90f;

        for (int i = 0; i < sides; i++)
        {
            float currentAngle = startAngle + (i * angleStep);
            // Convert angle to radians for trigonometric functions
            float angleInRadians = currentAngle * Mathf.Deg2Rad;

            // Calculate vertex position using trigonometry
            float x = shapeRadius * Mathf.Cos(angleInRadians);
            float y = shapeRadius * Mathf.Sin(angleInRadians);

            // Store the vertex (Z is 0 for 2D plane)
            vertices[i] = new Vector3(x, y, 0f);
        }

        lineRenderer.SetPositions(vertices);

        // Notify the player manager (Moises's part) that the snap points have changed
        // This is a placeholder for team integration:
        // FindObjectOfType<PlayerMovement>().UpdateSnapPoints(vertices);
    }
}