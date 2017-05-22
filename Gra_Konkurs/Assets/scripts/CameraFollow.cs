using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;

    public float m_speed = 0.1f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if(player == null)
        {
            FindPlayer();
        }

        if(player)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, m_speed) + new Vector3(0, 0, -10);
        }
    }

    public void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}

