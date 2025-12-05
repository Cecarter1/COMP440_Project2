// Written by Caitlyn Carter
// Finalized 11/24
// Score Manager: Tracks the players score and stats, lists them on the gameover menu, and saves adn loads the leaderboard.

using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameManager gameManager;

    [Header("Stats Pane")]
    public Transform gameoverPane;
    public TMP_Text songTitleTxt;
    public TMP_Text scoreTxt;
    public TMP_Text finalScoreTxt;
    public TMP_Text gradeTxt;
    public TMP_Text statsTxt;

    [Header("Leaderboard")]
    public Transform[] leaderboardEntries;
    private List<Highscore> highscores; 
    public TMP_Text pointsTxt;

    [Header("Score Stats")]
    [SerializeField] private int score = 0; // Score without combo points
    [SerializeField] private int finalScore = 0; // Score with combo points
    [SerializeField] private int maxPoints; // Max amount of points for a song
    [SerializeField] private string grade; // Letter grade for the song
    public int numPerfect = 0; // Number of perfect passes
    public int numOk = 0; // Number of ok passes
    public int numBad = 0; // Number of bad passes
    [SerializeField] private int maxCombo = 0; // Highest combo achieved
    [SerializeField] private float accuracy; // Accuracy of passes (score of each type of pass/score if each pass were perfect)

    [Header("Highscore/Leaderboard Info")]
    public bool highscoreSet = false; // Highscore has been made this round
    public int highscoreIndex; // Location of highscore on leaderboard
    public bool nameSet = true; // Player name has been set on leaderboard

    private int comboValue = 200;

    [Header("Menu Highscore Pane")]
    public List<LevelDataSO> levels;
    public Transform menuHighscorePane;
    public Button[] buttons;
    public TMP_Text lvlSongTxt;
    public TMP_Text difficultyTxt;
    public TMP_Text durationTxt;
    public TMP_Text bpmTxt;

    [Header("Menu Leaderboard")]
    public Transform[] menuLeaderboardEntries;
    private List<Highscore> menuHighscores;

    private void Start()
    {
        highscores = new List<Highscore>();
        LoadHighscores();
        maxPoints = 1000000;
        nameSet = true;
    }

    #region Scoring
    // Adds scoreToAdd points to the score and final score
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        finalScore += scoreToAdd;
        pointsTxt.text = "Score: " + score.ToString();
    }

    // Adds combo points to final score
    public void CalculateCombo(int numCombo)
    {
        finalScore += numCombo * comboValue;

        if(numCombo > maxCombo)
        {
            maxCombo = numCombo;
        }
    }
    #endregion

    #region Round Scoring UI
    // When the player hits a hexagon, stats are calculated, the stats pane is populated, and the leaderboard is updated.
    public void GameOver()
    {
        // Calculate letter grade
        if (score >= maxPoints + ((maxPoints / 500) * 200))
        {
            grade = "S";
        }
        else if (score >= maxPoints)
        {
            grade = "A";
        }
        else if (score >= maxPoints * 0.85)
        {
            grade = "B";
        }
        else if (score >= maxPoints * 0.75)
        {
            grade = "C";
        }
        else if (score >= maxPoints * 0.65)
        {
            grade = "D";
        }
        else 
        {
            grade = "F";
        }

        // Calculate accuracy
        accuracy = score / (500.0f * (numPerfect + numOk + numBad)) * 100;

        // Load highscores
        LoadHighscores();

        // Check for new highscore
        for (int i = 0; i < highscores.Count; i++)
        {
            if (finalScore > highscores[i].finalScore)
            {
                highscoreIndex = i;
                CreateHighscore();
                i = highscores.Count;
            }
            else if (finalScore == highscores[i].finalScore)
            {
                if (accuracy > highscores[i].accuracy)
                {
                    highscoreIndex = i;
                    CreateHighscore();
                    i = highscores.Count;
                }
                else if (accuracy <= highscores[i].accuracy)
                {
                    highscoreIndex = i + 1;
                    CreateHighscore();
                    i = highscores.Count;
                }
            }
        }

        PopulateGameoverPane();
    }

    public void PopulateGameoverPane()
    {
        //Populates stats
        songTitleTxt.text = "Level " + gameManager.levelNum + " - " + gameManager.songTitle;
        scoreTxt.text = "Score\n" + score;
        finalScoreTxt.text = "Final Score\n" + finalScore;
        gradeTxt.text = "Grade\n" + grade;
        statsTxt.text = numPerfect + "\n" + numOk + "\n" + numBad + "\n" + maxCombo + "\n" + string.Format("{0:#.0}", accuracy) + "%";

        //Adds empty highscores if there are < 3 leaderboard enteries
        if (highscores.Count < 3)
        {
            for (int i = highscores.Count; i < 3; i++)
            {
                highscores.Add(new Highscore());
            }
        }

        // Populate leaderboard
        for (int i = 0; i < highscores.Count; i++)
        {
            if (highscoreSet && i == highscoreIndex)
            {
                leaderboardEntries[i].Find("Name").GetComponent<TMP_InputField>().enabled = true;
                leaderboardEntries[i].Find("Name").GetComponent<Image>().enabled = true;
            }
            else
            {
                leaderboardEntries[i].Find("Name").Find("Text Area").Find("Text").GetComponent<TMP_Text>().text = highscores[i].name;
            }
            leaderboardEntries[i].Find("Grade").GetComponent<TMP_Text>().text = highscores[i].grade;
            leaderboardEntries[i].Find("Final Score").GetComponent<TMP_Text>().text = highscores[i].finalScore.ToString();
            leaderboardEntries[i].Find("Combo").GetComponent<TMP_Text>().text = highscores[i].maxCombo.ToString();
            leaderboardEntries[i].Find("Accuracy").GetComponent<TMP_Text>().text = string.Format("{0:#.0}", highscores[i].accuracy.ToString()) + "%";
        }

        gameoverPane.gameObject.SetActive(true);
    }

    // Adds a highscore to highscores
    public void CreateHighscore()
    {
        Highscore newHighscore = new Highscore{name = "", grade = this.grade, score = score, finalScore = this.finalScore, maxCombo = this.maxCombo, accuracy = this.accuracy};
        
        // Moves lower highscores down the list
        for (int i = 2; i > highscoreIndex; i--)
        {
            highscores[i] = highscores[i - 1];
        }

        highscores[highscoreIndex] = newHighscore;

        nameSet = false;
        highscoreSet = true;
        Debug.Log("Highscore!");
    }

    // Button handler for "Main Menu" button
    public void MainMenu()
    {
        if (nameSet)
        {
            gameoverPane.Find("Name Message").gameObject.SetActive(false);
            gameoverPane.gameObject.SetActive(false);
        }
        else
        {
            gameoverPane.Find("Name Message").gameObject.SetActive(true);
        }
    }

    // Button handler for "Play Again" button
    public void PlayAgain()
    {
        if (nameSet)
        {
            gameoverPane.Find("Name Message").gameObject.SetActive(false);
            gameoverPane.gameObject.SetActive(false);
            gameManager.gameActive = true;
            gameManager.gameOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            gameoverPane.Find("Name Message").gameObject.SetActive(true);
        }
    }

    // When the input field for a name is deselected, the input field is deactivated.
    public void InputDeselect()
    {
        leaderboardEntries[highscoreIndex].Find("Name").GetComponent<TMP_InputField>().enabled = false;
        leaderboardEntries[highscoreIndex].Find("Name").GetComponent<Image>().enabled = false;
        nameSet = true;
        highscores[highscoreIndex].name = leaderboardEntries[highscoreIndex].Find("Name").Find("Text Area").Find("Text").GetComponent<TMP_Text>().text;
        SaveHighscores();
    }
    #endregion

    #region Saving & Loading Highscores
    // Saves the highscores to a save file using JSon
    public void SaveHighscores()
    {
        Highscores testHighscores = new Highscores {highscoreEntryList = highscores};

        string json = JsonUtility.ToJson(testHighscores);
        File.WriteAllText(Application.dataPath + "/save.txt", json);
    }

    // Loads the highscore from the save file using JSon
    public void LoadHighscores()
    {
        if(File.Exists(Application.dataPath + "/save.txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");

            Highscores highscoresList = JsonUtility.FromJson<Highscores>(saveString);
            highscores = highscoresList.highscoreEntryList;
        }
    }
    #endregion

    #region Highscore Menu
    // Load all level data
    public void LoadAllLevelData()
    {
        if (File.Exists(Application.dataPath + "/LevelData.txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/LevelData.txt");

            Levels allLevelData = JsonUtility.FromJson<Levels>(saveString);
            levels = allLevelData.levelsData;
        }
    }

    // Save all level data
    public void SaveAllLevelData()
    {
        Levels testLevels = new Levels { levelsData = levels };

        string json = JsonUtility.ToJson(testLevels);
        File.WriteAllText(Application.dataPath + "/LevelData.txt", json);
    }

    // Populate Menu Highscore Page
    public void PopulateHighscoreMenu(LevelDataSO levelData)
    {
        lvlSongTxt.text = "Level " + levelData.levelNum + " - " + levelData.songTitle;
        difficultyTxt.text = "Difficulty:\n" + levelData.difficulty;
        durationTxt.text = "Duration:\n" + levelData.duration;
        bpmTxt.text = "BPM:\n" + levelData.bpm;

        // Populate leaderboard
        for (int i = 0; i < menuLeaderboardEntries.Length; i++)
        {
            menuLeaderboardEntries[i].Find("Name").GetComponent<TMP_Text>().text = levelData.leaderboard.highscoreEntryList[i].name;
            menuLeaderboardEntries[i].Find("Grade").GetComponent<TMP_Text>().text = levelData.leaderboard.highscoreEntryList[i].grade;
            menuLeaderboardEntries[i].Find("Final Score").GetComponent<TMP_Text>().text = levelData.leaderboard.highscoreEntryList[i].finalScore.ToString();
            menuLeaderboardEntries[i].Find("Combo").GetComponent<TMP_Text>().text = levelData.leaderboard.highscoreEntryList[i].maxCombo.ToString();
            menuLeaderboardEntries[i].Find("Accuracy").GetComponent<TMP_Text>().text = string.Format("{0:#.0}", levelData.leaderboard.highscoreEntryList[i].accuracy.ToString()) + "%";
        }
    }
    #endregion

    #region Helper Classes
    // Represents the data of each level
    public class Levels
    {
        public List<LevelDataSO> levelsData;
    }

    // Represents a single leaderboard
    public class Highscores
    {
        public List<Highscore> highscoreEntryList;

        public override string ToString()
        {
            string highschoreTable = "";

            foreach (Highscore highscore in highscoreEntryList)
            {
                highschoreTable += highscore.ToString() + ";";
            }

            return highschoreTable;
        }
    }


    // Represents a single highscore entry on the leaderboard
    [System.Serializable]
    public class Highscore
    {
        public string name;
        public string grade;
        public int score;
        public int finalScore;
        public int maxCombo;
        public float accuracy;

        public Highscore()
        {
            name = "AAA";
            grade = "N";
            score = 0;
            finalScore = 0;
            maxCombo = 0;
            accuracy = 0;
        }
    }
    #endregion
}
