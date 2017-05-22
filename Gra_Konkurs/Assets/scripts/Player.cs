using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    private GameMaster gm;

    [SerializeField]
    private GameObject fireball;

    public class PlayerStats
    {
        public float Health = 100f;
        //another player stats
    }

    public PlayerStats playerStats = new PlayerStats();

    public int fallBoundary = -20;
    public int spikesDamage = 10;
    public int fireDamage = 25;
    public int arrowDamage = 30;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    void FixedUpdate ()
    {
        gm.HealthText.text = ("Życie: " + playerStats.Health);
        gm.PointText.text = ("Pieniądze: " + gm.points);

        if (transform.position.y <= fallBoundary)
        {
            DamagePlayer (99999999);
        }
        //shoot
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public void DamagePlayer (int damage)
    {
        playerStats.Health -= damage;
        if (playerStats.Health <= 0)
        {
            /*highPoint --> before kill player
            if(PlayerPrefs.HasKey("highPoint"))
             {
                 if(PlayerPrefs.GetInt("highPoint") < gm.highPoint)
                 {
                     PlayerPrefs.GetInt("highPoint", gm.highPoint);
                 }
             }
             else
             {
                 PlayerPrefs.GetInt("highPoint", gm.highPoint);
             }*/
            GameMaster.KillPlayer(this);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Coins"))
        {
            Destroy(col.gameObject);
            gm.points += 1;
            gm.PlaySound("CoinsCollect");//play sounds when we collect some coins
        }
        else if (col.CompareTag("Heart"))
        {
            Destroy(col.gameObject);
            if (playerStats.Health != 100)
            {
                playerStats.Health += (100 - playerStats.Health);
                gm.PlaySound("HealthPlayer"); //play sound when player collect health
            }
        } else if (col.CompareTag("Spikes"))
        {
            DamagePlayer(spikesDamage);
            gm.PlaySound("DamagePlayer"); //play sound when player "is hit" -> prefect English xddd
            //change player color -->red
        }
        else if (col.CompareTag("Platform_fire_0.4"))
        {
            gm.PlaySound("DamagePlayer"); //play sound when player "is hit" -> prefect English xddd
            DamagePlayer(fireDamage);
        }
        else if (col.CompareTag("EnemyArrow"))
        {
            gm.PlaySound("DamagePlayer"); //play sound when player "is hit" -> prefect English xddd
            DamagePlayer(arrowDamage);
        } else if (col.CompareTag("Torch"))
        {
            //SAVE GAME!!!!
            SavePlayer();
        }
    }

    public void Shoot()
    {
        GameObject clone = Instantiate(fireball) as GameObject;
        clone.transform.position = transform.position;
        gm.PlaySound("Fire");
    }

    public void SavePlayer()
    {
        GameMaster.gm.Save();
        {
            GameMaster.gm.playerPositionX = transform.position.x;
            GameMaster.gm.playerPositionY = transform.position.y;
            GameMaster.gm.playerPositionZ = transform.position.z;
            GameMaster.gm.points = GameMaster.gm.playerPoints;//zmienić to!!!
        }
    }
}

//save&load game, we can only save game when we playing, but we can delete and load game in MeinMenu
/*if(Input.GetKeyDown(KeyCode.X))
{
    GameMaster.gm.Delete();
}
if(Input.GetButtonDown("Save")) //f5
{
    GameMaster.gm.Save();
    {
        GameMaster.gm.playerPositionX = transform.position.x;
        GameMaster.gm.playerPositionY = transform.position.y;
        GameMaster.gm.playerPositionZ = transform.position.z;
        GameMaster.gm.points = GameMaster.gm.playerPoints;//zmienić to!!!
    }
}
if (Input.GetButtonDown("Load")) //f9
{
    GameMaster.gm.Load();
    {
        transform.position = new Vector4
        (
           GameMaster.gm.playerPositionX,
           GameMaster.gm.playerPositionY,
           GameMaster.gm.playerPositionZ,
           GameMaster.gm.playerPoints             //points or playerPoints
        );
    }
};*/
