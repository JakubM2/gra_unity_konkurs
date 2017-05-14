using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    public GameObject PauseUI;

    private bool paused = false;

    void Start()
    {
        PauseUI.SetActive(false);
    }
    void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            paused = !paused;
        }
        if(paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        if(!paused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }


    //button
    public void Exit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        //Options
        Application.LoadLevel(Application.loadedLevel);
    }
    /*public void SaveGame()
    {
        //add save game script
    }*/
    public void MainMenu()
    {
        Application.LoadLevel(0);
    }
    public void Resume()
    {
        paused = false;
    }
}