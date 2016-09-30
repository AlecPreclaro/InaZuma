using UnityEngine;
using System.Collections;

public class EnemyHealthBehavior : MonoBehaviour {

	public bool enemyDead;
	public float health, playerHealthRewardAmount;
	private FighterManager myEnemyManager;
	private bool thisIsACore;
	private CoreLinkToCarrier myCoreLink;
	private GameObject player;

	void Start () {
		if (GetComponent<CoreLinkToCarrier> () != null) {
			thisIsACore = true;
			myCoreLink = GetComponent<CoreLinkToCarrier> ();
		} else {
			thisIsACore = false;
		}
		myEnemyManager = GameObject.Find ("FighterManager").GetComponent<FighterManager> ();
		player = myEnemyManager.GetComponent<FighterManager> ().playerstargetSystem.gameObject;
		enemyDead = false;
	}


	public void TakeDamage(float amountOfDamage)
	{
		if (health - amountOfDamage <= 0) {
			health = 0;
			player.GetComponent<PlayerHealthBehavior> ().playerHealth += playerHealthRewardAmount;
			if (player.GetComponent<PlayerHealthBehavior> ().playerHealth > 100) {
				player.GetComponent<PlayerHealthBehavior> ().playerHealth = 100;
			}
			EnemyDeath ();
		} else {
			health = health - amountOfDamage;
		}
	}

	void EnemyDeath(){
		enemyDead = true;
		transform.position = new Vector3 (99999, 99999, 99999);
		if (thisIsACore) {
			myCoreLink.RemoveFromLargeShip ();
		}
		myEnemyManager.CheckIfEnemyDead (this.gameObject);
	}
		
}
