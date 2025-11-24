using UnityEngine;

public class BeatTester : MonoBehaviour
{
    public HUDController hud;          // drag HUDCanvas here
    public GameOverUI gameOverUI;      // drag GOPanel here

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            float t = hud.GetCurrentTime();
            Debug.Log("G pressed. Sending Time = " + t + " to GameOverUI");
            gameOverUI.Show(t);
        }
    }
}
