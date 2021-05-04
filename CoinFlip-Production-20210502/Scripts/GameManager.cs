using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void Start()
    {
        this.Wait(3f, () =>
        {
            SceneManager.LoadScene("GameScene");
        });
    }
}
