<<<<<<< Updated upstream
using UnityEngine;
using UnityEngine.UI;
=======
using System.IO;
using UnityEngine;
using TMPro;
>>>>>>> Stashed changes
using UnityEngine.SceneManagement;

public class HighScoreDisplay : MonoBehaviour
{
<<<<<<< Updated upstream
    public Text[] scoreTexts; // Assign in inspector
=======
    // TextMeshPro UI elements
    public TMP_Text[] scoreTexts;

    private ScoreManager.Highscore[] loadedScores;
>>>>>>> Stashed changes

    void Start()
    {
        LoadScores();
<<<<<<< Updated upstream
=======
        DisplayScores();
>>>>>>> Stashed changes
    }

    void LoadScores()
    {
<<<<<<< Updated upstream
        // Replace these with your teammate's actual variables
        var highscores = ScoreManager.instance.highscores;
        var names = ScoreManager.instance.playerNames;

        for (int i = 0; i < scoreTexts.Length; i++)
        {
            if (i < highscores.Count)
            {
                scoreTexts[i].text = $"{names[i]} — {highscores[i]}";
=======
        string path = Application.dataPath + "/save.txt";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            // Read file using teammate's Highscores structure
            ScoreManager.Highscores scoreData =
                JsonUtility.FromJson<ScoreManager.Highscores>(json);

            loadedScores = scoreData.highscoreEntryList.ToArray();
        }
        else
        {
            // If no save file exists yet, create 10 blank entries
            loadedScores = new ScoreManager.Highscore[10];
            for (int i = 0; i < loadedScores.Length; i++)
            {
                loadedScores[i] = new ScoreManager.Highscore();
            }
        }
    }

    void DisplayScores()
    {
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            if (i < loadedScores.Length)
            {
                ScoreManager.Highscore entry = loadedScores[i];

                scoreTexts[i].text =
                    $"Player: {entry.name} — High Score: {entry.finalScore}";
>>>>>>> Stashed changes
            }
            else
            {
                scoreTexts[i].text = "---";
            }
        }
    }

<<<<<<< Updated upstream
    public void BackToMenu()
    {
        SceneManager.LoadScene("NewMainMenu"); // Change this to your main menu scene name
=======
    // BACK TO MENU BUTTON SUPPORT
    public void BackToMenu()
    {
        SceneManager.LoadScene("NewMainMenu");
        // Change scene name ONLY if your main menu scene is different
>>>>>>> Stashed changes
    }
}
