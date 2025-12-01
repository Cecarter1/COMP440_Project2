using UnityEngine;

public class spawner : MonoBehaviour
{
    [Header("Spawn Control")]
    public float spawnRate = 1f;

    [Header("External References")]
    // Reference to the GameManager to check the current shape and phase
    public GameManager gameManager;

    [Header("Obstacle Prefabs (Index 0 = Tri, 1 = Square, etc.)")]
    // Array to hold all different shape obstacles
    public GameObject[] obstaclePrefabs;

    [Header("Spawn Spacing")]
    [Tooltip("Don't spawn a new obstacle while any existing one is larger than this scale.")]
    public float minScaleToSpawnNext = 10f;

    private float nextSpawnTime = 0f;

    void Start()
    {
        if (gameManager == null || obstaclePrefabs.Length == 0)
        {
            Debug.LogError("Spawner references are missing! Please link GameManager and fill the Obstacle Prefabs array.");
            enabled = false;
        }
    }

    void Update()
    {
        // Safety check to ensure the game is active (using GameManager state)
        if (gameManager == null || !gameManager.gameActive) return;

        if (Time.time >= nextSpawnTime && CanSpawn())
        {
            // 1. Determine the index based on the current shape side count
            int currentSides = gameManager.CurrentSides;
            int prefabIndex = currentSides - 3; // Index 0 = 3 sides, Index 1 = 4 sides

            if (prefabIndex >= 0 && prefabIndex < obstaclePrefabs.Length)
            {
                GameObject prefabToSpawn = obstaclePrefabs[prefabIndex];

                if (prefabToSpawn != null)
                {
                    // 2. Instantiate the correct obstacle for the current phase
                    GameObject newObstacle = Instantiate(prefabToSpawn, Vector3.zero, Quaternion.identity);

                    // 3. P4 IMPLEMENTATION: Check for Pulse Mode and apply blinking
                    if (gameManager.currentActiveMode == GameMode.Pulse)
                    {
                        // Attach the BlinkController component only if the mode is Pulse
                        newObstacle.AddComponent<BlinkController>();
                        Debug.Log("Pulse Mode: Added BlinkController to new obstacle.");
                    }

                    nextSpawnTime = Time.time + 1f / spawnRate;
                }
                else
                {
                    Debug.LogError($"Obstacle Prefab is missing for {currentSides} sides (Index {prefabIndex}). Ensure the array is filled in the Inspector.");
                }
            }
        }
    }

    /// <summary>
    /// Only allow a new spawn if all active TriangleObs have shrunk enough.
    /// (Teammate's original logic is preserved here)
    /// </summary>
    private bool CanSpawn()
    {
        // NOTE: This logic assumes 'TriangleObs.Active' is still accessible and tracks all obstacles
        if (TriangleObs.Active == null) return true;

        if (TriangleObs.Active.Count == 0)
            return true;

        foreach (var obs in TriangleObs.Active)
        {
            if (obs != null && obs.CurrentScale > minScaleToSpawnNext)
            {
                return false;
            }
        }

        return true;
    }
}