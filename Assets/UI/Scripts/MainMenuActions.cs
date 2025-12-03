using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuActions : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("PreGameScene");
    }

    public void OpenHighScores()
    {
        SceneManager.LoadScene("HighScoreScene");  // Only if HighScoreScene actually exists
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
