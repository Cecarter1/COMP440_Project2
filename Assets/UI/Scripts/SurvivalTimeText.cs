using UnityEngine;
using TMPro;

public class SurvivalTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timeElapsed;
    private bool running;

    void Start()
    {
        running = true;
    }

    void Update()
    {
        if (!running) return;

        timeElapsed += Time.deltaTime;
        timerText.text = timeElapsed.ToString("0.00");
    }

    public void StopTimer()
    {
        running = false;
    }

    public float GetFinalTime()
    {
        return timeElapsed;
    }
}
