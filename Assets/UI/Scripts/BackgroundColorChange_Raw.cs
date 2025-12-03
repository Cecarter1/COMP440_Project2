using UnityEngine;
using UnityEngine.UI;

public class BackgroundColorChanger_Raw : MonoBehaviour
{
    public RawImage backgroundRawImage;
    public Color[] colors;
    public float changeDuration = 3f;

    private int currentIndex = 0;
    private int nextIndex = 1;
    private float t = 0f;

    void Awake()
    {
        if (backgroundRawImage == null)
            backgroundRawImage = GetComponent<RawImage>();
    }

    void Update()
    {
        if (backgroundRawImage == null || colors.Length < 2)
            return;

        t += Time.deltaTime / changeDuration;

        backgroundRawImage.color = Color.Lerp(
            colors[currentIndex],
            colors[nextIndex],
            t
        );

        if (t >= 1f)
        {
            t = 0f;
            currentIndex = nextIndex;
            nextIndex = (nextIndex + 1) % colors.Length;
        }
    }
}
