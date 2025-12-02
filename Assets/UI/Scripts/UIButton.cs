using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButton : MonoBehaviour
{
    public AudioSource uiAudioSource;   // drag UISFX here
    public AudioClip clickClip;         // drag click sound here

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlayClickSound);
    }

    private void PlayClickSound()
    {
        if (uiAudioSource != null && clickClip != null)
        {
            uiAudioSource.PlayOneShot(clickClip);
        }
    }
}
