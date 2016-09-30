using UnityEngine;
using System.Collections;

public class DisplayControlsInGame : MonoBehaviour {

	public GameObject controlsDisplay;

	private bool showingControls;

	// Use this for initialization
	void Start () {
		showingControls = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("StartButton")) {
			showingControls = !showingControls;
			controlsDisplay.SetActive (showingControls);
			if (showingControls) {
				Time.timeScale = 0;
			} else {
				Time.timeScale = 1;
			}
		}
	}
}
