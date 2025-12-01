using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class BlinkController : MonoBehaviour
{
    // ADJUSTMENT 1: Increased duration for slower, safer pulsing
    [Tooltip("Time the obstacle stays visible (longer duration).")]
    public float visibleDuration = 0.4f; // Increased from 0.2s
    [Tooltip("Time the obstacle is invisible (longer duration).")]
    public float invisibleDuration = 0.5f; // Increased from 0.3s

    private LineRenderer obstacleRenderer;

    void Start()
    {
        obstacleRenderer = GetComponent<LineRenderer>();

        if (obstacleRenderer != null)
        {
            // Start the continuous blinking loop
            StartCoroutine(BlinkRoutine());
        }
        else
        {
            Debug.LogError("BlinkController FATAL: Could not find LineRenderer on obstacle!");
            // Self-destruct if the required component is missing
            Destroy(this);
        }
    }

    private IEnumerator BlinkRoutine()
    {
        while (true)
        {
            // 1. Visible Phase
            obstacleRenderer.enabled = true;
            yield return new WaitForSeconds(visibleDuration);

            // 2. Invisible Phase
            obstacleRenderer.enabled = false;
            yield return new WaitForSeconds(invisibleDuration);
        }
    }
}