using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHighscoreLevelButton : MonoBehaviour
{
    public ScoreManager scoreManager;
    public LevelDataSO levelData;

    [Header("Menu Canvas")]
    public GameObject menuCanvas;  // Assign your Canvas in inspector

    private void Start()
    {
        if (scoreManager == null)
        {
            GameObject smObj = GameObject.Find("Score Manager");
            if (smObj != null)
                scoreManager = smObj.GetComponent<ScoreManager>();
        }
    }

    public void LevelButtonHandler()
    {
        if (levelData == null)
        {
            Debug.LogError("MenuHighscoreLevelButton: No LevelDataSO assigned on this button.", this);
            return;
        }

        // -------------------------------
        // LEVEL SELECT ACTIONS
        // -------------------------------
        switch (levelData.levelNum)
        {
            case 1:
                LoadLevel("triangle");
                return;

            case 2:
                LoadLevel("square");
                return;

            case 3:
                LoadLevel("pentagon");
                return;

            case 4:
                LoadLevel("hexagon");
                return;

            case 5:
                LoadLevel("heptagon");
                return;
        }

        // -------------------------------
        // DEFAULT: POPULATE HIGHSCORE MENU
        // -------------------------------
        if (scoreManager != null)
        {
            scoreManager.PopulateHighscoreMenu(levelData);
        }
        else
        {
            Debug.LogError("MenuHighscoreLevelButton: ScoreManager reference is missing.", this);
        }
    }

    private void LoadLevel(string sceneName)
    {
        // 1. Hide menu canvas
        if (menuCanvas != null)
            menuCanvas.SetActive(false);
        else
            Debug.LogWarning("MenuHighscoreLevelButton: menuCanvas is not assigned.", this);

        // 2. Enable CameraFollow on Main Camera
        if (Camera.main != null)
        {
            CameraFollow camFollow = Camera.main.GetComponent<CameraFollow>();
            if (camFollow != null)
                camFollow.enabled = true;
            else
                Debug.LogWarning("LoadLevel: Main Camera has no CameraFollow component.", Camera.main);
        }
        else
        {
            Debug.LogWarning("LoadLevel: No Main Camera found in the scene.");
        }

        // 3. Load the requested scene
        SceneManager.LoadScene(sceneName);
    }
}
