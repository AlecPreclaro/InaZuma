using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndMenuSelectionBehavior : MonoBehaviour {

	public InazumaMenuController menuController;
	public string sceneToPlay;
	private CursorIndexTracker myIndex;

	// Use this for initialization
	void Start () {
		myIndex = GetComponent<CursorIndexTracker> ();
	}

	// Update is called once per frame
	void Update () {
		Select ();
	}

	void Select(){
		if (Input.GetButtonDown ("AButton") || Input.GetButtonDown ("StartButton")) {
			if (myIndex.currentCursorIndex == 0) {
				//Go to our current gameplay scene
				SceneManager.LoadScene(sceneToPlay);
				//Debug.Log("Starting game scene");
			} else if (myIndex.currentCursorIndex == 1) {
				SceneManager.LoadScene("mainMenuScene");
			} 
		}
	}

	void Deselect (){
		if (Input.GetButtonDown ("BButton")) {
		}
	}
}
