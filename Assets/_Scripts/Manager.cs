using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Manager : MonoBehaviour
{
    public static Manager instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public int score = 0;
    private TimeSpan timePlaying;
    private bool timerGoing;
    public float remainingTime = 20;
    private List<ScoreData> entries = new List<ScoreData>();
    private string filename = "ScoresData.json";


    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);

        }
        else
        {
            instance = this;
            scoreText.text = "Score: 0";
        }

        timeText.text = "Time: 00.00";
        timerGoing = false;
        BeginTimer();
    }

    private void BeginTimer()
    {
        timerGoing = true;

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            remainingTime -= Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(remainingTime);
            string timePlayingStr = "Time: " + timePlaying.ToString(@"ss\.ff");
            timeText.text = timePlayingStr;

            if (remainingTime <= 0)
                EndGame();

            yield return null;
        }

    }

    private void EndGame()
    {
        timerGoing = false;
        remainingTime = 0;
        ScoreData.Instance.SetScoreData("PetiteGraine", char.ToUpper(PlayerPrefs.GetString("mode")[0]) + PlayerPrefs.GetString("mode").Substring(1), score, DateTime.Now.ToString("dd/MM/yyyy"));
        entries = SaveScoreJSON.ReadListFromJSON<ScoreData>(filename);
        entries.Add(ScoreData.Instance);
        SaveScoreJSON.SaveToJSON<ScoreData>(entries, filename);
        SceneManager.LoadScene("Main menu");

    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score.ToString();
    }
}
