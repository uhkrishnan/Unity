using UnityEngine.SceneManagement;

public static class SceneController
{
    static int mainScene = 1;

    public static void LoadMainScene()
    {
        SceneManager.LoadScene(mainScene);
    }

    public static void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if(currentScene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentScene + 1);
        }
    }
    
    public static void ReloadScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public static void LoadScene(int index)
    {
        if (index >=0 && index < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(index);
        }

    }
}
