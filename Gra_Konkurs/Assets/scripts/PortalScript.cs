using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PortalScript : MonoBehaviour {

    public int LevelToLoad;

    private GameMaster gm;
    private GameMaster player;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<GameMaster>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            gm.InputText.text = ("[E] - Aby Wyjść");
            if (Input.GetKeyDown("e"))
            {
                SaveScore();
                Application.LoadLevel(LevelToLoad);
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
                Application.LoadLevel(LevelToLoad);
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
    void SaveScore()
    {
        PlayerPrefs.SetInt("Points", gm.points);
        PlayerPrefs.SetInt("highPoint", gm.highPoint);
        PlayerPrefs.SetString("filename", gm.filename);
        //add player prefs + player int Health
    }
}
