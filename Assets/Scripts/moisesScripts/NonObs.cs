using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class NonObs : MonoBehaviour   // 9 sided obstacle
{
    [Header("Scaling")]
    public float startScale = 25f;     // how big it starts
    public float shrinkSpeed = 3f;     // shrink speed
    public float destroyScale = 0.5f;  // size at which it is destroyed

    [Header("Alignment")]
    [Tooltip("Reference to the center/base shape (nonagon now).")]
    public Transform centerShape;      // drag your BaseShapeManager nonagon here

    [Tooltip("Number of lanes/sides (9 for nonagon).")]
    public int lanes = 9;              // 9 sides -> 0, 40, 80, 120, etc.

    private LineRenderer line;

    // --- Static list of all active obstacles ---
    public static readonly List<NonObs> Active = new List<NonObs>();

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
        float laneAngle = laneIndex * (360f / lanes);  // 40° increments for nonagon

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
