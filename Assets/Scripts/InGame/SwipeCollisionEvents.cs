using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class SwipeCollisionEvents : MonoBehaviour {

	public GameObject particleHitFeedback;
	public PlayerTargetSystem targetSystem;
	public FighterManager fighterManager;
	public float damageToInflictMax;
	public float damageToInflictMin;
	public float decidedDamage;
	public bool canHit;

	private SwipeBehavior attackMotion;


	void Start () {
		attackMotion = GetComponent<SwipeBehavior> ();
		canHit = true;
	
	}
	

	void Update () {
		ResetHit (canHit);
	}

	void OnTriggerEnter(Collider other){
		if ((other.gameObject.tag == "targetable" || other.gameObject.tag == "core") && attackMotion.isSwiping) {
			decidedDamage = DeterminDamage ();
			Debug.Log ("I hit " + other.gameObject.name);
			Debug.LogFormat ("I did " + decidedDamage + " points of damage.");
			Instantiate (particleHitFeedback, this.transform.position, Quaternion.identity);
			GamePad.SetVibration (PlayerIndex.One, 0.5f, 0.5f);
			StartCoroutine (turnOffVibration ());
			other.gameObject.GetComponent<EnemyHealthBehavior> ().TakeDamage (decidedDamage);
			canHit = false;
		}
	}

	float DeterminDamage(){
		return Mathf.Round(Random.Range (damageToInflictMin, damageToInflictMax));
	}

	void ResetHit(bool hitThisSwipe){
		if (!hitThisSwipe && !attackMotion.hasSwiped) {
			canHit = true;
		}
		
	}

	IEnumerator turnOffVibration(){
		yield return new WaitForSeconds(0.1f);
		GamePad.SetVibration (PlayerIndex.One, 0, 0);
	}
}
