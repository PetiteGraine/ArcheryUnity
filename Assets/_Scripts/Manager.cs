using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;


public class Manager : MonoBehaviour
{
    public static Manager instance;
    public GameObject player;
    [SerializeField] private GameObject endPlayerPosition;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI finalScoreText;
    public TMP_InputField nameInputField;
    public GameObject bowTier1;
    public GameObject bowTier2;
    public GameObject bowTier3;

    public int score = 0;
    public GameObject directHands;
    public GameObject rayHands;
    public GameObject endCanvas;
    public ActionBasedContinuousMoveProvider continuousMove;
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

        Debug.Log(PlayerPrefs.GetString("mode"));
        switch (PlayerPrefs.GetString("mode"))
        {
            case "medium":
                bowTier2.SetActive(true);
                break;
            case "hard":
                bowTier2.SetActive(true);
                bowTier3.SetActive(true);
                break;
        }
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
        finalScoreText.text = scoreText.text;
        player.transform.position = endPlayerPosition.transform.position;
        player.transform.rotation = endPlayerPosition.transform.rotation;
        bowTier1.SetActive(false);
        bowTier2.SetActive(false);
        bowTier3.SetActive(false);
        directHands.SetActive(false);
        rayHands.SetActive(true);
        endCanvas.SetActive(true);
        continuousMove.enabled = false;
    }

    public void BackToMainMenu()
    {
        ScoreData.Instance.SetScoreData(nameInputField.text, char.ToUpper(PlayerPrefs.GetString("mode")[0]) + PlayerPrefs.GetString("mode").Substring(1), score, DateTime.Now.ToString("dd/MM/yyyy"));
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
