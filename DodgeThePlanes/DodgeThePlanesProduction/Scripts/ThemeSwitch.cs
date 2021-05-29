using UnityEngine;
using UnityEngine.UI;

public class ThemeSwitch : MonoBehaviour
{
    [SerializeField] Image player;
    [SerializeField] Image playButtonImage;
    [SerializeField] Image rankingButtonImage;
    [SerializeField] Image soundOn;
    [SerializeField] Image soundOff;
    [SerializeField] Image vibrateOn;
    [SerializeField] Image vibrateOff;
    [SerializeField] Image exitPopupPanel;
    [SerializeField] Image rewardsPanel;
    [SerializeField] Image rateUsButton;
    [SerializeField] Image exitButton;
    [SerializeField] Image background;
    [SerializeField] Text rateUsTMP;
    [SerializeField] Text exitTMP;
    [SerializeField] Text playButtonText;
    [SerializeField] Text rankingButtonText;
    [SerializeField] Text titleTextDodge;
    [SerializeField] Text titleTextThe;
    [SerializeField] Text titleTextPlanes;
    [SerializeField] Text moongaBrosTMP;
    [SerializeField] Text gameNumberText;

    [SerializeField] Image morseCode01;
    [SerializeField] Image morseCode02;
    [SerializeField] Image morseCode03;
    [SerializeField] Image morseCode11;
    [SerializeField] Image morseCode12;
    [SerializeField] Image morseCode13;
    [SerializeField] Image morseCode04;
    [SerializeField] Image morseCode05;
    [SerializeField] Image morseCode06;

    private Color darkThemeBackground = new Color(82 / 255f, 82 / 255f, 82 / 255f);
    private Color lightThemeBackground = new Color(224 / 255f, 224 / 255f, 224 / 255f);

    private Color darkCamera = new Color(72 / 255f, 72 / 255f, 72 / 255f);
    private Color lightCamera = new Color(207 / 255f, 207 / 255f, 207 / 255f);

    private Color lightThemeShadow = new Color(0, 0, 0, 0.5f);
    private Color darkThemeShadow = new Color(1, 1, 1, 0.5f);

    private Shadow playerShadow;
    private Shadow playButtonImageShadow;
    private Shadow rankingButtonShadow;
    private Shadow soundOffShadow;
    private Shadow vibrateOffShadow;
    private Shadow rateUsButtonShadow;
    private Shadow exitButtonShadow;

    private Sprite darkThemeImage;
    private Sprite lightThemeImage;
    private Sprite lightSoundOn;
    private Sprite lightSoundOff;
    private Sprite darkSoundOn;
    private Sprite darkSoundOff;
    private Sprite lightVibrationOn;
    private Sprite lightVibrationOff;
    private Sprite darkVibrationOn;
    private Sprite darkVibrationOff; 
    
    private int currentTheme;

    public void Start()
    {
        playerShadow = player.gameObject.GetComponent<Shadow>();
        playButtonImageShadow = playButtonImage.gameObject.GetComponent<Shadow>();
        rankingButtonShadow = rankingButtonImage.gameObject.GetComponent<Shadow>();
        soundOffShadow = soundOff.gameObject.GetComponent<Shadow>();
        vibrateOffShadow = vibrateOff.gameObject.GetComponent<Shadow>();
        rateUsButtonShadow = rateUsButton.gameObject.GetComponent<Shadow>();
        exitButtonShadow = exitButton.gameObject.GetComponent<Shadow>();

        darkThemeImage = Resources.Load<Sprite>("General/0_Player");
        lightThemeImage = Resources.Load<Sprite>("General/1_Player");
        lightSoundOn = Resources.Load<Sprite>("General/0_SoundOn");
        lightSoundOff = Resources.Load<Sprite>("General/0_SoundOff");
        darkSoundOn = Resources.Load<Sprite>("General/1_SoundOn");
        darkSoundOff = Resources.Load<Sprite>("General/1_SoundOff");
        lightVibrationOn = Resources.Load<Sprite>("General/0_VibrationOn");
        lightVibrationOff = Resources.Load<Sprite>("General/0_VibrationOff");
        darkVibrationOn = Resources.Load<Sprite>("General/1_VibrationOn");
        darkVibrationOff = Resources.Load<Sprite>("General/1_VibrationOff");

        gameNumberText.text = GameDataManager.GetGameNumber().ToString("000");
        currentTheme = GameDataManager.SelectedTheme();

        if (currentTheme == 0)
        {
            DarkTheme();
        }
        else
        {
            LightTheme();
        }
    }

    public void Switch()
    {
        currentTheme = GameDataManager.SelectedTheme();

        if (currentTheme == 0)
        {
            LightTheme();
        }
        else
        {
            DarkTheme();
        }
    }

    private void LightTheme()
    {
        ChangeColour(lightThemeBackground);
        ChangeInverseColour(darkThemeBackground);

        Camera.main.backgroundColor = lightCamera;
        
        player.sprite = lightThemeImage;        
        playerShadow.effectDistance = new Vector3(0, -15);
        soundOn.sprite = lightSoundOn;
        soundOff.sprite = lightSoundOff;                
        vibrateOn.sprite = lightVibrationOn;
        vibrateOff.sprite = lightVibrationOff;        

        ChangeShadowColour(lightThemeShadow);

        GameDataManager.SetLightTheme();
    }

    private void DarkTheme()
    {
        Camera.main.backgroundColor = darkCamera;
        ChangeColour(darkThemeBackground);
        ChangeInverseColour(lightThemeBackground);

        player.sprite = darkThemeImage;
        playerShadow.effectDistance = new Vector3(0, -15);

        soundOn.sprite = darkSoundOn;
        soundOff.sprite = darkSoundOff;
        
        vibrateOn.sprite = darkVibrationOn;
        vibrateOff.sprite = darkVibrationOff;

        ChangeShadowColour(darkThemeShadow);

        GameDataManager.SetDarkTheme();
    }

    private void ChangeShadowColour(Color shadowColour)
    {
        playerShadow.effectColor = playButtonImageShadow.effectColor = rankingButtonShadow.effectColor = 
        soundOffShadow.effectColor = vibrateOffShadow.effectColor = rateUsButtonShadow.effectColor = 
        exitButtonShadow.effectColor = shadowColour;
    }

    private void ChangeColour(Color themeColour)
    {
        background.color = rewardsPanel.color = rateUsTMP.color = exitTMP.color = playButtonText.color 
            = rankingButtonText.color = themeColour;
    }

    private void ChangeInverseColour(Color themeInverseColour)
    {
        playButtonImage.color = rankingButtonImage.color = exitPopupPanel.color = rateUsButton.color = exitButton.color
            = titleTextDodge.color = titleTextThe.color = titleTextPlanes.color = moongaBrosTMP.color = gameNumberText.color
            = morseCode01.color = morseCode02.color = morseCode03.color = morseCode04.color = morseCode05.color = morseCode06.color
            = morseCode11.color = morseCode12.color = morseCode13.color            
            = themeInverseColour;
    }                                                      

}
