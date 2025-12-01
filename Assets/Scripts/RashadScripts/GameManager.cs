using UnityEngine;
using System.Linq; 
using System.Collections; 

public class GameManager : MonoBehaviour
{
    // FIX: Added required public state variables for Player collision logic (Teammate's requirement)
    [Header("Game State")]
    public bool gameActive = true;
    public bool gameOver = false;

    [Header("Progression Settings (P3 Default)")]
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

    [Header("Challenge Variety (P4)")]
    [Tooltip("Define all tuning parameters for each mode.")]
    public ModeSettings[] availableModes;

    public GameMode selectedMode = GameMode.Standard;

    private float initialRotationSpeed = 100f;
    private float speedIncreaseFactor = 1.05f;

    [Header("References")]
    public BaseShapeManager shapeManager;

    // FIX 1: Reference type is now the correct component: Player
    private Player playerScript;

    private float survivalTimer = 0f;
    private int currentPhase = 0;
    private float nextShapeTime;

    public int CurrentSides { get; private set; } = 3;


    void Awake()
    {
        // 1. P4: Auto-Find the Player script in the scene
        // FIX 2: Using the correct, non-obsolete function and looking for the correct class (Player)
        playerScript = FindFirstObjectByType<Player>();
        if (playerScript == null)
        {
            Debug.LogError("FATAL ERROR: Could not find Player component (Player.cs) in the scene! Cannot initialize game.");
            enabled = false;
            return;
        }

        // 2. P4: Check for custom mode settings and override defaults if found
        ModeSettings modeOverride = availableModes.FirstOrDefault(m => m.mode == selectedMode);

        if (modeOverride.timeToNextShape != 0)
        {
            timeToNextShape = modeOverride.timeToNextShape;
            initialRotationSpeed = modeOverride.initialRotationSpeed;
            speedIncreaseFactor = modeOverride.speedIncreaseFactor;

            if (ColorCycling.Instance != null)
            {
                ColorCycling.Instance.TriggerColorShift(modeOverride.colorPaletteIndex);
            }
        }
        else
        {
            initialRotationSpeed = 100f;
            speedIncreaseFactor = 1.05f;

            if (ColorCycling.Instance != null && ColorCycling.Instance.palettes.Length > 0)
            {
                ColorCycling.Instance.TriggerColorShift(0);
            }
        }
    }


    void Start()
    {
        if (shapeManager == null)
        {
            Debug.LogError("BaseShapeManager reference is missing! Drag BaseShape into the slot.");
            enabled = false;
            return;
        }

        gameActive = true;
        gameOver = false;

        CurrentSides = startingSides;
        nextShapeTime = timeToNextShape;

        shapeManager.rotationSpeed = initialRotationSpeed;
        shapeManager.GenerateNewShape(CurrentSides, baseShapeRadius);

        playerScript.SetOrbitRadius(baseShapeRadius);
    }

    void Update()
    {
        if (gameActive && !gameOver)
        {
            survivalTimer += Time.deltaTime;

            if (survivalTimer >= nextShapeTime)
            {
                ProgressToNextShape();
                nextShapeTime = survivalTimer + timeToNextShape;
            }
        }
    }

    public void ProgressToNextShape()
    {
        if (CurrentSides < maxSides)
        {
            CurrentSides++;
            currentPhase++;

            shapeManager.rotationSpeed *= speedIncreaseFactor;

            float newRadius = baseShapeRadius * Mathf.Pow(shrinkFactor, currentPhase);

            shapeManager.GenerateNewShape(CurrentSides, newRadius);
            playerScript.SetOrbitRadius(newRadius);

            if (ColorCycling.Instance != null && ColorCycling.Instance.palettes.Length > 0)
            {
                int nextPaletteIndex = currentPhase % ColorCycling.Instance.palettes.Length;
                ColorCycling.Instance.TriggerColorShift(nextPaletteIndex);
            }
        }
        else
        {
            shapeManager.rotationSpeed *= speedIncreaseFactor * 1.1f;
        }
    }

    public void ResetGame()
    {
        gameActive = true;
        gameOver = false;

        survivalTimer = 0f;
        currentPhase = 0;
        CurrentSides = startingSides;
        nextShapeTime = timeToNextShape;
        shapeManager.rotationSpeed = initialRotationSpeed;
        shapeManager.GenerateNewShape(CurrentSides, baseShapeRadius);
        playerScript.SetOrbitRadius(baseShapeRadius);
    }

    public void SetGameMode(GameMode newMode)
    {
        selectedMode = newMode;
    }
}