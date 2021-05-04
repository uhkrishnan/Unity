using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public void Start()
    {
        Debug.Log("started");
        Invoke("LoadSceneGame", 3);
    }

    public void LoadSceneGame()
    {
        Debug.Log("scene 2");
        SceneManager.LoadScene("GameScene");
    }


}
