using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;

#if UNITY_ANDROID && !UNITY_EDITOR
using UnityEngine.Networking;
#endif

public static class ScoreManager
{
    private static string FileName = "scores.json";

    private static string GetPersistentPath()
    {
        return Path.Combine(Application.persistentDataPath, FileName);
    }

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
            Debug.LogError("Impossible de copier scores.json depuis StreamingAssets : " + www.error);
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

    public static void SaveCurrentPlayerScore()
    {
        EnsureFileExists();

        var scoreList = LoadScoreList();

        ScoreEntry entry = new ScoreEntry(
            PlayerData.pseudo,
            PlayerData.score,
            DateTime.UtcNow.ToString("o")
        );

        scoreList.scores.Add(entry);

        string json = JsonUtility.ToJson(scoreList);
        File.WriteAllText(GetPersistentPath(), json);

        Debug.Log($"Score sauvegardé : {PlayerData.pseudo} - {PlayerData.score}");
    }

    public static ScoreList LoadScoreList()
    {
        EnsureFileExists();

        string path = GetPersistentPath();
        string json = File.ReadAllText(path);
        var list = JsonUtility.FromJson<ScoreList>(json);
        return list ?? new ScoreList();
    }

    public static List<ScoreEntry> GetTopNScores(int n)
    {
        List<ScoreEntry> all = LoadScoreList().scores;
        all.Sort((a, b) => b.score.CompareTo(a.score));
        return all.GetRange(0, Math.Min(n, all.Count));
    }
}
