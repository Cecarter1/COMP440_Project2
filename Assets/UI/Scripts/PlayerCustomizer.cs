using UnityEngine;

public class PlayerCustomizer : MonoBehaviour
{
    public void SelectIcon(int id)
    {
        PlayerPrefs.SetInt("selected_icon", id);
        PlayerPrefs.Save();
        Debug.Log("Saved Icon: " + id);
    }
}
