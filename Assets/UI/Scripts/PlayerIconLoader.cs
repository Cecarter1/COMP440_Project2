using UnityEngine;
using UnityEngine.UI;

public class PlayerIconLoader : MonoBehaviour
{
    public Image playerIconImage;   // the HUD UI image
    public Sprite[] icons;          // length 3

    void Start()
    {
        int id = PlayerPrefs.GetInt("selected_icon", 0);
        Debug.Log("PlayerIconLoader: loaded id " + id);

        if (icons == null || icons.Length == 0)
        {
            Debug.LogWarning("No icons assigned!");
            return;
        }

        if (id < 0 || id >= icons.Length)
            id = 0;

        playerIconImage.sprite = icons[id];
    }
}
