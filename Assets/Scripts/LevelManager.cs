using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

//tutorial help found from https://www.youtube.com/watch?v=xSDfSDTtUMs

public class LevelManager : MonoBehaviour {

	[System.Serializable]
	public class Level {
		public string LevelText;
		public int isUnlocked; //UnLocked
		public bool isPlayable; //IsInteractable

		public Button.ButtonClickedEvent OnClickedEvent;

	}

	public GameObject button;
	public Transform Spacer;
	public List<Level> LevelList;

	private int numOfLevels = 8;
	private int currentLevel;



	// Use this for initialization
	void Start (){
		FillList ();
	}


	void FillList () {

		foreach(var level in LevelList) {
			GameObject newButton = Instantiate (button) as GameObject;
			LevelButton actualButton = newButton.GetComponent<LevelButton>();
			actualButton.LevelText.text = level.LevelText;


			if (PlayerPrefs.GetInt("Level" + actualButton.LevelText.text) == 1) {
			
				level.isUnlocked = 1;
				level.isPlayable = true;
			}

			actualButton.unlocked = level.isUnlocked;
			actualButton.GetComponent<Button> ().interactable = level.isPlayable;

			newButton.transform.SetParent (Spacer, false);
		}

		saveAll ();


	}

	void saveAll() {
	
		GameObject[] allButtons = GameObject.FindGameObjectsWithTag ("LevelButton");
		foreach (GameObject buttons in allButtons) {
			LevelButton button = buttons.GetComponent<LevelButton> ();
			PlayerPrefs.SetInt ("Level" + button.LevelText.text, button.unlocked);


		}

	}

//	void checkCurrentLevel(){
//
//		for (int i = 1; i < numOfLevels; i++) {
//			loadedLevel = SceneManager.GetActiveScene;
//		
//
//		}
//	}


}
