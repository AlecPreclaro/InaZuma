using UnityEngine;
using System.Collections;

public class TargetableBehavior : MonoBehaviour {

	public GameObject player;
	public bool isVisible;
	public float distanceFromPlayer;

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update(){
		distanceFromPlayer = Vector3.Distance (transform.position, player.transform.position);
		if (distanceFromPlayer < player.GetComponent<PlayerTargetSystem> ().targetMaxDistance && isVisible && !player.GetComponent<PlayerTargetSystem>().targetsOnScreen.Contains(this.gameObject) ) {
			player.GetComponent<PlayerTargetSystem> ().targetsOnScreen.Add (this.gameObject);	
		}
		else if((distanceFromPlayer > player.GetComponent<PlayerTargetSystem> ().targetMaxDistance || !isVisible) && player.GetComponent<PlayerTargetSystem>().targetsOnScreen.Contains(this.gameObject))  {
			player.GetComponent<PlayerTargetSystem> ().targetsOnScreen.Remove (this.gameObject);
		}
	}

	void OnBecameVisible(){
		if (player != null) {
			isVisible = true;

		}
	}

	void OnBecameInvisible(){
		if (player != null) {
			isVisible = false;

		}
	}
}
