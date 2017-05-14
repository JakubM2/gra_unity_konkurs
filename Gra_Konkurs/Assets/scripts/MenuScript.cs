using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    //gameMaster -> saveScore, highScore etc
    private GameMaster gm;

    private Player player;

    //Options Menu
    public Slider[] volumeSliders;
    public Toggle[] resolutionToggels;
    public int[] screenWidths;
    int activeScreenIndex; //domyślnie będzie otwierany w 1280x720

    //add intro&logo to game
    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 0.33f;

    public RectTransform menuContainer;
    public Transform NewGame_Panel;
    public Transform Options_Panel;

    //HighScore & "Score Board"
    public Text maxCash;
    public Text maxHP;
    public Text maxPoint;
    public Text death;
    public Text enemyKill;

    private Vector3 desieredMenuPosition;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();   //Add GameMaster

        gm.Load(); //dzięki temu będą "świerze" dane

        //fadeGroup = FindObjectOfType<CanvasGroup>();

        //fadeGroup.alpha = 1;

        initOptions();

        //add buttons on-click event to levels
        initNewGame();

        activeScreenIndex = PlayerPrefs.GetInt("screen res index");
        bool isFullScreen = (PlayerPrefs.GetInt("fullscreen") == 1)?true:false;

        for(int i = 0; i <resolutionToggels.Length; i++)
        {
            resolutionToggels[i].isOn = i == activeScreenIndex;
        }
        SetFullScreen(isFullScreen);
    }

    private void Update()
    {
        //Zrobić też pobieranie zmiennych z plików binarnych!!! po to jest funcka Load() w Start()
        //Score Board -> Update text & Update Values 
            maxCash.text = ("Maks Pieniędzy: " + gm.highScore); //dokończyć dodawanie danych ze skryptu SaveScript
            //maxHP.text = ("Maks Zachowango życia: " + player.playerStats.Health); //dokończyć
            maxPoint.text = ("maks Punktów: " + gm.highPoint); //dokończyć
            death.text = ("Ilość Śmierci: " + gm.playerDeath); //dokończyć
            enemyKill.text = ("Zabitych Przeciwników: " + gm.enemyKill); //dokończyć
        
        //fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;

        //menu navigation
        menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, desieredMenuPosition, 0.1f);
    }

    private void initNewGame()
    {
        if (NewGame_Panel == null)
        {
            Debug.Log("I dont see new Game panel!");
        }
        int i = 0;
        foreach (Transform t in NewGame_Panel)
        {
            int currentIndex = i;
            Button b = t.GetComponent<Button>();//add few another... for example textbox
            //Text tx = t.GetComponent<Text>();
            //b.onClick.AddListener(() => newGamePanel(currentIndex));

            i++;
        }
    }

    private void initOptions()
    {
        if(Options_Panel == null)
        {
            Debug.Log("I dont see options panel!");
        }
        int i = 0;
        foreach(Transform t in Options_Panel)
        {
            int currentIndex = i;
            Button b = t.GetComponent<Button>();//add few another... for example textbox
            //Text tx = t.GetComponent<Text>();
            //b.onClick.AddListener(() => optionPanel(currentIndex));

            i++;
        }
        //Reset index
        i = 0;
        //optional we can add another panel --- 12:44 YT
    }

    private void NavigateTo(int menuIndex)
    {
        switch (menuIndex)
        {
            default:
            case 0:
                desieredMenuPosition = Vector3.zero;
                break;
            case 1:
                if (activeScreenIndex == 2)
                {
                    desieredMenuPosition = Vector3.right * 1920;
                }
                else if (activeScreenIndex == 1)
                {
                    desieredMenuPosition = Vector3.right * (1280 + (1280 / 2));
                }
                else if (activeScreenIndex == 0)
                {
                    desieredMenuPosition = Vector3.right * (720 + 81);
                }
                break;
            case 2:
                if (activeScreenIndex == 2)
                {
                    desieredMenuPosition = Vector3.left * 1920;
                }
                else if (activeScreenIndex == 1)
                {
                    desieredMenuPosition = Vector3.left * (1280 + (1280 / 2));
                }
                else if (activeScreenIndex == 0)
                {
                    desieredMenuPosition = Vector3.left * (720 + 81);
                }
                break;
            case 3:
                desieredMenuPosition = Vector3.down * Screen.height;
                break;
            case 4:
                desieredMenuPosition = Vector3.up * Screen.height;
                break;
        }
    }

    //button
    public void newGame()
    {
        NavigateTo(1);
        Debug.Log("NewGame");
    }
    public void continueGame()
    {
        NavigateTo(3);
        Debug.Log("continue");
    }
    public void options()
    {
        NavigateTo(2);
        Debug.Log("options");
    }
    public void info()
    {
        NavigateTo(4);
        Debug.Log("info");
    }
    public void exit()
    {
        Debug.Log("exit");
        Application.Quit();
    }
    public void backToMenu()
    {
        NavigateTo(0);
        Debug.Log("back to menu");
    }
    public void startGame()
    {
        //load game scene
        Application.LoadLevel(1);
    }

    //
    private void optionPanel(int currentIndex)
    {
        Debug.Log("Selecting options: " + currentIndex);
    }
    private void newGamePanel(int currentIndex)
    {
        Debug.Log("Selecting Levels: " + currentIndex);
    }

    //screen resolution

    public void SetScreenResolution(int i)
    {
        if(resolutionToggels[i].isOn)
        {
            activeScreenIndex = i;
            float aspectRatio = 16 / 9f;
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / aspectRatio), false);
            PlayerPrefs.SetInt("screen res index", activeScreenIndex);
            PlayerPrefs.Save();
        }
    }

    public void SetFullScreen(bool isFullScreen)
    {
        for (int i = 0; i < resolutionToggels.Length; i++)
        {
            resolutionToggels[i].interactable = !isFullScreen;
        }
        if(isFullScreen)
        {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolutions = allResolutions[allResolutions.Length - 1];
            Screen.SetResolution(maxResolutions.width, maxResolutions.height, true);
        }
        else
        {
            SetScreenResolution(activeScreenIndex);
        }
        PlayerPrefs.SetInt("fullscreen", ((isFullScreen) ? 1 : 0));
        PlayerPrefs.Save();
    }
    
    public void SetMusicVolume()
    {
        //add save ect in gamemaster
    }

    public void SetSoundsVolume()
    {
        //add save ect in gamemaster
    }

}

/*
 * if (activeScreenIndex == 2)
                {
                    desieredMenuPosition = Vector3.left * 1920;
                }
                else if (activeScreenIndex == 1)
                {
                    desieredMenuPosition = Vector3.left * (1280 + (1280 / 2));
                }
                else if (activeScreenIndex == 0)
                {
                    desieredMenuPosition = Vector3.left * (720 + 81);
                }
*/