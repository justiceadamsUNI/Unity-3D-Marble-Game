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
    private float animationTime = 0.0f;
    private bool respawnAnimationPlaying;
    private bool destroyAnimationPlaying;
    public int score = 0;


    void Start(){
		rb = GetComponent<Rigidbody> ();
        intialPosition = transform.position;

        //Disable respawn and destroy animations
        setDestroyAnimationEnabled(false);
        setRespawnAnimationEnabled(false);
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
                startDestroyAnimation();
                break;
			case "Level Finish Detector":
				//get current level scene, unlock next level scene

				checkCurrentLevel ();
				SceneManager.LoadScene ("LevelSelect");
                break;
        }
    }

    private void Update() {
        animationTime -= Time.deltaTime;
        if (animationTime < 0 && 
            (respawnAnimationPlaying || destroyAnimationPlaying)) {

            if (destroyAnimationPlaying) {
                setDestroyAnimationEnabled(false);
                destroyAnimationPlaying = false;
                respawn();
            } else if(respawnAnimationPlaying) {
                setRespawnAnimationEnabled(false);
                respawnAnimationPlaying = false;
            }
        }
    }

    private void respawn() {
        animationTime = 2.0f;
        transform.position = intialPosition;

        //enable animation
        respawnAnimationPlaying = true;
        setRespawnAnimationEnabled(true);
    }

    private void startDestroyAnimation() {
        animationTime = 1.5f;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(Vector3.zero);

        // enable animation
        destroyAnimationPlaying = true;
        setDestroyAnimationEnabled(true);
    }

    private void setDestroyAnimationEnabled(bool enabled) {
        transform.GetChild(1).gameObject.SetActive(enabled);
    }

    private void setRespawnAnimationEnabled(bool enabled) {
        transform.GetChild(0).gameObject.SetActive(enabled);
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
