using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoreDisplay : MonoBehaviour
{
    public Text[] scoreTexts; // Assign in inspector

    void Start()
    {
        LoadScores();
    }

    void LoadScores()
    {
        // Replace these with your teammate's actual variables
        var highscores = ScoreManager.instance.highscores;
        var names = ScoreManager.instance.playerNames;

        for (int i = 0; i < scoreTexts.Length; i++)
        {
            if (i < highscores.Count)
            {
                scoreTexts[i].text = $"{names[i]} — {highscores[i]}";
            }
            else
            {
                scoreTexts[i].text = "---";
            }
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("NewMainMenu"); // Change this to your main menu scene name
    }
}
