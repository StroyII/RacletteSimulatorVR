using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public TMP_Text text;
    public GameObject gameOverPanel;

    // Method to show game over message
    public void ShowGameOverMessage(int finalScore)
    {
        gameOverPanel.SetActive(true);
        text.text = "Bien joué " + PlayerData.pseudo + " ton score final est de " + finalScore + " !\nQue faire ? ";
    }

    // Method called on game over
    public void onGameOver()
    {
        ScoreManager.SaveCurrentPlayerScore();
        PlayerData.ResetScore();
    }
}
