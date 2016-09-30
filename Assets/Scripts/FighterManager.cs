using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FighterManager : MonoBehaviour {

	//public List<GameObject> fightersInScene = new List<GameObject> ();
	public PlayerTargetSystem playerstargetSystem;

	public float timeBeforeDestroy;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	//Called when a fighter is killed from enemy health

	public void CheckIfEnemyDead(GameObject deadUnit){
		foreach (Transform childDestroy in deadUnit.transform) {
			if (playerstargetSystem.currentTarget == childDestroy.gameObject) {
				Debug.Log ("Removed from current target");
				playerstargetSystem.currentTarget = null;
				playerstargetSystem.targetOn = false;
			}
			if (playerstargetSystem.potentialTarget == childDestroy.gameObject) {
				Debug.Log ("Removed from potential target");
				playerstargetSystem.potentialTarget = null;
			}
			if (playerstargetSystem.targetsOnScreen.Contains (childDestroy.gameObject)) {
				Debug.Log ("Removed from targetsOnScreen");
				playerstargetSystem.targetsOnScreen.Remove (childDestroy.gameObject);
				Destroy (childDestroy.gameObject);
			}
			
		}
		if (playerstargetSystem.currentTarget == deadUnit) {
			Debug.Log ("Removed from current target");
			playerstargetSystem.currentTarget = null;
			playerstargetSystem.targetOn = false;
		}
		if (playerstargetSystem.potentialTarget == deadUnit) {
			Debug.Log ("Removed from potential target");
			playerstargetSystem.potentialTarget = null;
		}
		if (playerstargetSystem.targetsOnScreen.Contains (deadUnit)) {
			Debug.Log ("Removed from targetsOnScreen");
			playerstargetSystem.targetsOnScreen.Remove (deadUnit);
			Destroy (deadUnit);
		} else {
			Destroy (deadUnit);
		}
		//If we're dealing with a core remove it from its large ship core list.
		//If this was the last core it will be destroyed with the large ship parent object
		/*if (deadUnit.tag == "core"){
			deadUnit.GetComponent<CoreLinkToCarrier> ().myLargeShip.GetComponent<EnemyLargeShipDeathBehavior> ().tempCore = deadUnit.GetComponent<CoreLinkToCarrier> ().myLargeShip.GetComponent<EnemyLargeShipDeathBehavior> ().myCores [0];
			deadUnit.GetComponent<CoreLinkToCarrier> ().myLargeShip.GetComponent<EnemyLargeShipDeathBehavior> ().myCores.Remove (deadUnit);
			deadUnit.GetComponent<CoreLinkToCarrier> ().myLargeShip.GetComponent<EnemyLargeShipDeathBehavior> ().CheckIfDestroyed ();
			//Destroy (deadUnit);
		}*/
		//If we're not dealing with a core just destroy the object




	}
}
