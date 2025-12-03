using UnityEngine;
using UnityEngine.UI;

public class BackgroundColorChanger : MonoBehaviour
{
    private Image backgroundImage;

    [Header("Colors to cycle through")]
    public Color[] colors;

    [Header("Time between color changes (seconds)")]
    public float changeInterval = 2f;

    private int currentIndex = 0;
    private float timer = 0f;

    void Awake()
    {
        // Automatically grab the Image component on THIS object
        backgroundImage = GetComponent<Image>();
    }

    void Start()
    {
        if (backgroundImage != null && colors.Length > 0)
        {
            backgroundImage.color = colors[0];
        }
    }

    void Update()
    {
        if (backgroundImage == null || colors.Length == 0)
            return;

        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            timer = 0f;
            currentIndex = (currentIndex + 1) % colors.Length;
            backgroundImage.color = colors[currentIndex];
        }
    }
}
