using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDownPlatform : MonoBehaviour {
    private float initialY;
    public float speed;
    public float travelDistance;


    // Use this for initialization
    void Start()
    {
        initialY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            transform.position.x,
            initialY + (travelDistance / 2) - Mathf.PingPong(Time.time * speed, 10),
            transform.position.z);

    }
}
