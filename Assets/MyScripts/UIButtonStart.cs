using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UIButtonStart : MonoBehaviour
{
    public Transform tpPoint;
    public string sceneName;
    public Slider progressBar;

    public GameObject player;

    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerData.pseudo == "Player1")
        {
           button.interactable = false;
        } else
        {
           button.interactable = true;
        }
    }

    // Method to load the specified scene and teleport player in a loading zone
    public void loadScene()
    {
        player.transform.position = tpPoint.position;
        player.transform.rotation = tpPoint.rotation;

        StartCoroutine(LoadAsync());

    }

    // Coroutine to load scene asynchronously
    IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;

            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
