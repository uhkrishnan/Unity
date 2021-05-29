using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Credits : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    private bool pointerDown;
    private float pointerDownTimer;
    
    [SerializeField] 
    private float requiredHoldTime;
    [SerializeField] GameObject creditsPopUp;
    [SerializeField] GameObject morseCodeHint;

    public UnityEvent onLongClick;

    private void Start()
    {
        morseCodeHint.SetActive(false);
        int creditsDecider = GameDataManager.GetGameNumber() % 5;
        PlayerPrefs.SetString("credits", "");
        creditsPopUp.SetActive(false);
        if(creditsDecider == 0)
        {
            morseCodeHint.SetActive(true);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
    }

    private void Update()
    {
        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime;
            if(pointerDownTimer > requiredHoldTime)
            {
                if(onLongClick != null)
                {
                    onLongClick.Invoke();
                }
                string credits = PlayerPrefs.GetString("credits");
                PlayerPrefs.SetString("credits", credits + "1");
                Reset();
            }
        }

        if(PlayerPrefs.GetString("credits") == "000101010000")
        {
            PlayerPrefs.SetString("credits", "");
            creditsPopUp.SetActive(true);
        }
    }

    private void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0f;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        string credits = PlayerPrefs.GetString("credits");   
        PlayerPrefs.SetString("credits", credits + "0");
    }
}
