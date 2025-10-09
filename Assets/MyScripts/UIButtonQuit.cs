using UnityEngine;

public class UIButtonQuit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void QuitGame()
    {
        Debug.Log("Quit Button Clicked");
        Application.Quit();
    }
}
