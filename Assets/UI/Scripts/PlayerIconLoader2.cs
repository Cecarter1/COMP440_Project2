using UnityEngine;
using UnityEngine.UI;   // remove this if you use SpriteRenderer instead

public class PlayerIconLoader2 : MonoBehaviour
{
    public Sprite[] iconSprites;      // assign in Inspector
    public Image uiIcon;              // if using UI Image
    // public SpriteRenderer spriteRenderer; // if using SpriteRenderer instead

    void Start()
    {
        int index = PlayerPrefs.GetInt("SelectedIconIndex", 0);

        if (index < 0 || index >= iconSprites.Length)
        {
            index = 0; // safety
        }

        if (uiIcon != null)
        {
            uiIcon.sprite = iconSprites[index];
        }

        // If you’re using SpriteRenderer instead of Image:
        // if (spriteRenderer != null)
        //     spriteRenderer.sprite = iconSprites[index];
    }
}
