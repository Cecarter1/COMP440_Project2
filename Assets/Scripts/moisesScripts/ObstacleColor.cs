using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ObstacleColor : MonoBehaviour
{
    void Awake()
    {
        var sr = GetComponent<SpriteRenderer>();
        if (PaletteManager.Instance != null)
        {
            sr.color = PaletteManager.Instance.Bright;
        }
    }
}
