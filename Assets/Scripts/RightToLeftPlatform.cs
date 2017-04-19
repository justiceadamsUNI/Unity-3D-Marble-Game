using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightToLeftPlatform : MonoBehaviour {
    private float initialX;
    public float speed;
    public float travelDistance;


    // Use this for initialization
    void Start()
    {
        initialX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            initialX + (travelDistance / 2) - Mathf.PingPong(Time.time * speed, 10),
            transform.position.y,
            transform.position.z);

    }
}
