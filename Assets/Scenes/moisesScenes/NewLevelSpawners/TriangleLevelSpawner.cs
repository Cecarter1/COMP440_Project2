using UnityEngine;

public class TriangleLevelSpawner : MonoBehaviour
{
    [Header("Music Sync Settings")]
    public float songBPM = 120f;           // Beats per minute of the track
    public int beatsPerSpawn = 4;          // Spawn every 4 beats

    [Header("Obstacle Spawn Settings")]
    public GameObject triPrefab;
    public float minScaleToSpawnNext = 10f;

    private float beatInterval;            // Time between beats in seconds
    private float spawnInterval;           // Time between spawns
    private float nextSpawnTime = 0f;

    void Start()
    {
        beatInterval = 60f / songBPM;                   // seconds per beat
        spawnInterval = beatInterval * beatsPerSpawn;   // seconds per 4 beats
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime && CanSpawn())
        {
            SpawnObstacle();
            nextSpawnTime += spawnInterval;
        }
    }

    private void SpawnObstacle()
    {
        Instantiate(triPrefab, Vector3.zero, Quaternion.identity);
    }

    private bool CanSpawn()
    {
        if (TriangleObs.Active.Count == 0)
            return true;

        foreach (var obs in TriangleObs.Active)
        {
            if (obs != null && obs.CurrentScale > minScaleToSpawnNext)
                return false;
        }

        return true;
    }
}

