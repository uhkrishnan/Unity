using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public void Start()
    {
        this.Wait(3f, () =>
        {
            SceneController.LoadNextScene();
        });
    }
}
