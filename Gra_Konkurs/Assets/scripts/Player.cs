using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    private GameMaster gm;

    public class PlayerStats
    {
        public float Health = 100f;
        //another player stats
    }

    public PlayerStats playerStats = new PlayerStats();

    public int fallBoundary = -20;

    //Health text
    public Text HealthText;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    void Update ()
    {
        HealthText.text = ("Życie: " + playerStats.Health);

        if (transform.position.y <= fallBoundary)
        {
            DamagePlayer (99999999);
        }
    }

    public void DamagePlayer (int damage)
    {
        playerStats.Health -= damage;
        if (playerStats.Health <= 0)
        {
            GameMaster.KillPlayer(this);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Coins"))
        {
            Destroy(col.gameObject);
            gm.points += 1;
            //add sounds
        }
        else if (col.CompareTag("Heart"))
        {
            Destroy(col.gameObject);
            if (playerStats.Health != 100)
            {
                playerStats.Health += (100 - playerStats.Health); // sprawdzić poprawność dziłania i ogólnie jak coś to poproawić et cetera
                //add sounds
            }
        }

    }
}
