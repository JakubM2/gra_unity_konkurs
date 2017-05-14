using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PortalScript : MonoBehaviour {

    public int LevelToLoad;

    private GameMaster gm;
    private GameObject player;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<GameObject>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            gm.InputText.text = ("[E] - Aby Wyjść");
            if (Input.GetKeyDown("e"))
            {
                SaveScore();
                if(Application.loadedLevel == 1)
                {
                    Application.LoadLevel(LevelToLoad);
                } else if(Application.loadedLevel ==2)
                {
                    Application.LoadLevel(LevelToLoad+1);
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if(Input.GetKeyDown("e"))
            {
                SaveScore();
                if (Application.loadedLevel == 1)
                {
                    Application.LoadLevel(LevelToLoad);
                }
                else if (Application.loadedLevel == 2)
                {
                    Application.LoadLevel(LevelToLoad + 1);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            gm.InputText.text = (" ");
        }
    }

    //to zapisuje wynik tylko "po przejściu przez drzwi", a trzeba w innym pliku stworzyć cały system zapisu, aby korzystać z niego w mainMenu, a także system zapisu
    public void SaveScore()
    {
        PlayerPrefs.SetInt("Points", gm.points);
        PlayerPrefs.SetInt("highPoint", gm.highPoint);
        PlayerPrefs.SetString("filename", gm.filename);
        PlayerPrefs.SetInt("PlayerDeath", gm.playerDeath);
        PlayerPrefs.SetInt("PlayerHighScore", gm.highScore);
        PlayerPrefs.SetInt("EnemyKils", gm.enemyKill);
        //PlayerPrefs.SetFloat("PlayerHealth", player.playerStats.Health); ->>Opcional
    }
}
