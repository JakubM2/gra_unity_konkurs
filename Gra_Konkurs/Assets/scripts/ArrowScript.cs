using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

    private GameMaster gm;
    private GameObject player;
    private GameObject enemy;

    private Rigidbody2D myBody;
    private float arrowSpeed = 2f;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<GameObject>();
    }

    void FixedUpdate()
    {
        myBody.velocity = new Vector2(arrowSpeed, 0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Destroy(gameObject);
            //Player.PlayerStats.
            //Destroy(GameObject.FindGameObjectWithTag("Fire"));
            gm.PlaySound("DamagePlayer");
        }
    }
}
