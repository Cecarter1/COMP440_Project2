using UnityEngine;

public class BackgroundBeatMove : MonoBehaviour
{
    public float pulseScale = 1.03f;     // small pulse
    public float pulseSpeed = 4f;        // faster or slower
    public float moveAmount = 15f;       // how much it drifts
    public float moveSpeed = 0.5f;       // slow drifting

    private Vector3 startPos;
    private Vector3 startScale;

    void Start()
    {
        startPos = transform.localPosition;
        startScale = transform.localScale;
    }

    void Update()
    {
        float pulse = Mathf.Sin(Time.time * pulseSpeed) * 0.5f + 0.5f;
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveAmount;

        transform.localScale = startScale * Mathf.Lerp(1f, pulseScale, pulse);

        transform.localPosition = startPos + new Vector3(offset, offset * 0.5f, 0f);
    }
}
