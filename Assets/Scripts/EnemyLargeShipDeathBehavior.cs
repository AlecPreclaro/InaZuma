using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyLargeShipDeathBehavior : MonoBehaviour {

	public GameObject myLargeShipManager;
	public List<GameObject> myCores = new List<GameObject>();

	void Start () {
		myLargeShipManager = transform.parent.gameObject;
		FindMyCores ();
	}

	void FindMyCores(){
		foreach (Transform largeShipChild in transform) {
			if (largeShipChild.gameObject.tag == "core") {
				myCores.Add (largeShipChild.gameObject);
			}
		}
	}

	//Called in Fighter Manager after a core is destroyed
	public void CheckIfDestroyed(){
		
		if (myCores.Count == 0) {
			myLargeShipManager.GetComponent<CarrierManager> ().carrierList.Remove (this.gameObject);
			GetComponent<EnemyHealthBehavior> ().TakeDamage (GetComponent<EnemyHealthBehavior> ().health);
		}
	}
}
