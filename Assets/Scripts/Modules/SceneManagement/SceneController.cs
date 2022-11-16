using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController instance;

    public static SceneController Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null && instance != this)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void RequestSceneLoad(SceneDataSO scene, Action callback, bool loadAdditively, bool setActive = false)
    {
        StartCoroutine(LoadNewScene(scene.sceneAsset.name, callback, loadAdditively, setActive));
    }

    private IEnumerator LoadNewScene(string sceneName, Action OnSceneLoaded, bool loadAdditive, bool setAsActive = false)
    {
        LoadSceneMode mode = default;

        if (loadAdditive)
        {
            mode = LoadSceneMode.Additive;
        }
        else
        {
            mode = LoadSceneMode.Single;
        }

        AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync(sceneName, mode);

        while (!sceneLoadOperation.isDone)
        {
            yield return null;
        }

        var loadedScene = SceneManager.GetSceneByName(sceneName);
        if (loadedScene != null)
        {
            Debug.Log(loadedScene.name);
            if (setAsActive)
            {
                SceneManager.SetActiveScene(loadedScene);
            }
        }

        OnSceneLoaded?.Invoke();

        sceneLoadOperation.allowSceneActivation = true;
    }
}