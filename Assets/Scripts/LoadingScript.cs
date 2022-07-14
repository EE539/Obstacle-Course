using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingScript : MonoBehaviour
{
    public Slider slider;

    AsyncOperation async;

    private void Start()
    {

        StartCoroutine(LoadingSceen());
    }

    IEnumerator LoadingSceen()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //Get the index of the scene
        int nextSceneIndex = currentSceneIndex + 1;
        async = SceneManager.LoadSceneAsync(nextSceneIndex);
        async.allowSceneActivation = false;

        while (async.isDone == false)
        {
            slider.value = async.progress;
            if (async.progress == 0.9f)
            {
                slider.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}

