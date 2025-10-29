using System;
using System.Collections.Generic;

[Serializable]
public class ScoreEntry
{
    public string pseudo;
    public int score;
    public string timestamp;

    public ScoreEntry(string pseudo, int score, string timestamp)
    {
        this.pseudo = pseudo;
        this.score = score;
        this.timestamp = timestamp;
    }
}

[Serializable]
public class ScoreList
{
    public List<ScoreEntry> scores = new List<ScoreEntry>();
}
