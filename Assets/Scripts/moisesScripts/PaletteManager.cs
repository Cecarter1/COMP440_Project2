using UnityEngine;

public class PaletteManager : MonoBehaviour
{
    public static PaletteManager Instance { get; private set; }

    [Header("Base Color (X region)")]
    public Color baseColor = new Color(0f, 0.35f, 0.35f); // tweak in Inspector

    [Header("Brightness Multipliers")]
    [Range(1f, 2f)] public float midMultiplier = 1.15f;   // Y region
    [Range(1f, 3f)] public float brightMultiplier = 1.35f; // Z region / walls

    public Color XColor => baseColor;
    public Color YColor => Brighten(baseColor, midMultiplier);
    public Color ZColor => Brighten(baseColor, brightMultiplier);

    public Color Dark => baseColor;          // keep these for camera, etc.
    public Color Bright => ZColor;           // for your core outline, etc.

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private Color Brighten(Color c, float mul)
    {
        Color.RGBToHSV(c, out float h, out float s, out float v);
        v = Mathf.Clamp01(v * mul);
        return Color.HSVToRGB(h, s, v);
    }
}
