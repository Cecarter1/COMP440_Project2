using TMPro;
using UnityEngine;

public class HUDTimer : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text timeText;   // TimeValue
    public TMP_Text bestText;   // BestValue

    private float currentTime = 0f;
    private float bestTime = 0f;
    private bool isRunning = true;

    void Start()
    {
        // Load saved best time
        bestTime = PlayerPrefs.GetFloat("BestTime", 0f);
        bestText.text = FormatTime(bestTime);
    }

    void Update()
    {
        if (!isRunning) return;

        currentTime += Time.deltaTime;
        timeText.text = FormatTime(currentTime);
    }

    public void StopTimer()
    {
        isRunning = false;

        // Update best time if this run is better
        if (currentTime > bestTime)
        {
            bestTime = currentTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
            PlayerPrefs.Save();
        }

        bestText.text = FormatTime(bestTime);
    }

    private string FormatTime(float t)
    {
        int minutes = (int)(t / 60f);
        float seconds = t % 60f;
        return string.Format("{0:0}:{1:00.00}", minutes, seconds);
    }
}

