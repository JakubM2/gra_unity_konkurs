using System.Collections;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {

    public Transform background;
    public float smoothing = 1f;

    private Transform camera;

    void Awake()
    {
        camera = Camera.main.transform;
    }

    void Update()
    {
        if (background)
        {
            transform.position = Vector2.Lerp(transform.position, background.position, smoothing) + new Vector2(0, 0);
        }
    }
}
