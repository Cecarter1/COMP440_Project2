using UnityEngine;
using System.Collections;

public class TimeSlowPowerUp : MonoBehaviour
{
    [Header("Power-Up Settings")]
    public float slowDuration = 5f;          // How long time stays slowed
    public float slowFactor = 0.3f;          // 0.3 = 30% speed
    public float cooldownTime = 10f;         // Cooldown after using

    private bool isSlowing = false;
    private bool isOnCooldown = false;

    void Update()
    {
        // Spacebar to activate time slow
        if (Input.GetKeyDown(KeyCode.Space) && !isSlowing && !isOnCooldown)
        {
            StartCoroutine(ActivateSlowMotion());
        }
    }

    private IEnumerator ActivateSlowMotion()
    {
        isSlowing = true;
        isOnCooldown = true;

        // Slow down global game time
        Time.timeScale = slowFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // keeps physics stable

        Debug.Log("TIME SLOW ACTIVATED");

        yield return new WaitForSecondsRealtime(slowDuration);

        // Return to normal time
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        Debug.Log("TIME SLOW ENDED");

        isSlowing = false;

        // Wait for cooldown to reset
        yield return new WaitForSecondsRealtime(cooldownTime);

        isOnCooldown = false;
    }
}
