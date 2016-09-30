using UnityEngine;
using System.Collections;

public class WhatAmIHitting : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<Rigidbody> ().velocity.magnitude > 0.0f) {
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
		}
	
	}

	void OnCollisionEnter(Collision other){
		Debug.Log (other.gameObject + " collided with me!");
		if (GetComponent<Rigidbody> ().velocity.magnitude > 0.0f) {
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
		}
	}
}
