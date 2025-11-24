using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraPalette : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        ApplyColor();
    }

    private void OnValidate()
    {
        if (cam == null) cam = GetComponent<Camera>();
        ApplyColor();
    }

    private void ApplyColor()
    {
        if (PaletteManager.Instance == null) return;
        cam.backgroundColor = PaletteManager.Instance.XColor; // X region base
    }
}
