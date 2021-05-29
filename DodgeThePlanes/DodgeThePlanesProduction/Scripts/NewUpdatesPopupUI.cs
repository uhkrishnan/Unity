using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

namespace UpgradeSystem
{
    struct GameData
    {
        public string Description;
        public string Version;
        public string Url;
    }

    public class NewUpdatesPopupUI : MonoBehaviour
    {
        [SerializeField] GameObject uiCanvas;
        [SerializeField] Button uiNotNowButton;
        [SerializeField] Button uiUpdateButton;
        [SerializeField] Text uiDescriptionText;
        [SerializeField] [TextArea(1, 5)] string jsonDataURL;
        static bool isAlreadyCheckedForUpdates = false;
        GameData latestGameData;

        void Start()
        {
            if (!isAlreadyCheckedForUpdates)
            {
                StopAllCoroutines();
                StartCoroutine(CheckForUpdates());
            }
        }

        IEnumerator CheckForUpdates()
        {
            UnityWebRequest request = UnityWebRequest.Get(jsonDataURL);
            request.chunkedTransfer = false;
            request.disposeDownloadHandlerOnDispose = true;
            request.timeout = 60;

            //yield return request.Send();
            yield return request.SendWebRequest();

            if (request.isDone)
            {
                isAlreadyCheckedForUpdates = true;

                if (!request.isNetworkError)
                {
                    latestGameData = JsonUtility.FromJson<GameData>(request.downloadHandler.text);
                    if (!string.IsNullOrEmpty(latestGameData.Version) && !Application.version.Equals(latestGameData.Version))
                    {
                        uiDescriptionText.text = latestGameData.Description;
                        ShowPopup();
                    }
                }
            }

            request.Dispose();
        }

        void ShowPopup()
        {
            uiNotNowButton.onClick.AddListener(() => {
                HidePopup();
            });

            uiUpdateButton.onClick.AddListener(() => {
                Application.OpenURL(latestGameData.Url);
                HidePopup();
            });

            uiCanvas.SetActive(true);
        }

        void HidePopup()
        {
            uiCanvas.SetActive(false);
            uiNotNowButton.onClick.RemoveAllListeners();
            uiUpdateButton.onClick.RemoveAllListeners();
        }

        void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}