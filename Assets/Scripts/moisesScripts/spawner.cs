using UnityEngine;

public class spawner : MonoBehaviour
{
    public float spawnRate = 1f;
    public GameObject triPrefab;

    [Header("Spawn Spacing")]
    [Tooltip("Don't spawn a new obstacle while any existing one is larger than this scale.")]
    public float minScaleToSpawnNext = 10f;

    private float nextSpawnTime = 0f;

    void Update()
    {
        if (Time.time >= nextSpawnTime && CanSpawn())
        {
            Instantiate(triPrefab, Vector3.zero, Quaternion.identity);
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
    }

    /// <summary>
    /// Only allow a new spawn if all active TriangleObs have shrunk enough.
    /// </summary>
    private bool CanSpawn()
    {
        // If there are no obstacles yet, it's always safe
        if (TriangleObs.Active.Count == 0)
            return true;

        foreach (var obs in TriangleObs.Active)
        {
            if (obs != null && obs.CurrentScale > minScaleToSpawnNext)
            {
                // At least one obstacle is still big -> delay spawning
                return false;
            }
        }

        return true;
    }
}
