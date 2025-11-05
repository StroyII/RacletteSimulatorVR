using UnityEngine;

// Static class to hold player data
public static class PlayerData
{
    // Player pseudo
    public static string pseudo = "Player";

    // Player score
    public static int score = 0;

    // Method to reset player score
    public static void ResetScore()
    {
        score = 0;
    }
}
