using UnityEngine;
using System.Collections;

public class SwipeEmitParticles : MonoBehaviour {

	public float timeTilDeath = 1.5f;

	// Use this for initialization
	void Start () {
		StartCoroutine (DestroyAfterTime ());
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent <ParticleSystem> () != null) {
			if (!GetComponent<ParticleSystem> ().isPlaying) {
				Destroy (this.gameObject);
			}
		}
	}

	IEnumerator DestroyAfterTime(){
		yield return new WaitForSeconds (timeTilDeath);
		Destroy (this.gameObject);
	}
}
