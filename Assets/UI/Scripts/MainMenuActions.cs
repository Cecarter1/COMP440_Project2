using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuActions : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("InstructionsScene");
    }

    public void OpenHighScores()
    {
        SceneManager.LoadScene("HighScoreScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
