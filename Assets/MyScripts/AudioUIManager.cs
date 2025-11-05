using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioUIManager : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        AddListenerInActiveScene();
    }

    // Method to add listener to all buttons in the active scene
    public void AddListenerInActiveScene()
    {
        var activeScene = SceneManager.GetActiveScene();
        Button[] buttons = Resources.FindObjectsOfTypeAll<Button>();
        foreach (Button btn in buttons)
        {
            if (btn.gameObject.scene == activeScene)
            {
                btn.onClick.AddListener(PlayClickSound);
            }
        }

    }

    // Method to play click sound
    private void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
