using UnityEngine;
using TMPro;

public class TitleBeatGlow : MonoBehaviour
{
    public TextMeshProUGUI titleText;

    [Header("Movement")]
    public float moveAmount = 15f;        // how much it moves up/down
    public float moveSpeed = 2f;          // movement speed

    [Header("Glow")]
    public float glowIntensity = 1.5f;
    public float glowSpeed = 2f;

    Vector3 startPos;

    void Start()
    {
        if (titleText == null)
            titleText = GetComponent<TextMeshProUGUI>();

        startPos = transform.localPosition;
    }

    void Update()
    {
        float beat = Mathf.Sin(Time.time * moveSpeed);

        // MOVE UP + DOWN
        transform.localPosition = startPos + new Vector3(0, beat * moveAmount, 0);

        // TEXT GLOW
        float glow = (beat + 1) * 0.5f * glowIntensity;
        titleText.outlineWidth = glow;
    }
}
