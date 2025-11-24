using UnityEngine;

public class RotatingBackgroundShape : MonoBehaviour
{
    public float rotationSpeed = 20f;
    public float driftSpeedX = 0f;
    public float driftSpeedY = 0f;
    public float scalePulse = 0f;
    public float pulseSpeed = 2f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Rotation
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Drift
        transform.Translate(
            driftSpeedX * Time.deltaTime,
            driftSpeedY * Time.deltaTime,
            0
        );

        // Pulse
        if (scalePulse > 0)
        {
            float pulse = Mathf.Sin(Time.time * pulseSpeed) * scalePulse;
            transform.localScale = originalScale + Vector3.one * pulse;
        }
    }
}
