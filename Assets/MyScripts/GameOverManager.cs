using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public TMP_Text text;
    public GameObject gameOverPanel;

    public void ShowGameOverMessage(int finalScore)
    {
        gameOverPanel.SetActive(true);
        text.text = "Bien joué " + PlayerData.pseudo + " ton score final est de " + finalScore + " !\nQue faire ? ";
    }
}
