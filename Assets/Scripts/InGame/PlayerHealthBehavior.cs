using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerHealthBehavior : MonoBehaviour {

	public float playerHealth;
	public bool isAlive;


	private GameObject inflictingDamageObj;
	private GameObject winLoseCondition;
	private bool hasRecievedDamage;

	// Use this for initialization
	void Start () {
		isAlive = true;
		hasRecievedDamage = false;
		inflictingDamageObj = null;
		winLoseCondition = GameObject.Find ("HUDController");
	
	}


	void PlayerDeath(){
		
		isAlive = false;
		Debug.Log ("You dead");

	}

	void OnTriggerEnter(Collider other){
		// Checking the collision to recieve damage
		// Will not take damage from one object twice
		if (other.gameObject.tag == "inflictsDamage") {
			if (hasRecievedDamage) {
				if (other.gameObject != inflictingDamageObj) {
					hasRecievedDamage = false;
				}
			}
			if (!hasRecievedDamage) {
				float amountOfDamage = 0;
				inflictingDamageObj = other.gameObject;
				//pull the amount of damage from the inflictingDamageObject
				amountOfDamage = inflictingDamageObj.GetComponent<TestEnemyDamage>().myDamageAmount;
				Debug.Log ("I've been hit by " + inflictingDamageObj.name);
				PlayerReceiveDamage (amountOfDamage);
				GamePad.SetVibration (PlayerIndex.One, 0.5f, 0.5f);
				StartCoroutine (timeToVibrate());
				hasRecievedDamage = true;
				Destroy (other.gameObject);
			}
		}
	}

	void PlayerReceiveDamage(float amountToInflict){
		if (playerHealth - amountToInflict <= 0 && !winLoseCondition.GetComponent<WinLoseTextBehavior>().endOfGame) {
			playerHealth = 0;
			PlayerDeath ();
		}
		else{
			playerHealth = playerHealth - amountToInflict;
		}
	}

	IEnumerator timeToVibrate(){
		yield return new WaitForSeconds (0.3f);
		GamePad.SetVibration (PlayerIndex.One, 0, 0);
	}
}
