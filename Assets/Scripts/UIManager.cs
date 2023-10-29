using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private static void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    private static void ExitScene()
    {
        SceneManager.LoadScene(0);
    }

    static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            case 0:
                GameObject.FindGameObjectWithTag("Level1Button").GetComponent<Button>().onClick.AddListener(LoadFirstLevel);
                break;
            case 1:
            {
                GameObject.FindGameObjectWithTag("ExitButton").GetComponent<Button>().onClick.AddListener(ExitScene);
                var scaredText = GameObject.FindGameObjectWithTag("ScaredText");
                if (scaredText)
                {
                    scaredText.SetActive(false);
                }

                break;
            }
        }
    }
}
