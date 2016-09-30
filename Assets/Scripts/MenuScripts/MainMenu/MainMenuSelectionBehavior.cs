using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuSelectionBehavior : MonoBehaviour {

	public string levelToStart;
	public InazumaMenuController menuController;
	public GameObject controlsDisplay;

	private CursorIndexTracker myIndex;
	private bool controlsDisplayedOn;

	// Use this for initialization
	void Start () {
		controlsDisplayedOn = false;
		myIndex = GetComponent<CursorIndexTracker> ();
	}
	
	// Update is called once per frame
	void Update () {
		TurnOffControlsDisplayed ();
		Select ();
		Deselect ();
	}

	void Select(){
		if (Input.GetButtonDown ("AButton") || Input.GetButtonDown ("StartButton")) {
			if (myIndex.currentCursorIndex == 0) {
				//Go to our current gameplay scene
				SceneManager.LoadScene(levelToStart);
				//Debug.Log("Starting game scene");
			} else if (myIndex.currentCursorIndex == 1) {
				controlsDisplayedOn = true;
				controlsDisplay.SetActive (true);
			} else if (myIndex.currentCursorIndex == 2) {
				Application.Quit ();
				Debug.Log("You quit the game");
			}
		}
	}

	void Deselect (){
		if (Input.GetButtonDown ("BButton")) {
			if (myIndex.currentCursorIndex == 1) {
				controlsDisplayedOn = false;
				controlsDisplay.SetActive (controlsDisplayedOn);
			}
		}
	}

	void TurnOffControlsDisplayed(){
		if (myIndex.currentCursorIndex != 1 && controlsDisplay.activeInHierarchy) {
			controlsDisplayedOn = false;
			controlsDisplay.SetActive (controlsDisplayedOn);
		}
	}
}
