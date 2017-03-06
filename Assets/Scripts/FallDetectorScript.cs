using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetectorScript : MonoBehaviour {
    float initialY;


	// Use this for initialization
	void Start () {
        initialY = transform.position.y;
		
	}
	
	// Update is called once per frame
	void Update () {
        float x = transform.parent.position.x;
        float z = transform.parent.position.z;

        transform.position = new Vector3(x, initialY, z);
	}
}
