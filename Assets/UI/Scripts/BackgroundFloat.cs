using UnityEngine;

public class BackgroundFloat : MonoBehaviour
{
    public float moveSpeed = 5f;       // horizontal/vertical movement speed
    public float moveAmount = 15f;     // how far it drifts
    public float scaleSpeed = 1f;      // "breathing" speed
    public float scaleAmount = 0.03f;  // how much it scales

    private Vector3 startPos;
    private Vector3 startScale;

    void Start()
    {
        startPos = transform.localPosition;
        startScale = transform.localScale;
    }

    void Update()
    {
        // Smooth drifting motion
        float x = Mathf.Sin(Time.time * moveSpeed) * moveAmount;
        float y = Mathf.Cos(Time.time * moveSpeed * 0.7f) * moveAmount;

        transform.localPosition = startPos + new Vector3(x, y, 0);

        // Breathing / scale animation
        float scale = 1 + Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;
        transform.localScale = startScale * scale;
    }
}
