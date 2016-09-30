using UnityEngine;
using System.Collections;

public class PlayerGoingOutOfBounds : MonoBehaviour {
	public float xPositiveBound, xNegativeBound, yPositiveBound, yNegativeBound,
	zPositiveBound, zNegativeBound, jumpBackAmount;

	

	void Update () {
		KeepPlayerInBounds ();
	}

	void KeepPlayerInBounds(){
		if (transform.position.x > xPositiveBound) {
			transform.position = new Vector3 (transform.position.x - jumpBackAmount, transform.position.y, transform.position.z);
		}
		if (transform.position.x < xNegativeBound) {
			transform.position = new Vector3 (transform.position.x + jumpBackAmount, transform.position.y, transform.position.z);
		}
		if (transform.position.y > yPositiveBound) {
			transform.position = new Vector3 (transform.position.x, transform.position.y - jumpBackAmount, transform.position.z);
		}
		if (transform.position.y < yNegativeBound) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + jumpBackAmount, transform.position.z);
		}
		if (transform.position.z > zPositiveBound) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z - jumpBackAmount);
		}
		if (transform.position.z < zNegativeBound) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + jumpBackAmount);
		}
	}
}
