using UnityEngine;

public static class SaveSystem
{
    public static void SaveHighScore(float time)
    {
        float currentHigh = PlayerPrefs.GetFloat("highscore", 0f);

        if (time > currentHigh)
        {
            PlayerPrefs.SetFloat("highscore", time);
            PlayerPrefs.Save();
        }
    }

    public static float LoadHighScore()
    {
        return PlayerPrefs.GetFloat("highscore", 0f);
    }

    // -----------------------------
    // PLAYER ICON SAVE / LOAD
    // -----------------------------
    public static void SaveSelectedIcon(int id)
    {
        PlayerPrefs.SetInt("selected_icon", id);
        PlayerPrefs.Save();
    }

    public static int LoadSelectedIcon()
    {
        return PlayerPrefs.GetInt("selected_icon", 0);
    }
}
