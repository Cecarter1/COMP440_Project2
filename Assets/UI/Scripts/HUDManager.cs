using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [Header("Live HUD Text")]
    public TextMeshProUGUI survivalTimeText;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI phaseText;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalTimeText;
    public TextMeshProUGUI phaseRankText;   // "Highest Phase Reached"

    [Header("Scene Names")]
    [Tooltip("Name of your Main Menu scene in Build Settings")]
    public string mainMenuSceneName = "MainMenu";

    float survivalTime = 0f;
    bool isAlive = true;

    // store phase name or index
    string currentPhaseName = "Triangle";

    void Start()
    {
        // make sure game runs normally
        Time.timeScale = 1f;

        // make sure game over is hidden at start
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // initial text
        if (phaseText != null)
            phaseText.text = "PHASE: " + currentPhaseName;
    }

    void Update()
    {
        if (!isAlive) return;

        // update timer
        survivalTime += Time.deltaTime;

        if (survivalTimeText != null)
            survivalTimeText.text = "TIME: " + survivalTime.ToString("0.00");
    }

    // call this from your phase system later
    public void SetPhase(string phaseName)
    {
        currentPhaseName = phaseName;

        if (phaseText != null)
            phaseText.text = "PHASE: " + currentPhaseName;
    }

    public void GameOver()
    {
        if (!isAlive) return;  // prevent double-calls

        Debug.Log("GAME OVER triggered by HUDManager");
        isAlive = false;

        // freeze gameplay
        Time.timeScale = 0f;

        // show panel
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // fill in results
        if (finalTimeText != null)
            finalTimeText.text = "Final Time: " + survivalTime.ToString("0.00") + "s";

        if (phaseRankText != null)
            phaseRankText.text = "Highest Phase: " + currentPhaseName;
    }

    // ==========================
    //  BUTTON FUNCTIONS
    // ==========================

    // Called by Retry button
    public void Retry()
    {
        Debug.Log("Retry pressed – reloading gameplay scene");

        // un-pause time BEFORE reload just in case
        Time.timeScale = 1f;

        // reload current scene
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }

    // Called by Main Menu button
    public void GoToMainMenu()
    {
        Debug.Log("Main Menu pressed – loading main menu scene: " + mainMenuSceneName);

        Time.timeScale = 1f;

        if (!string.IsNullOrEmpty(mainMenuSceneName))
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
        else
        {
            Debug.LogError("HUDManager: mainMenuSceneName is empty! Set it in the Inspector.");
        }
    }
}
