using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneControlButton : MonoBehaviour
{
    enum TargetScene
    {
        Next,
        MainMenu,
        Restart
    }

    [SerializeField] TargetScene targetScene;

    Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        button.onClick.RemoveAllListeners();
        switch (targetScene)
        {
            case TargetScene.MainMenu:
                button.onClick.AddListener(() => SceneController.LoadMainScene());
                break;

            case TargetScene.Next:
                button.onClick.AddListener(() => SceneController.LoadNextScene());
                break;

            case TargetScene.Restart:
                button.onClick.AddListener(() => SceneController.ReloadScene());
                break;
        }
    }
}
