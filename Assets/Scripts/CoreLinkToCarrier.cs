using UnityEngine;
using System.Collections;

public class CoreLinkToCarrier : MonoBehaviour {

	public GameObject myLargeShip;
	private EnemyHealthBehavior myHealth;

	void Start () {
		myLargeShip = transform.parent.gameObject;
		myHealth = GetComponent<EnemyHealthBehavior> ();
	}

	void Update(){
		if (myHealth.enemyDead) {

		}
	}

	public void RemoveFromLargeShip(){
		Debug.Log ("I'm removing myself from my carriers core list");
		myLargeShip.GetComponent<EnemyLargeShipDeathBehavior> ().myCores.Remove (this.gameObject);
		myLargeShip.GetComponent<EnemyLargeShipDeathBehavior> ().CheckIfDestroyed ();
	}
}
