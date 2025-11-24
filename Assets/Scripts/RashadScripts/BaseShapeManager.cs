using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class BaseShapeManager : MonoBehaviour
{
    [Header("Shape Properties")]
    public float targetRadius = 2f; // The ultimate radius the shape should be
    public float rotationSpeed = 100f; // Degrees per second
    public float resizeDuration = 0.5f; // Time for the shape to smoothly resize

    // Public list of calculated snap points (for Player integration)
    public List<Vector3> SnapPoints { get; private set; } = new List<Vector3>();

    private LineRenderer lineRenderer;
    private int currentSides = 0;
    private float currentRadius; // The radius used for calculation
    private float resizeStartTime;
    private float initialRadius;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.loop = true;
        currentRadius = targetRadius; // Start at target radius
    }

    void Update()
    {
        // Continuous rotation
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Handle smooth resizing if targetRadius is different from currentRadius
        if (currentRadius != targetRadius)
        {
            SmoothResize();
        }

        // Re-calculate the geometry every frame if resizing, or whenever the shape changes.
        // This ensures the player SnapPoints are updated as the size changes.
        if (currentSides > 0)
        {
            CalculateVertices(currentSides, currentRadius);
        }
    }

    /// <summary>
    /// Triggered by GameManager to start the smooth resize and geometry change.
    /// </summary>
    public void GenerateNewShape(int sides, float newTargetRadius = 5f)
    {
        // 1. Store resizing parameters
        initialRadius = currentRadius;
        targetRadius = newTargetRadius;
        resizeStartTime = Time.time;
        currentSides = sides;

        // 2. Immediately calculate vertices using the initial radius for a smooth start
        CalculateVertices(currentSides, initialRadius);
    }

    /// <summary>
    /// Smoothly interpolates the current size towards the target size.
    /// </summary>
    private void SmoothResize()
    {
        float t = (Time.time - resizeStartTime) / resizeDuration;

        // Use an Ease-Out function for a smooth feel
        t = Mathf.SmoothStep(0f, 1f, t);

        currentRadius = Mathf.Lerp(initialRadius, targetRadius, t);

        if (t >= 1f)
        {
            currentRadius = targetRadius;
        }
    }


    /// <summary>
    /// Recalculates the shape geometry based on current sides and radius.
    /// </summary>
    private void CalculateVertices(int sides, float radius)
    {
        lineRenderer.positionCount = sides;
        SnapPoints.Clear();

        float angleStep = 360f / sides;
        float startAngle = 90f;

        for (int i = 0; i < sides; i++)
        {
            float currentAngle = startAngle + (i * angleStep);
            float angleInRadians = currentAngle * Mathf.Deg2Rad;

            float x = radius * Mathf.Cos(angleInRadians);
            float y = radius * Mathf.Sin(angleInRadians);

            Vector3 vertexPosition = new Vector3(x, y, 0f);
            lineRenderer.SetPosition(i, vertexPosition);

            // Store the snap point (vertex) for the player
            SnapPoints.Add(vertexPosition);
        }
    }
}