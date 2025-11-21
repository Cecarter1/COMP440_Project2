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

    [Header("References")]
    public BaseShapeManager shapeManager; // Reference to the shape script

    private float survivalTimer = 0f;
    private int currentPhase = 0;
    private float nextShapeTime;

    // Static property for other scripts to read the current game state
    public static int CurrentSides { get; private set; }

    void Start()
    {
        // Safety check
        if (shapeManager == null)
        {
            Debug.LogError("BaseShapeManager reference is missing on GameManager!");
            enabled = false;
            return;
        }

        // Initialize the game state
        CurrentSides = startingSides;
        shapeManager.GenerateNewShape(CurrentSides);
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
            nextShapeTime = survivalTimer + timeToNextShape; // Set the next trigger time
        }
    }

    /// <summary>
    /// Changes the base shape to the next level of difficulty.
    /// </summary>
    public void ProgressToNextShape()
    {
        if (CurrentSides < maxSides)
        {
            CurrentSides++;
            currentPhase++;

            // Seamlessly trigger the shape change
            shapeManager.GenerateNewShape(CurrentSides);

            // Optional: Trigger color cycle logic here for advanced feature
            // You can call your Hue/Color Cycling System script here
            // Example: GetComponent<ColorCycleManager>().ShiftPalette(CurrentSides);

            Debug.Log($"PROGRESSION: Shape changed to {CurrentSides} sides (Phase {currentPhase + 1}).");
        }
        else
        {
            // Optional: Once max sides is reached, increase rotation speed or complexity
            Debug.Log("MAX SIDES REACHED. Increasing difficulty parameters...");
            shapeManager.rotationSpeed *= 1.1f; // Example of speed tuning (for Challenge Variety)
        }
    }
}