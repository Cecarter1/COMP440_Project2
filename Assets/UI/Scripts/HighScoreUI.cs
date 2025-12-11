using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HighScoreUI : MonoBehaviour
{
    public Transform listParent;              // This is HighScorePanel
    public GameObject rowPrefab;              // The Row prefab you created
    public int maxDisplay = 10;               // Shows top 10 scores

    void Start()
    {
        PopulateList();  // When the scene loads, fill in the scores
    }

    void PopulateList()
    {
        // Get the high scores from ScoreManager
        var highscores = ScoreManager.instance.highscores;

        // Sort highest score ? lowest score
        highscores.Sort((a, b) => b.score.CompareTo(a.score));

        // Create one row per score
        for (int i = 0; i < Mathf.Min(maxDisplay, highscores.Count); i++)
        {
            GameObject row = Instantiate(rowPrefab, listParent);
            TextMeshProUGUI[] texts = row.GetComponentsInChildren<TextMeshProUGUI>();

            texts[0].text = highscores[i].playerName;     // Left text
            texts[1].text = highscores[i].score.ToString(); // Right text
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("NewMainMenu");
    }
}
