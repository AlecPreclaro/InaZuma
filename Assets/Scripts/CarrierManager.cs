using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CarrierManager : MonoBehaviour {
    
    public List<GameObject> carrierList = new List<GameObject>();

    void Start () {
        foreach (GameObject carrier in GameObject.FindGameObjectsWithTag("Carrier"))
        {
            carrierList.Add(carrier); 
        }
	}

	void Remove (GameObject carrier)
    {
        //tell each remaining carrier to add +2 to maxShips
        //update shipsArray size
		foreach (GameObject updateCarrier in carrierList) {
			CarrierBehavior carrierBehavior = updateCarrier.GetComponent<CarrierBehavior> ();
			carrierBehavior.maxShips = carrierBehavior.maxShips + 2; 
			carrierBehavior.shipsArray = new GameObject[carrierBehavior.maxShips];
		}
    }
}
