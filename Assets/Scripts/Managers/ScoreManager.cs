using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public int totalScore = 0;
    private Highscore[] highscores;
    public TMP_Text pointsTxt;
    public int maxPoints = 0;

    [Header("Stats")]
    public Transform gameoverPane;
    public TMP_Text songTitleTxt;
    public TMP_Text scoreTxt;
    public TMP_Text totalScoreTxt;
    public TMP_Text gradeTxt;
    public TMP_Text statsTxt;

    [Header("Leaderboard")]
    public Transform[] leaderboardEntries;

    [Header("Score Stats")]
    public string grade;
    public int numPerfect = 0;
    public int numOk = 0;
    public int numBad = 0;
    public int maxCombo = 0;
    public float accuracy;

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        totalScore += scoreToAdd;
        pointsTxt.text = "Score: " + score.ToString();
    }

    public void CalculateCombo(int numCombo)
    {
        totalScore += numCombo * 200;

        if(numCombo > maxCombo)
        {
            maxCombo = numCombo;
        }
    }

    public void GameOver(int maxPoints, int score)
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
        else if (score >= maxPoints * 0.55)
        {
            grade = "F";
        }

        // Calculate accuracy
        accuracy = (score / 300 * (numPerfect + numOk + numBad)) * 100;

        // Load highscores
        LoadHighscores();

        // Check for highscore
        for (int i = 0; i < 3; i++)
        {
            if(score > highscores[i].score)
            {
                CreateHighscore(i);
                return;
            }
        }

        PopulateGameoverPane();
    }

    public void PopulateGameoverPane()
    {
        //songTitleTxt.text = "";
        scoreTxt.text = "Score/n" + score;
        totalScoreTxt.text = "Total Score/n" + totalScore;
        gradeTxt.text = "Grade/n" + grade;
        statsTxt.text = numPerfect + "/n" + numOk + "/n" + numBad + "/n" + maxCombo + "/n" + accuracy + "%";

        // Populate leaderboard
        for (int i = 0; i < 3; i++)
        {
            leaderboardEntries[i].Find("Name").GetComponent<TMP_Text>().text = highscores[i].name;
            leaderboardEntries[i].Find("Score").GetComponent<TMP_Text>().text = highscores[i].score.ToString();
            leaderboardEntries[i].Find("Total Score").GetComponent<TMP_Text>().text = highscores[i].totalScore.ToString();
            leaderboardEntries[i].Find("Combo").GetComponent<TMP_Text>().text = highscores[i].maxCombo.ToString();
            leaderboardEntries[i].Find("Accuracy").GetComponent<TMP_Text>().text = highscores[i].accuracy.ToString();
        }
    }

    public void CreateHighscore(int index)
    {
        Highscore newHighscore = new Highscore{name = this.name, grade = this.grade, score = this.score, totalScore = this.totalScore, maxCombo = this.maxCombo, accuracy = this.accuracy};
        for (int i = 2; i > index; i--)
        {
            highscores[i] = highscores[i - 1];
        }
        highscores[index] = newHighscore;
        SaveHighscores();
    }

    public void SaveHighscores()
    {
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    public void LoadHighscores()
    {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscoreList = JsonUtility.FromJson<Highscores>(jsonString);
    }

    private class Highscores
    {
        public List<Highscore> highscoreEntryList;
    }

    // Represents a single highscore entry
    [System.Serializable]
    private class Highscore
    {
        public string name;
        public string grade;
        public int score;
        public int totalScore;
        public int maxCombo;
        public float accuracy;
    }
}
