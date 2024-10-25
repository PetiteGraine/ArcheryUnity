using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PutScoreInUI : MonoBehaviour
{
    public GameObject ScoreElementPrefab;
    private string _filename = "ScoresData.json";
    private ScoreData[] _scoreDataList;
    private List<ScoreData> _topScores = new List<ScoreData>();
    private List<GameObject> _uiElements = new List<GameObject>();
    private int _maxCount = 10;

    void Start()
    {
        _scoreDataList = SaveScoreJSON.ReadFromJSONToListString(_filename);
        _topScores = _scoreDataList
           .AsEnumerable()
           .OrderByDescending(score => score.score)
           .Take(_maxCount)
           .ToList();

        UpdateUI(_topScores);
    }

    private void UpdateUI(List<ScoreData> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            ScoreData el = list[i];
            if (i >= _uiElements.Count)
            {
                var inst = Instantiate(ScoreElementPrefab, Vector3.zero, Quaternion.identity);
                inst.transform.SetParent(gameObject.transform, false);

                _uiElements.Add(inst);
            }

            var texts = _uiElements[i].GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = el.name;
            texts[1].text = el.difficulty;
            texts[2].text = el.score.ToString();
            texts[3].text = el.date;
            Debug.Log(el.name);
        }
    }
}
