using UnityEngine;
using TMPro;

public class TitleGlow : MonoBehaviour
{
    public float speed = 2f;
    public Color colorA = new Color(1, 1, 1, 0.6f);
    public Color colorB = new Color(1, 1, 1, 1f);

    private TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        text.color = Color.Lerp(colorA, colorB, (Mathf.Sin(Time.time * speed) + 1) / 2);
    }
}
