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
    public static Manager Instance;
    public GameObject Player;
    [SerializeField] private GameObject endPlayerPosition;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI FinalScoreText;
    public TMP_InputField NameInputField;
    public GameObject BowTier1;
    public GameObject BowTier2;
    public GameObject BowTier3;
    public int Score = 0;
    public GameObject DirectHands;
    public GameObject RayHands;
    public GameObject EndCanvas;
    public ActionBasedContinuousMoveProvider ContinuousMove;
    private TimeSpan _timePlaying;
    private bool _timerGoing;
    public float RemainingTime = 40;
    private List<ScoreData> _entries = new List<ScoreData>();
    private string _filename = "ScoresData.json";

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);

        }
        else
        {
            Instance = this;
            ScoreText.text = "Score: 0";
        }

        TimeText.text = "Time: 00.00";
        _timerGoing = false;
        BeginTimer();

        Debug.Log(PlayerPrefs.GetString("mode"));
        switch (PlayerPrefs.GetString("mode"))
        {
            case "medium":
                BowTier2.SetActive(true);
                break;
            case "hard":
                BowTier2.SetActive(true);
                BowTier3.SetActive(true);
                break;
        }
    }

    private void BeginTimer()
    {
        if (PlayerPrefs.GetString("mode") != "training")
        {
            _timerGoing = true;
            StartCoroutine(UpdateTimer());
        }
    }

    public void EndTimer()
    {
        _timerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (_timerGoing)
        {
            RemainingTime -= Time.deltaTime;
            _timePlaying = TimeSpan.FromSeconds(RemainingTime);
            string timePlayingStr = "Time: " + _timePlaying.ToString(@"ss\.ff");
            TimeText.text = timePlayingStr;

            if (RemainingTime <= 0)
                EndGame();

            yield return null;
        }

    }

    private void EndGame()
    {
        _timerGoing = false;
        RemainingTime = 0;
        FinalScoreText.text = ScoreText.text;
        Player.transform.position = endPlayerPosition.transform.position;
        Player.transform.rotation = endPlayerPosition.transform.rotation;
        BowTier1.SetActive(false);
        BowTier2.SetActive(false);
        BowTier3.SetActive(false);
        DirectHands.SetActive(false);
        RayHands.SetActive(true);
        EndCanvas.SetActive(true);
        ContinuousMove.enabled = false;
    }

    public void BackToMainMenu()
    {
        ScoreData.Instance.SetScoreData(NameInputField.text, char.ToUpper(PlayerPrefs.GetString("mode")[0]) + PlayerPrefs.GetString("mode").Substring(1), Score, DateTime.Now.ToString("dd/MM/yyyy"));
        _entries = SaveScoreJSON.ReadListFromJSON<ScoreData>(_filename);
        _entries.Add(ScoreData.Instance);
        SaveScoreJSON.SaveToJSON<ScoreData>(_entries, _filename);
        SceneManager.LoadScene("Main menu");
    }

    public void UpdateScore(int points)
    {
        Score += points;
        ScoreText.text = "Score: " + Score.ToString();
    }
}
