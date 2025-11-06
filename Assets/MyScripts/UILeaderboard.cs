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

    // Method to display the leaderboard entries
    private void DisplayLeaderboard()
    {
        // Get top 100 scores
        List<ScoreEntry> scores = ScoreManager.GetTopNScores(100);

        // Sort scores in descending order
        scores.Sort((a, b) => b.score.CompareTo(a.score));

        // Clear existing entries
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // Instantiate entry prefab for each score
        foreach (var score in scores)
        {
            // create entry game object
            GameObject entryGO = Instantiate(entryPrefab, contentParent);
            entryGO.transform.localScale = Vector3.one;
            TMPro.TextMeshProUGUI entryText = entryGO.GetComponent<TMPro.TextMeshProUGUI>();
            
            // Set entry text
            entryText.text = num.ToString() + ". " + score.pseudo + " - " + score.score.ToString();
            num++;
        }
    }

}
