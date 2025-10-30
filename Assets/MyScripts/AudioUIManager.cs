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

    private void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
