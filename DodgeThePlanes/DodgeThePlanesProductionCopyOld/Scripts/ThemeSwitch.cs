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

        //gameNumberText.text = PlayerPrefs.GetInt("gameNumber", 1).ToString("000");
        gameNumberText.text = GameDataManager.GetGameNumber().ToString("000");


        //currentTheme = PlayerPrefs.GetInt("theme", 0);
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
        //currentTheme = PlayerPrefs.GetInt("theme", 0);
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
        //player.gameObject.GetComponent<Shadow>().effectDistance = new Vector3(0, -15);
        //player.gameObject.GetComponent<Shadow>().effectColor = lightThemeShadow;
        //playButtonImage.gameObject.GetComponent<Shadow>().effectColor = lightThemeShadow;
        //rankingButtonImage.gameObject.GetComponent<Shadow>().effectColor = lightThemeShadow;
        //soundOn.sprite = Resources.Load<Sprite>("General/0_SoundOn");
        //soundOff.sprite = Resources.Load<Sprite>("General/0_SoundOff");
        //soundOff.gameObject.GetComponent<Shadow>().effectColor = lightThemeShadow;
        //vibrateOn.sprite = Resources.Load<Sprite>("General/0_VibrationOn");
        //vibrateOff.sprite = Resources.Load<Sprite>("General/0_VibrationOff");
        //vibrateOff.gameObject.GetComponent<Shadow>().effectColor = lightThemeShadow;
        //rateUsButton.gameObject.GetComponent<Shadow>().effectColor = lightThemeShadow;
        //exitButton.gameObject.GetComponent<Shadow>().effectColor = lightThemeShadow;


        /* Changing to single function : ChangeColour();
        * background.color = lightThemeBackground;
        * rewardsPanel.color = lightThemeBackground;
        * rateUsTMP.color = lightThemeBackground;
        * exitTMP.color = lightThemeBackground;
        */
        ChangeColour(lightThemeBackground);

        /* Changing to single function : ChangeInverseColour();
         * playButtonImage.color = darkThemeBackground;        
         * rankingButtonImage.color = darkThemeBackground;        
         * exitPopupPanel.color = darkThemeBackground;
         * rateUsButton.color = darkThemeBackground;     
         * exitButton.color = darkThemeBackground;        
         */
        
        ChangeInverseColour(darkThemeBackground);

        Camera.main.backgroundColor = lightCamera;
        
        player.sprite = lightThemeImage;        
        playerShadow.effectDistance = new Vector3(0, -15);
        soundOn.sprite = lightSoundOn;
        soundOff.sprite = lightSoundOff;                
        vibrateOn.sprite = lightVibrationOn;
        vibrateOff.sprite = lightVibrationOff;        
        

        /* moved to ChangeShadowColour()
         * playerShadow.effectColor = lightThemeShadow;
         * playButtonImageShadow.effectColor = lightThemeShadow;
         * rankingButtonShadow.effectColor = lightThemeShadow;
         * soundOffShadow.effectColor = lightThemeShadow;        
         * vibrateOffShadow.effectColor = lightThemeShadow;
         * rateUsButtonShadow.effectColor = lightThemeShadow;
         * exitButtonShadow.effectColor = lightThemeShadow;
         */
        ChangeShadowColour(lightThemeShadow);
        //PlayerPrefs.SetInt("theme", 1);
        GameDataManager.SetLightTheme();
    }

    private void DarkTheme()
    {
        //player.gameObject.GetComponent<Shadow>().effectDistance = new Vector3(0, -15);
        //player.gameObject.GetComponent<Shadow>().effectColor = darkThemeShadow;
        //playButtonImage.gameObject.GetComponent<Shadow>().effectColor = darkThemeShadow;
        //rankingButtonImage.gameObject.GetComponent<Shadow>().effectColor = darkThemeShadow;
        //soundOn.sprite = Resources.Load<Sprite>("General/1_SoundOn");
        //soundOff.sprite = Resources.Load<Sprite>("General/1_SoundOff");
        //soundOff.gameObject.GetComponent<Shadow>().effectColor = darkThemeShadow;
        //vibrateOn.sprite = Resources.Load<Sprite>("General/1_VibrationOn");
        //vibrateOff.sprite = Resources.Load<Sprite>("General/1_VibrationOff");
        //vibrateOff.gameObject.GetComponent<Shadow>().effectColor = darkThemeShadow;
        //rateUsButton.gameObject.GetComponent<Shadow>().effectColor = darkThemeShadow;
        //exitButton.gameObject.GetComponent<Shadow>().effectColor = darkThemeShadow;

        Camera.main.backgroundColor = darkCamera;
        /* Changing to single function : ChangeColour();
         * background.color = darkThemeBackground;
         * rewardsPanel.color = darkThemeBackground;
         * rateUsTMP.color = darkThemeBackground;
         * exitTMP.color = darkThemeBackground;
         */

        ChangeColour(darkThemeBackground);

        /* Changing to single function : ChangeInverseColour();
         * playButtonImage.color = lightThemeBackground;
         * rankingButtonImage.color = lightThemeBackground;        
         * exitPopupPanel.color = lightThemeBackground;
         * rateUsButton.color = lightThemeBackground;   
         * exitButton.color = lightThemeBackground;         
         */

        ChangeInverseColour(lightThemeBackground);

        player.sprite = darkThemeImage;
        playerShadow.effectDistance = new Vector3(0, -15);
        
        
        soundOn.sprite = darkSoundOn;
        soundOff.sprite = darkSoundOff;
        
        vibrateOn.sprite = darkVibrationOn;
        vibrateOff.sprite = darkVibrationOff;


        /* moved to ChangeShadowColour()
         * playerShadow.effectColor = darkThemeShadow;
         * playButtonImageShadow.effectColor = darkThemeShadow;
         * rankingButtonShadow.effectColor = darkThemeShadow;
         * soundOffShadow.effectColor = darkThemeShadow;        
         * vibrateOffShadow.effectColor = darkThemeShadow;
         * rateUsButtonShadow.effectColor = darkThemeShadow;
         * exitButtonShadow.effectColor = darkThemeShadow;
         */

        ChangeShadowColour(darkThemeShadow);

        //PlayerPrefs.SetInt("theme", 0);
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
            = themeInverseColour;
    }                                                      

}
