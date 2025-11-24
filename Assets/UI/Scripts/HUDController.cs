using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI survivalTimeText;

    private float survivalTime = 0f;

    void Update()
    {
        survivalTime += Time.deltaTime;
        survivalTimeText.text = "Time: " + survivalTime.ToString("F2");
    }

    public float GetCurrentTime()
    {
        return survivalTime;
    }
}
