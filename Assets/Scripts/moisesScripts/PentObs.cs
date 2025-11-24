using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class PentObs : MonoBehaviour   // you can rename to PentagonObs if you want
{
    [Header("Scaling")]
    public float startScale = 25f;     // how big it starts
    public float shrinkSpeed = 3f;     // shrink speed
    public float destroyScale = 0.5f;  // size at which it’s destroyed

    [Header("Alignment")]
    [Tooltip("Reference to the center/base shape (pentagon now).")]
    public Transform centerShape;      // drag your BaseShapeManager pentagon object here

    [Tooltip("Number of lanes/sides (5 for pentagon, 4 for square, 3 for triangle, etc.).")]
    public int lanes = 5;              // 5 sides -> 0°, 72°, 144°, 216°, 288°

    private LineRenderer line;

    // --- Static list of all active obstacles ---
    public static readonly List<PentObs> Active = new List<PentObs>();

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
        line.useWorldSpace = false;    // so scaling works with LineRenderer
    }

    void Start()
    {
        // 1. Pick a random lane (which side the gap will be on)
        int laneIndex = Random.Range(0, lanes);
        float laneAngle = laneIndex * (360f / lanes); // 0,72,144,216,288 for pentagon

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
