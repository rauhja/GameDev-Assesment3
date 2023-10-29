using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    void ExitScene()
    {
        SceneManager.LoadScene(0);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            GameObject.FindGameObjectWithTag("Level1Button").GetComponent<Button>().onClick.AddListener(LoadFirstLevel);
        }
        if (scene.buildIndex == 1)
        {
            GameObject.FindGameObjectWithTag("ExitButton").GetComponent<Button>().onClick.AddListener(ExitScene);
        }
    }
}
