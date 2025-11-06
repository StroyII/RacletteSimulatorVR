using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;

#if UNITY_ANDROID && !UNITY_EDITOR
using UnityEngine.Networking;
#endif

public static class ScoreManager
{
    // Name of the score file
    private static string FileName = "scores.json";

    // Get the full path to the persistent score file
    private static string GetPersistentPath()
    {
        return Path.Combine(Application.persistentDataPath, FileName);
    }

    // Ensure the score file exists in persistent data path
    private static void EnsureFileExists()
    {
        string persistentPath = GetPersistentPath();

        string dir = Path.GetDirectoryName(persistentPath);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        if (File.Exists(persistentPath))
            return;

        string sourcePath = Path.Combine(Application.streamingAssetsPath, FileName);

        // Copy from StreamingAssets to persistentDataPath
#if UNITY_ANDROID && !UNITY_EDITOR
        // Android / Quest : StreamingAssets compressé, on doit utiliser UnityWebRequest
        UnityWebRequest www = UnityWebRequest.Get(sourcePath);
        var asyncOp = www.SendWebRequest();
        while (!asyncOp.isDone) { }

        if (www.result == UnityWebRequest.Result.Success)
        {
            File.WriteAllBytes(persistentPath, www.downloadHandler.data);
        }
        else
        {
            File.WriteAllText(persistentPath, "{\"scores\":[]}");
        }
#else
        // Windows / macOS / Linux : copie directe
        if (File.Exists(sourcePath))
            File.Copy(sourcePath, persistentPath);
        else
            File.WriteAllText(persistentPath, "{\"scores\":[]}");
#endif
    }

    // Save the current player's score to the score file
    public static void SaveCurrentPlayerScore()
    {
        EnsureFileExists();

        var scoreList = LoadScoreList();

        // Create a new score entry
        ScoreEntry entry = new ScoreEntry(
            PlayerData.pseudo,
            PlayerData.score,
            DateTime.UtcNow.ToString("o")
        );

        scoreList.scores.Add(entry);

        // Serialize and save back to file
        string json = JsonUtility.ToJson(scoreList);
        File.WriteAllText(GetPersistentPath(), json);
    }

    // Load the score list from the score file
    public static ScoreList LoadScoreList()
    {
        EnsureFileExists();

        string path = GetPersistentPath();
        string json = File.ReadAllText(path);
        var list = JsonUtility.FromJson<ScoreList>(json);
        return list ?? new ScoreList();
    }

    // Get the top N scores from the score list
    public static List<ScoreEntry> GetTopNScores(int n)
    {
        List<ScoreEntry> all = LoadScoreList().scores;
        all.Sort((a, b) => b.score.CompareTo(a.score));
        return all.GetRange(0, Math.Min(n, all.Count));
    }
}
