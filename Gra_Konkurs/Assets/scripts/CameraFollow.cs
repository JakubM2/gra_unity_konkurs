using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float m_speed = 0.1f;
    Camera mycam;

    void Start()
    {
        mycam = GetComponent<Camera>();
    }

    void Update()
    {

        if (target == null)
            return;

        mycam.orthographicSize = (Screen.height / 100f);

        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, m_speed) + new Vector3(0, 0, -10);
        }
    }
}

