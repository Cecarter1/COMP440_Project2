using UnityEngine;
using UnityEngine.UI;

public class BeatPulse : MonoBehaviour
{
    public Image pulseImage;
    public float pulseSpeed = 5f;
    public float maxAlpha = 0.25f;

    void Update()
    {
        Color c = pulseImage.color;
        c.a = Mathf.MoveTowards(c.a, 0f, Time.deltaTime * pulseSpeed);
        pulseImage.color = c;
    }

    public void Pulse()
    {
        Color c = pulseImage.color;
        c.a = maxAlpha;
        pulseImage.color = c;
    }
}
