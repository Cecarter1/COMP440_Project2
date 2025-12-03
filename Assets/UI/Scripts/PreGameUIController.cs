using UnityEngine;

public class PreGameUIController : MonoBehaviour
{
    public GameObject iconSelectPanel;

    // Called when the big "SELECT ICON" button is pressed
    public void OnSelectIconButton()
    {
        iconSelectPanel.SetActive(true);
    }

    // Optional: called by a Close / Back button on the icon panel
    public void CloseIconPanel()
    {
        iconSelectPanel.SetActive(false);
    }
}
