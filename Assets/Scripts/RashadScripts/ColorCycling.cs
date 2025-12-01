using UnityEngine;

[System.Serializable]
public struct ColorPalette
{
    [Tooltip("The name of the theme (e.g., 'Blue Grid').")]
    public string themeName;

    [Tooltip("Background Color (Camera Clear Color).")]
    public Color backgroundColor;

    [Tooltip("Primary Accent Color (Base Shape emission/LineRenderer).")]
    public Color primaryColor;

    [Tooltip("Obstacle/Core Color (Contrast color).")]
    public Color secondaryColor;
}


public class ColorCycling : MonoBehaviour
{
    // Singleton instance for easy access (used in GameManager)
    public static ColorCycling Instance { get; private set; }

    [Header("Color Data (4a & 4b)")]
    public ColorPalette[] palettes;
    public float transitionTime = 1.0f;

    [Header("References")]
    public Camera mainCamera;
    public BaseShapeManager shapeManager; // To access the LineRenderer material

    private Color currentBgColor;
    private Color targetBgColor;
    private Color currentPrimaryColor;
    private Color targetPrimaryColor;

    private float startTime;
    private Material shapeMaterial;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        // Get the Material instance from the LineRenderer (IMPORTANT: use sharedMaterial or get a new instance)
        shapeMaterial = shapeManager.GetComponent<LineRenderer>().material;

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Initialize colors with the first palette
        if (palettes.Length > 0)
        {
            currentBgColor = targetBgColor = palettes[0].backgroundColor;
            currentPrimaryColor = targetPrimaryColor = palettes[0].primaryColor;
            ApplyColors(palettes[0].backgroundColor, palettes[0].primaryColor);
        }
    }

    void Update()
    {
        // 1. Smoothly transition colors
        if (currentBgColor != targetBgColor)
        {
            float t = (Time.time - startTime) / transitionTime;

            // Smoothly interpolate colors
            Color lerpedBg = Color.Lerp(currentBgColor, targetBgColor, t);
            Color lerpedPrimary = Color.Lerp(currentPrimaryColor, targetPrimaryColor, t);

            // Apply the interpolated colors
            ApplyColors(lerpedBg, lerpedPrimary);

            if (t >= 1f)
            {
                currentBgColor = targetBgColor;
                currentPrimaryColor = targetPrimaryColor;
            }
        }

        // 2. Dynamic Neon Cycling (4c): Continuous, subtle pulse/shift on the primary color
        // This makes the neon aesthetic feel alive
        float hueShift = Mathf.Sin(Time.time * 2f) * 0.01f; // Subtle, slow shift
        shapeMaterial.SetColor("_EmissionColor", currentPrimaryColor * (1f + hueShift));
    }


    /// <summary>
    /// Applies the current colors to the environment elements.
    /// </summary>
    private void ApplyColors(Color bgColor, Color primaryColor)
    {
        mainCamera.backgroundColor = bgColor;

        // Assuming your BaseShape material uses an Emission property
        // For a neon effect, set the color and give it some intensity (HDR color).
        shapeMaterial.SetColor("_Color", primaryColor);
        shapeMaterial.SetColor("_EmissionColor", primaryColor * 2f); // *2f for HDR glow
    }


    /// <summary>
    /// Called by the GameManager to trigger a phase-based palette shift.
    /// </summary>
    /// <param name="paletteIndex">The index of the palette to transition to.</param>
    public void TriggerColorShift(int paletteIndex)
    {
        if (paletteIndex >= 0 && paletteIndex < palettes.Length)
        {
            // Start the transition
            startTime = Time.time;

            // Set the current colors to the final lerped colors from the last frame
            currentBgColor = mainCamera.backgroundColor;
            currentPrimaryColor = shapeMaterial.GetColor("_EmissionColor") / 2f;

            // Set the target colors for the new phase
            targetBgColor = palettes[paletteIndex].backgroundColor;
            targetPrimaryColor = palettes[paletteIndex].primaryColor;
        }
    }
}
