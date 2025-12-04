using UnityEngine;

public class MenuHighscoreLevelButton : MonoBehaviour
{
    public ScoreManager scoreManager;
    public LevelDataSO levelData;

    public void Start()
    {
        scoreManager = GameObject.Find("Score Manager").GetComponent<ScoreManager>();
    }

    public void LevelButtonHandler()
    {
        scoreManager.PopulateHighscoreMenu(levelData);
    }
}
