using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Progression Settings")]
    [Tooltip("Time in seconds before the base shape changes.")]
    public float timeToNextShape = 60f;
    [Tooltip("Starting number of sides for the Base Shape (Triangle).")]
    public int startingSides = 3;
    [Tooltip("Maximum number of sides (Tridecagon).")]
    public int maxSides = 13;

    [Header("Custom Radius Settings")]
    [Tooltip("The radius of the Base Shape.")]
    public float baseShapeRadius = 5f;
    [Tooltip("The factor by which the radius shrinks each phase (e.g., 0.95 = 5% shrink).")]
    public float shrinkFactor = 0.95f;

    [Header("References")]
    public BaseShapeManager shapeManager;

    private float survivalTimer = 0f;
    private int currentPhase = 0;
    private float nextShapeTime;

    public static int CurrentSides { get; private set; }

    void Start()
    {
        // 1. Initial Setup and Safety Check
        if (shapeManager == null)
        {
            Debug.LogError("BaseShapeManager reference is missing on GameManager!");
            enabled = false;
            return;
        }

        // 2. Initialize the game state
        CurrentSides = startingSides;

        // Use the new method signature to set the initial shape and radius
        shapeManager.GenerateNewShape(CurrentSides, baseShapeRadius);
        nextShapeTime = timeToNextShape;

        Debug.Log("Game Started. Current Shape: Triangle.");
    }

    void Update()
    {
        // 1. Survival Time Tracking (for Caitlyn's scoring)
        survivalTimer += Time.deltaTime;

        // 2. Shape Progression Check
        if (survivalTimer >= nextShapeTime)
        {
            ProgressToNextShape();
            nextShapeTime = survivalTimer + timeToNextShape;
        }
    }

    /// <summary>
    /// Changes the base shape to the next level of difficulty (a. & b.)
    /// </summary>
    public void ProgressToNextShape()
    {
        if (CurrentSides < maxSides)
        {
            CurrentSides++;
            currentPhase++;

            // Calculate the new radius, shrinking it based on the number of phases passed.
            // This progressively reduces the player's available movement distance.
            float newRadius = baseShapeRadius * Mathf.Pow(shrinkFactor, currentPhase);

            // Trigger the shape change and smooth resize
            shapeManager.GenerateNewShape(CurrentSides, newRadius);

            // Optional: You can also increase the rotation speed here for difficulty scaling
            shapeManager.rotationSpeed *= 1.05f;

            Debug.Log($"PROGRESSION: Shape changed to {CurrentSides} sides (Phase {currentPhase + 1}). New Radius: {newRadius:F2}");
        }
        else
        {
            // Once max sides is reached, only speed and rotation increase
            Debug.Log("MAX SIDES REACHED. Increasing difficulty parameters...");
            shapeManager.rotationSpeed *= 1.1f;
        }
    }

    // Public method to reset the game state on Game Over (Ahmad's task)
    public void ResetGame()
    {
        survivalTimer = 0f;
        currentPhase = 0;
        CurrentSides = startingSides;
        nextShapeTime = timeToNextShape;
        shapeManager.rotationSpeed = 100f; // Reset to initial speed
        shapeManager.GenerateNewShape(CurrentSides, baseShapeRadius);
    }
}