using UnityEngine;

public class CustomizeButtonHandler : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject playerCustomizerPanel;

    public void OpenCustomizer()
    {
        if (playerCustomizerPanel != null)
        {
            playerCustomizerPanel.SetActive(true);
            Debug.Log("Opening player customizer panel");
        }
        else
        {
            Debug.LogWarning("CustomizeButtonHandler: Panel reference is missing!");
        }
    }
}
