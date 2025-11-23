using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public float spawnRate = 1f;
    public GameObject hexagonPrefab;
    private float spawnTimer = 1f;

    private void Update()
    {
        if (Time.time >= spawnTimer)
        {
            Instantiate(hexagonPrefab, Vector3.zero, Quaternion.identity);
            spawnTimer = Time.time + 1f / spawnRate;
        }
    }
}
