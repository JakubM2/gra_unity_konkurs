using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO; //input output

public class GameMaster : MonoBehaviour
{

    public static GameMaster gm;
    private Player player;

    public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay = 2;

    //coins
    public int points;
    public Text PointText;

    //high coins
    public int highPoint = 0;

    //portal
    public Text InputText;

    //health
    public Text HealthText;

    //death -> ilośc śmierci, aby wypisywać je w menu
    public int death;

    //high Score (XP)
    public int highScore;

    //inputField -- name of your game save
    public string filename;

    //SaveScript - playerPosition
    public float playerPositionX;
    public float playerPositionY;
    public float playerPositionZ;

    public int playerPoints; //to powinno być w PlayerStats !
    
    void Awake()
    {
        Application.targetFrameRate = 60; //frames pre second

        /*if(gm == null)
        {
            DontDestroyOnLoad(gameObject);
            gm = this;
        }
        else if(gm != this)
        {
            Destroy(gm);
        }*/
    }

    void Start()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>(); //trzeba było dodać TAG
        }

        //save points "in game"
        if(PlayerPrefs.HasKey("Points"))
        {
            if(Application.loadedLevel == 1) //chyba 1, a nie 0
            {
                PlayerPrefs.DeleteKey("Points");
                points = 0;
            }
            else
            {
                points = PlayerPrefs.GetInt("Points");
            }
        }
        
        //save HighPoint ---------> jeśli będę dodawać zapis życia itp to można to zrobić chyba w tej samej pętli, anie twrzyć niepotrzebny dodatkowy kod --> najwyższy wynik uzyskanyuch pieniędzy
        if (PlayerPrefs.HasKey("highPoint"))
        {
            highPoint = PlayerPrefs.GetInt("highPoint");
        }

        //calculate maxScore ect
        highScore = (points/highPoint)*100; //for example...
    }

    void Update()
    {
        fileNameSave();
    }

    //Save&Load&Delete
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + filename + ".dat");

        PlayerStats data = new PlayerStats();
        data.playerPosX = playerPositionX;
        data.playerPosY = playerPositionY;
        data.playerPosZ = playerPositionZ;
        playerPoints = points;

        Debug.Log(Application.persistentDataPath + " zapisano");

        bf.Serialize(file, data);
        file.Close();
    }
    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/" + filename + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + filename + ".dat", FileMode.Open);

            PlayerStats data = (PlayerStats)bf.Deserialize(file);
            file.Close();

            playerPositionX = data.playerPosX;
            playerPositionY = data.playerPosY;
            playerPositionZ = data.playerPosZ;
            points = playerPoints;

            Debug.Log(Application.persistentDataPath + " załadowano");
        }
    }
    public void Delete()
    {
        if(File.Exists(Application.persistentDataPath + "/" + filename + ".dat"))
        {
            File.Delete(Application.persistentDataPath + "/" + filename + ".dat");
        }
    }

    [Serializable]
    class PlayerStats
    {
        public float playerPosX;
        public float playerPosY;
        public float playerPosZ;
        //public int playerPoints;
    }

    public void fileName(string newText)
    {
        filename = newText;
    }

    //save game -> filename
    public void fileNameSave()
    {
        if (PlayerPrefs.HasKey("fielname"))
        {
            if (filename == null)
            {
                PlayerPrefs.DeleteKey("filename");
                filename = filename;
            }
            else
            {
                filename = PlayerPrefs.GetString("filename");
            }
        }
    }

    //Respawn player&Kill player
    public IEnumerator RespawnPlayer()
    {
        Debug.Log("TODO: Add waiting for spawn sound");
        yield return new WaitForSeconds(spawnDelay);

        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log("TODO: Add Spawn Particles");
    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer());
    }
}