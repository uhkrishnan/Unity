using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] TextMeshProUGUI rateUsTMP;
    [SerializeField] TextMeshProUGUI exitTMP;
    [SerializeField] TextMeshProUGUI playButtonText;
    [SerializeField] TextMeshProUGUI rankingButtonText;
    [SerializeField] TextMeshProUGUI titleTextDodge;
    [SerializeField] TextMeshProUGUI titleTextThe;
    [SerializeField] TextMeshProUGUI titleTextPlanes;
    [SerializeField] TextMeshProUGUI moongaBrosTMP;
    [SerializeField] Image background;
    

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


        currentTheme = PlayerPrefs.GetInt("theme", 0);

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
        currentTheme = PlayerPrefs.GetInt("theme", 0);

        if(currentTheme == 0)
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
        Camera.main.backgroundColor = lightCamera;
        background.color = lightThemeBackground;

        player.sprite = lightThemeImage;
        //player.gameObject.GetComponent<Shadow>().effectDistance = new Vector3(0, -15);
        //player.gameObject.GetComponent<Shadow>().effectColor = lightThemeShadow;
        playerShadow.effectDistance = new Vector3(0, -15);
        playerShadow.effectColor = lightThemeShadow;

        playButtonImage.color = darkThemeBackground;
        //playButtonImage.gameObject.GetComponent<Shadow>().effectColor = lightThemeShadow;
        playButtonImageShadow.effectColor = lightThemeShadow;

        rankingButtonImage.color = darkThemeBackground;
        //rankingButtonImage.gameObject.GetComponent<Shadow>().effectColor = lightThemeShadow;
        rankingButtonShadow.effectColor = lightThemeShadow;

        //soundOn.sprite = Resources.Load<Sprite>("General/0_SoundOn");
        //soundOff.sprite = Resources.Load<Sprite>("General/0_SoundOff");
        soundOn.sprite = lightSoundOn;
        soundOff.sprite = lightSoundOff;
        //soundOff.gameObject.GetComponent<Shadow>().effectColor = lightThemeShadow;
        soundOffShadow.effectColor = lightThemeShadow;

        //vibrateOn.sprite = Resources.Load<Sprite>("General/0_VibrationOn");
        //vibrateOff.sprite = Resources.Load<Sprite>("General/0_VibrationOff");
        vibrateOn.sprite = lightVibrationOn;
        vibrateOff.sprite = lightVibrationOff;
        //vibrateOff.gameObject.GetComponent<Shadow>().effectColor = lightThemeShadow;
        vibrateOffShadow.effectColor = lightThemeShadow;

        exitPopupPanel.color = darkThemeBackground;
        rewardsPanel.color = lightThemeBackground;
        rateUsButton.color = darkThemeBackground;
        //rateUsButton.gameObject.GetComponent<Shadow>().effectColor = lightThemeShadow;
        rateUsButtonShadow.effectColor = lightThemeShadow;
        rateUsTMP.color = lightThemeBackground;
        exitButton.color = darkThemeBackground;
        //exitButton.gameObject.GetComponent<Shadow>().effectColor = lightThemeShadow;
        exitButtonShadow.effectColor = lightThemeShadow;
        exitTMP.color = lightThemeBackground;


        ChangeInverseFontColour(darkThemeBackground);
        ChangeFontColour(lightThemeBackground);
        
        PlayerPrefs.SetInt("theme", 1);
    }

    private void DarkTheme()
    {
        Camera.main.backgroundColor = darkCamera;
        background.color = darkThemeBackground;

        player.sprite = darkThemeImage;
        //player.gameObject.GetComponent<Shadow>().effectDistance = new Vector3(0, -15);
        //player.gameObject.GetComponent<Shadow>().effectColor = darkThemeShadow;
        playerShadow.effectDistance = new Vector3(0, -15);
        playerShadow.effectColor = darkThemeShadow;

        playButtonImage.color = lightThemeBackground;
        //playButtonImage.gameObject.GetComponent<Shadow>().effectColor = darkThemeShadow;
        playButtonImageShadow.effectColor = darkThemeShadow;

        rankingButtonImage.color = lightThemeBackground;
        //rankingButtonImage.gameObject.GetComponent<Shadow>().effectColor = darkThemeShadow;
        rankingButtonShadow.effectColor = darkThemeShadow;


        //soundOn.sprite = Resources.Load<Sprite>("General/1_SoundOn");
        //soundOff.sprite = Resources.Load<Sprite>("General/1_SoundOff");
        soundOn.sprite = darkSoundOn;
        soundOff.sprite = darkSoundOff;
        //soundOff.gameObject.GetComponent<Shadow>().effectColor = darkThemeShadow;
        soundOffShadow.effectColor = darkThemeShadow;

        //vibrateOn.sprite = Resources.Load<Sprite>("General/1_VibrationOn");
        //vibrateOff.sprite = Resources.Load<Sprite>("General/1_VibrationOff");
        vibrateOn.sprite = darkVibrationOn;
        vibrateOff.sprite = darkVibrationOff;
        //vibrateOff.gameObject.GetComponent<Shadow>().effectColor = darkThemeShadow;
        vibrateOffShadow.effectColor = darkThemeShadow;

        exitPopupPanel.color = lightThemeBackground;
        rewardsPanel.color = darkThemeBackground;
        rateUsButton.color = lightThemeBackground;
        //rateUsButton.gameObject.GetComponent<Shadow>().effectColor = darkThemeShadow;
        rateUsButtonShadow.effectColor = darkThemeShadow;
        rateUsTMP.color = darkThemeBackground;
        exitButton.color = lightThemeBackground;
        //exitButton.gameObject.GetComponent<Shadow>().effectColor = darkThemeShadow;
        exitButtonShadow.effectColor = darkThemeShadow;
        exitTMP.color = darkThemeBackground;

        ChangeFontColour(darkThemeBackground);
        ChangeInverseFontColour(lightThemeBackground);
        

        PlayerPrefs.SetInt("theme", 0);
    }

    private void ChangeFontColour(Color fontColour)
    {   
        playButtonText.color = fontColour;
        rankingButtonText.color = fontColour;   
    }

    private void ChangeInverseFontColour(Color fontColour)
    {
        titleTextDodge.color = fontColour;
        titleTextThe.color = fontColour;
        titleTextPlanes.color = fontColour;
        moongaBrosTMP.color = fontColour;

    }

}
