using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    //gameMaster -> saveScore, highScore etc
    private GameMaster gm;

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
    }

    private void Update()
    {
        //Score Board -> Update text & Update Values
            maxCash.text = ("Maks Pieniędzy: " + gm.highScore); //dokończyć dodawanie danych ze skryptu SaveScript
            maxHP.text = ("Maks Zachowango życia: " + 0); //dokończyć
            maxPoint.text = ("maks Punktów: " + gm.highPoint); //dokończyć
            death.text = ("Ilość Śmierci: " + gm.death); //dokończyć
            enemyKill.text = ("Zabitych Przeciwników: " + 0); //dokończyć
        
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
                desieredMenuPosition = Vector3.right * Screen.width;
                break;
            case 2:
                desieredMenuPosition = Vector3.left * Screen.width;
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
}