using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO; //input output

//Audiomanager
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f, 0.5f)]
    public float randomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        //source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        //source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
    }
}

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;
    private Player player;
    private GameObject menu;

    public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay = 2;

    //AudioManger
    public static GameMaster instatce;

    [SerializeField]
    Sound[] sounds;

    //coins
    public int points;
    public Text PointText;

    //high coins
    public int highPoint = 0;

    //dialogues
    public Text DialoguesText;

    //portal
    public Text InputText;

    //health
    public Text HealthText;

    //death -> ilośc śmierci, aby wypisywać je w menu
    public int playerDeath;

    //ilość zabitych przeciwników
    public int enemyKill;

    //player on sceene
    public int howmuchplayer;

    //high Score (XP)
    public int highScore;

    //inputField -- name of your game save
    public string filename;

    //load game
    public string loadgamename;

    //SaveScript - playerPosition
    public float playerPositionX;
    public float playerPositionY;
    public float playerPositionZ;

    public int playerPoints; //to powinno być w PlayerStats !
    
    void Awake()
    {
        Application.targetFrameRate = 60; //frames per second

        if (instatce != null)
        {
            Debug.Log("more than AudioManager in the scene");
        }
        else
        {
            instatce = this;
        }
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
        //find gameMaster
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>(); //trzeba było dodać TAG
            //menu = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<GameObject>();
        }

        //AudioManager
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }

        //ADD SAVEGAME & LOADED DATA, ADD MENU DATA
        //to są zapisy statyystyk podczas gry. niestety musze zrobić jeszcze odczyst zapisów z plików binarnych

        //save game -> filename
        if (PlayerPrefs.HasKey("filename"))
        {
            if (Application.loadedLevel == 1) //filename != null
            {
                PlayerPrefs.DeleteKey("filename");
                filename = filename;
            }
            else
            {
                filename = PlayerPrefs.GetString("filename");
            }
        }
        //save points "in game"
        if (PlayerPrefs.HasKey("Points"))
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
        //save game -> PlayerDeath
        if (PlayerPrefs.HasKey("PlayerDeath"))
        {
            if (Application.loadedLevel == 1) //filename != null
            {
                PlayerPrefs.DeleteKey("PlayerDeath");
                playerDeath = playerDeath;
            }
            else
            {
                playerDeath = PlayerPrefs.GetInt("PlayerDeath");
            }
        }
        //save game -> EnemyKils
        if (PlayerPrefs.HasKey("EnemyKils"))
        {
            if (Application.loadedLevel == 1) //filename != null
            {
                PlayerPrefs.DeleteKey("EnemyKils");
                enemyKill = enemyKill;
            }
            else
            {
                enemyKill = PlayerPrefs.GetInt("EnemyKils");
            }
        }
        //save HighPoint ---------> jeśli będę dodawać zapis życia itp to można to zrobić chyba w tej samej pętli, anie twrzyć niepotrzebny dodatkowy kod --> najwyższy wynik uzyskanyuch pieniędzy
        if (PlayerPrefs.HasKey("highPoint"))
        {
            //calculate maxScore ect
            highScore = (points / 2); //for example...
            //and save
            highPoint = PlayerPrefs.GetInt("highPoint");
        }
        //save game ->PlayerHighScore
        if (PlayerPrefs.HasKey("PlayerHighScore"))
        {
            if (Application.loadedLevel == 1) //filename != null
            {
                PlayerPrefs.DeleteKey("PlayerHighScore");
                highScore = highScore;
            }
            else
            {
                highScore = PlayerPrefs.GetInt("PlayerHighScore");
            }
        }
        //to jest dodatkowo, jak bym chciął zapisac preferencje gracza itp, ale na szczęscie dziła to już w innym skrypcie
        //PlayerPrefs.SetString("name", filename);
    }

    void Update()
    {
        Debug.Log(filename + " :Oto filename");
        Debug.Log(loadgamename + " :Oto loadgamename");
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
    
    public void LoadGameName()
    {
        if (File.Exists(Application.persistentDataPath + "/" + loadgamename + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + loadgamename + ".dat", FileMode.Open);

            PlayerStats data = (PlayerStats)bf.Deserialize(file);
            file.Close();

            playerPositionX = data.playerPosX;
            playerPositionY = data.playerPosY;
            playerPositionZ = data.playerPosZ;
            points = playerPoints;
            //add load level

            Debug.Log(Application.persistentDataPath + " załadowano");
        }
    }
    public void DeleteGameName()
    {
        if (File.Exists(Application.persistentDataPath + "/" + loadgamename + ".dat"))
        {
            File.Delete(Application.persistentDataPath + "/" + loadgamename + ".dat");
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

    public void loadGameName(string newText)
    {
        loadgamename = newText;
    }

    //Respawn player&Kill player
    public IEnumerator RespawnPlayer(int howmuch)
    {
        Debug.Log("TODO: Add waiting for spawn sound");
        yield return new WaitForSeconds(spawnDelay);

        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log("TODO: Add Spawn Particles");
    }

    public static void KillPlayer(Player player)
    {
        gm.playerDeath += 1;
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer(1));
        //add if with checking how much we have player on scene
    }

    //AudioManager
    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }
        //add no sound
        Debug.Log("AudioManager: Sound not found -> " + _name);
    }
}