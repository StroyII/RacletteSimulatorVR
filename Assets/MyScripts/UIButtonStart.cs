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

    public void loadScene()
    {
        player.transform.position = tpPoint.position;
        player.transform.rotation = tpPoint.rotation;

        StartCoroutine(LoadAsync());

    }

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
