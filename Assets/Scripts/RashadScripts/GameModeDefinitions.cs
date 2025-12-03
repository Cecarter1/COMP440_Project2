using UnityEngine;

// Define the available game modes
public enum GameMode
{
    Standard, // Baseline difficulty
    Pulse,    // Obstacles blink (requires Moises's implementation)
    Flow      // High speed, tight timing
}

// A structure to hold the tuning parameters for each mode
[System.Serializable]
public struct ModeSettings
{
    public GameMode mode;

    [Header("Progression")]
    [Tooltip("Time in seconds before phase change.")]
    public float timeToNextShape;

    [Tooltip("Initial rotation speed of the base shape.")]
    public float initialRotationSpeed;

    [Tooltip("Factor by which rotation speed increases each phase.")]
    public float speedIncreaseFactor;

    [Header("Visuals")]
    [Tooltip("The unique color palette key/index for this mode.")]
    public int colorPaletteIndex;
}