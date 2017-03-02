using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	private Rigidbody rb;
    private Vector3 intialPosition;

	void Start(){
		rb = GetComponent<Rigidbody> ();
        intialPosition = transform.position;
    }

	void FixedUpdate(){
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (horizontal, 0.0f, vertical);

		//rb.AddTorque (movement * speed);
		rb.AddForce (movement * speed);
	}

    void OnTriggerEnter(Collider other) {
        switch(other.tag) {
            //Happens when a player falls of the map.
            case "Fall Detector":
                respawn();
                break;
        }
    }

    private void respawn() {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(Vector3.zero);

        transform.position = intialPosition;
    }
}
