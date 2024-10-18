using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveScoreJSON : MonoBehaviour
{
    private ScoreData scoreData;
    
    void Start()
    {
        scoreData = ScoreData.Instance;
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(scoreData);
        Debug.Log(json);

        using (StreamWriter writer = new StreamWriter(Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json"))
        {
            writer.Write(json);
        }
    }

    public void LoadDate()
    {
        string json = string.Empty;
        using (StreamReader reader = new StreamReader(Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json"))
        {
            json = reader.ReadToEnd();
        }
        ScoreData data = JsonUtility.FromJson<ScoreData>(json);
        scoreData.SetScoreData(data.name, data.difficulty, data.score, data.date);
    }
}