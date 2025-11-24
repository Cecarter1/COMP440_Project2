using UnityEngine;
using UnityEngine.UI;

public class PlayerIconLoader1 : MonoBehaviour
{
    [Header("Icon Image On HUD")]
    public Image playerIconImage;

    [Header("Available Icon Sprites")]
    public Sprite[] icons;   // assign in Inspector

    void Start()
    {
        // Get saved icon id (0 if nothing saved yet)
        int id = SaveSystem.LoadSelectedIcon();
        Debug.Log("PlayerIconLoader: Loaded icon id = " + id);

        if (icons == null || icons.Length == 0)
        {
            Debug.LogWarning("PlayerIconLoader: No icons assigned in Inspector!");
            return;
        }

        // safety clamp: if id is out of range, use 0
        if (id < 0 || id >= icons.Length)
        {
            Debug.LogWarning("PlayerIconLoader: Icon id out of range, using 0 instead.");
            id = 0;
        }

        // set sprite
        playerIconImage.sprite = icons[id];
    }
}
