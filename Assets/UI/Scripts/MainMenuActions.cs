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
<<<<<<< Updated upstream
        SceneManager.LoadScene("HighScoreScene");
=======
        SceneManager.LoadScene("NewHighScoreScene");  // Only if HighScoreScene actually exists
>>>>>>> Stashed changes
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
