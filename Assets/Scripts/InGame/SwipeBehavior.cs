using UnityEngine;
using System.Collections;

public class SwipeBehavior : MonoBehaviour {

	public GameObject player;
	public float swipeSpeed, returnSpeed, timeLengthOfSwipe, endOfReturnTime, lengthIncrease, forwardMovement;
	public bool hasSwiped, isSwiping;

	private Collider swipeCollider;
	private float startReturnNeutral, endReturnNeutral;
	private Quaternion originalRot;
	private Vector3 originalPos, originalScale;
	private bool swipe;

	// Use this for initialization
	void Start () {
		swipeCollider = GetComponent<Collider> ();
		swipeCollider.enabled = false;
		hasSwiped = false;
		isSwiping = false;
		originalScale = transform.localScale;
		originalPos = transform.localPosition;
		originalRot = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
		//if (!isSwiping && !hasSwiped) {

		//}

		if (hasSwiped) {
			ReturnToNeutral ();
		}

		if (Input.GetButtonDown ("AButton") && !hasSwiped) {
			transform.localScale = originalScale;
			transform.localPosition = originalPos;
			transform.localRotation = originalRot;
			swipe = true;
		}

		if (Input.GetButtonUp ("AButton") && !isSwiping) {
			transform.localScale = originalScale;
			transform.localPosition = originalPos;
			transform.localRotation = originalRot;
			hasSwiped = false;
		}

		if (swipe) {
			SwipeMovement ();
		}

		if (transform.localScale == originalScale && transform.localPosition == originalPos && transform.localRotation == originalRot) {
			GetComponent<MeshRenderer> ().enabled = false;
		} else {
			GetComponent<MeshRenderer> ().enabled = true;
		}
	}

	void SwipeMovement(){
		isSwiping = true;
		swipeCollider.enabled = true;
		transform.position += player.transform.forward.normalized * forwardMovement * Time.deltaTime;
	//	transform.position += player.transform.up.normalized * (forwardMovement+25) * Time.deltaTime;
		transform.RotateAround (player.transform.position, player.transform.right, swipeSpeed * Time.deltaTime);
		transform.RotateAround (player.transform.position, -player.transform.up, swipeSpeed * Time.deltaTime);
		transform.localScale += new Vector3 (0.0f, lengthIncrease * Time.deltaTime, 0.0f);
		StartCoroutine (howLongSwipe (timeLengthOfSwipe));
	}

	void ReturnToNeutral(){
		startReturnNeutral += Time.deltaTime;
		transform.localPosition = Vector3.Lerp (transform.localPosition, originalPos, returnSpeed * Time.deltaTime);
		transform.localRotation = Quaternion.Lerp (transform.localRotation, originalRot, returnSpeed * Time.deltaTime);
		transform.localScale = Vector3.Lerp (transform.localScale, originalScale, returnSpeed * Time.deltaTime);
		if (startReturnNeutral > endOfReturnTime) {
			hasSwiped = false;
		}
	}
		

	IEnumerator howLongSwipe(float timeLengthToSwipe){
		yield return new WaitForSeconds (timeLengthToSwipe);
		swipeCollider.enabled = false;
		swipe = false;
		isSwiping = false;
		hasSwiped = true;
		startReturnNeutral = 0.0f;
	}
}
