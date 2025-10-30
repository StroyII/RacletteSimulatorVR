using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UILeaderboard : MonoBehaviour
{
    public GameObject entryPrefab;
    public Transform contentParent;

    private int num = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisplayLeaderboard();
    }

    private void DisplayLeaderboard()
    {
        List<ScoreEntry> scores = ScoreManager.GetTopNScores(50);

        scores.Sort((a, b) => b.score.CompareTo(a.score));

        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var score in scores)
        {
            GameObject entryGO = Instantiate(entryPrefab, contentParent);
            entryGO.transform.localScale = Vector3.one;
            TMPro.TextMeshProUGUI entryText = entryGO.GetComponent<TMPro.TextMeshProUGUI>();
            entryText.text = num.ToString() + ". " + score.pseudo + " - " + score.score.ToString();
            
            num++;
        }
    }

}
