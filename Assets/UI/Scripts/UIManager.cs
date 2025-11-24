using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("HUD Text")]
    public TextMeshProUGUI survivalTimeText;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI phaseText;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI rankText;
    public GameObject tryAgainButton;
    public GameObject mainMenuButton;

    private float survivalTime;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // Start hidden until game begins
        gameOverPanel.SetActive(false);

        survivalTime = 0f;
        UpdateSurvivalTime(0f);
        UpdateCombo(0);
        UpdatePhase(1);
    }

    void Update()
    {
        // Only add survival time if gameplay is active
        if (!gameOverPanel.activeSelf)
        {
            survivalTime += Time.deltaTime;
            UpdateSurvivalTime(survivalTime);
        }
    }

    // -----------------------
    // HUD UPDATE FUNCTIONS
    // -----------------------

    public void UpdateSurvivalTime(float time)
    {
        survivalTimeText.text = "TIME: " + time.ToString("0.00");
    }

    public void UpdateCombo(int combo)
    {
        comboText.text = "COMBO: " + combo.ToString();
    }

    public void UpdatePhase(int phase)
    {
        phaseText.text = "PHASE: " + phase.ToString();
    }

    // -----------------------
    // GAME OVER
    // -----------------------

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);

        finalScoreText.text = "FINAL TIME: " + survivalTime.ToString("0.00");

        // Placeholder rank system (can improve later)
        if (survivalTime < 10) rankText.text = "RANK: C";
        else if (survivalTime < 20) rankText.text = "RANK: B";
        else if (survivalTime < 30) rankText.text = "RANK: A";
        else rankText.text = "RANK: S";
    }

    // -----------------------
    // BUTTONS
    // -----------------------

    public void OnTryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
