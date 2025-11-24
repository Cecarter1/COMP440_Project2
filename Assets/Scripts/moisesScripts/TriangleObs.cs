using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class TriangleObs : MonoBehaviour
{
    [Header("Scaling")]
    public float startScale = 25f;     // how big it starts
    public float shrinkSpeed = 3f;     // shrink speed
    public float destroyScale = 0.5f;  // size at which it’s destroyed

    [Header("Alignment")]
    [Tooltip("Reference to the center/base triangle (optional).")]
    public Transform centerShape;      // drag your BaseShapeManager object here

    [Tooltip("How many lanes/sides (3 for triangle).")]
    public int lanes = 3;              // 3 sides -> 0°, 120°, 240°

    private LineRenderer line;

    // --- Static list of all active obstacles ---
    public static readonly List<TriangleObs> Active = new List<TriangleObs>();

    public float CurrentScale => transform.localScale.x;

    void OnEnable()
    {
        Active.Add(this);
    }

    void OnDisable()
    {
        Active.Remove(this);
    }

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = false;    // so scaling works
    }

    void Start()
    {
        // 1. Pick a random lane (which side the gap will be on)
        int laneIndex = Random.Range(0, lanes);
        float laneAngle = laneIndex * (360f / lanes); // 0, 120, 240 for triangle

        // 2. Base orientation (optionally match the current center shape rotation)
        Quaternion baseRot = centerShape != null ? centerShape.rotation : Quaternion.identity;

        // 3. Final rotation for this obstacle (set ONCE)
        transform.rotation = baseRot * Quaternion.Euler(0f, 0f, laneAngle);

        // 4. Start very large so it comes from off-screen
        transform.localScale = Vector3.one * startScale;
    }

    void Update()
    {
        // Only shrink — rotation stays what we set in Start()

        float s = transform.localScale.x - shrinkSpeed * Time.deltaTime;
        s = Mathf.Max(0f, s); // clamp to avoid negative

        transform.localScale = new Vector3(s, s, 1f);

        if (s <= destroyScale)
        {
            Destroy(gameObject);
        }
    }
}
