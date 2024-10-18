using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreData
{
    public string name = string.Empty;
    public string difficulty = string.Empty;
    public int score = 0;
    public string date = string.Empty;
    private static ScoreData _instance = null;

    public ScoreData(string name, string difficulty, int score, string date)
    {
        this.name = name;
        this.difficulty = difficulty;
        this.score = score;
        this.date = date;
    }

    public void SetScoreData(string name, string difficulty, int score, string date)
    {
        this.name = name;
        this.difficulty = difficulty;
        this.score = score;
        this.date = date;
    }

    public static ScoreData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ScoreData(string.Empty, string.Empty, 0, string.Empty);
            }
            return _instance;
        }
    }
}