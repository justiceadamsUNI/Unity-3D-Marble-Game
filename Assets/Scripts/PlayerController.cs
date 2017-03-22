using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed;
	private Rigidbody rb;
    private Vector3 intialPosition;

	private int numOfLevels = 8;
	private int currentLevel;
	public int score = 0;

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
			case "Level Finish Detector":
				//get current level scene, unlock next level scene

				checkCurrentLevel ();
				SceneManager.LoadScene ("LevelSelect");
                break;
        }
    }

    private void respawn() {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(Vector3.zero);

        transform.position = intialPosition;
    }
	 
	void checkCurrentLevel(){

		for (int i = 1; i < numOfLevels; i++) {
			if (SceneManager.GetActiveScene().name == "Level" + i) {

				currentLevel = i;
				saveGame ();
			}

		}

	}

	void saveGame(){
		int nextLevel = currentLevel + 1;
		if (nextLevel < numOfLevels) {
			PlayerPrefs.SetInt ("Level" + nextLevel.ToString (), 1); //unlocks next level
		}
		PlayerPrefs.SetInt("Level" + currentLevel.ToString() + "_score", score);

	}
}
