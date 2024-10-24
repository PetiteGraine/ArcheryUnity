using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PutScoreInUI : MonoBehaviour
{
    public GameObject scoreElementPrefab;
    private string filename = "ScoresData.json";
    private ScoreData[] scoreDataList;
    private List<ScoreData> topScores = new List<ScoreData>();
    List<GameObject> uiElements = new List<GameObject>();
    private int maxCount = 10;

    void Start()
    {
        scoreDataList = SaveScoreJSON.ReadFromJSONToListString(filename);
        topScores = scoreDataList
           .AsEnumerable()
           .OrderByDescending(score => score.score)
           .Take(maxCount)
           .ToList();

        UpdateUI(topScores);
    }

    private void UpdateUI(List<ScoreData> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            ScoreData el = list[i];
            if (i >= uiElements.Count)
            {
                var inst = Instantiate(scoreElementPrefab, Vector3.zero, Quaternion.identity);
                inst.transform.SetParent(gameObject.transform, false);

                uiElements.Add(inst);
            }

            var texts = uiElements[i].GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = el.name;
            texts[1].text = el.difficulty;
            texts[2].text = el.score.ToString();
            texts[3].text = el.date;

        }
    }
}
