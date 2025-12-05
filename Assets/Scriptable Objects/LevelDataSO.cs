using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataSO", menuName = "Scriptable Objects/Level Data")]
public class LevelDataSO : ScriptableObject
{
    public ScoreManager scoreManager;

    public string songTitle;
    public int levelNum;
    public AudioClip song;
    public int duration;
    public int bpm;
    public string difficulty;
    public ScoreManager.Highscores leaderboard;
}
