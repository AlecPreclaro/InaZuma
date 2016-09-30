using UnityEngine;
using System.Collections;

public class ObjectiveScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Time.timeScale = 0.0f;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("AButton")) {
			Time.timeScale = 1.0f;
			Destroy (this.gameObject);
		}
	
	}
}
