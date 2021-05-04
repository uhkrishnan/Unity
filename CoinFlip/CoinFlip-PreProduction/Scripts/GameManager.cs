using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void Start()
    {
        Invoke("LoadSceneGame", 3);
    }

    public void LoadSceneGame()
    {
        SceneManager.LoadScene("GameScene");
    }

}
