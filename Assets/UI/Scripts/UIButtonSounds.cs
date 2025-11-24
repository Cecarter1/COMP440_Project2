using UnityEngine;

public class UIButtonSounds : MonoBehaviour
{
    public AudioSource audioSource;

    // This will be called by the button OnClick
    public void PlayClick()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
