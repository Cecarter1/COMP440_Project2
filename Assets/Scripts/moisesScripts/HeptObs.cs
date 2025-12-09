using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class HeptObs : MonoBehaviour   // 7 sided obstacle
{
    [Header("Scaling")]
    public float startScale = 25f;     // how big it starts
    public float shrinkSpeed = 3f;     // shrink speed
    public float destroyScale = 0.5f;  // size at which it is destroyed

    [Header("Alignment")]
    [Tooltip("Reference to the center/base shape (heptagon now).")]
    public Transform centerShape;      // drag your BaseShapeManager heptagon here

    [Tooltip("Number of lanes/sides (7 for heptagon).")]
    public int lanes = 7;              // 7 sides -> 0, ~51.43, ~102.86, etc.

    private LineRenderer line;

    // --- Static list of all active obstacles ---
    public static readonly List<HeptObs> Active = new List<HeptObs>();

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
        line.useWorldSpace = false;    // scaling affects the line
    }

    void Start()
    {
        // 1. Pick a random lane (which side the gap will be on)
        int laneIndex = Random.Range(0, lanes);
        float laneAngle = laneIndex * (360f / lanes);  // ~51.43° increments for heptagon

        // 2. Base orientation (match the rotating center shape)
        Quaternion baseRot = centerShape != null ? centerShape.rotation : Quaternion.identity;

        // 3. Apply final rotation once at spawn
        transform.rotation = baseRot * Quaternion.Euler(0f, 0f, laneAngle);

        // 4. Start very large so it shrinks inward
        transform.localScale = Vector3.one * startScale;
    }

    void Update()
    {
        // Shrink inward uniformly
        float s = transform.localScale.x - shrinkSpeed * Time.deltaTime;
        s = Mathf.Max(0f, s);

        transform.localScale = new Vector3(s, s, 1f);

        // Destroy when too small
        if (s <= destroyScale)
        {
            Destroy(gameObject);
        }
    }
}
