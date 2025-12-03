using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverController : MonoBehaviour
{
    public Image gameOverImage;      // full-screen GAME OVER PNG
    public GameObject gameOverPanel; // your stats UI panel
    public SurvivalTimer timer;      // your timer script

    void Start()
    {
        // Start with GameOverImage invisible
        if (gameOverImage != null)
            gameOverImage.color = new Color(1, 1, 1, 0);

        // Stats panel is off at the start
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void TriggerGameOver()
    {
        timer.StopTimer();
        StartCoroutine(GameOverSequence());
    }

    IEnumerator GameOverSequence()
    {
        // Fade in GAME OVER image
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            gameOverImage.color = new Color(1, 1, 1, t);
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);

        // Fade out GAME OVER image
        for (float t = 1; t > 0; t -= Time.deltaTime)
        {
            gameOverImage.color = new Color(1, 1, 1, t);
            yield return null;
        }

        // Show stats panel
        gameOverPanel.SetActive(true);
    }
}
