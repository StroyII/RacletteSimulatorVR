using System;
using System.Collections.Generic;

// Model to represent a score entry and a list of scores
[Serializable]
public class ScoreEntry
{
    public string pseudo;
    public int score;
    public string timestamp;

    // Constructor
    public ScoreEntry(string pseudo, int score, string timestamp)
    {
        this.pseudo = pseudo;
        this.score = score;
        this.timestamp = timestamp;
    }
}

// Model to represent a list of score entries
[Serializable]
public class ScoreList
{
    public List<ScoreEntry> scores = new List<ScoreEntry>();
}
