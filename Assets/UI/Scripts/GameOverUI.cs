using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public CanvasGroup panel;
    public TextMeshProUGUI finalTimeText;

    // This is the function BeatTester will call
    public void Show(float time)
    {
        Debug.Log("GameOverUI.Show() CALLED. Time = " + time);

        // Display time
        finalTimeText.text = "Final Time: " + time.ToString("F2");

        // SAVE THE SCORE
        SaveSystem.SaveHighScore(time);

        // SHOW the panel
        panel.alpha = 1f;
        panel.blocksRaycasts = true;
        panel.interactable = true;
    }
}
