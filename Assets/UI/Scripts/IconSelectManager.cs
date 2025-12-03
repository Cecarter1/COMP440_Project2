using UnityEngine;
using UnityEngine.SceneManagement;

public class IconSelectionManager : MonoBehaviour
{
    // Index in your icon array (0, 1, 2, ...)
    private int selectedIconIndex = 0;

    // Called by each icon button in the PreGame scene
    public void SelectIcon(int index)
    {
        selectedIconIndex = index;
        PlayerPrefs.SetInt("SelectedIconIndex", selectedIconIndex);
        PlayerPrefs.Save();

        Debug.Log("Icon selected: " + index);
    }

    // Called by a "Start Game" button AFTER an icon is picked
    public void StartGame(string sceneName)
    {
        // (optional safety) default to 0 if nothing is set
        if (!PlayerPrefs.HasKey("SelectedIconIndex"))
        {
            PlayerPrefs.SetInt("SelectedIconIndex", 0);
            PlayerPrefs.Save();
        }

        SceneManager.LoadScene("AhmadSampleScene");
    }
}
