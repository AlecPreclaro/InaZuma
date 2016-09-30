using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerTargetSystem : MonoBehaviour {

	public GameObject visualizePositions;
	public GameObject potentialTarget, currentTarget;
	public List<GameObject> targetsOnScreen = new List<GameObject>();
	public Texture redTargetRect;
	static public float targetingGUIScale;
	public float minGUISize, maxGUISize, targetMaxDistance, timeToTarget;
	public bool checkForTargets, targetOn, facingTarget;

	private Rigidbody playerBody;
	private Vector3 unlockedRotation;
	private Rect targetingRect;
	private Vector2 centerOfScreen, targetClosestPos;
	public float potentialTargetDistance, targetDistance;

	void Start(){
		playerBody = GetComponent<Rigidbody> ();
		facingTarget = false;
		targetOn = false;
		targetingGUIScale = 10.0f;
		centerOfScreen = new Vector2 (Screen.width / 2, Screen.height / 2);
	}

	void Update (){
		if (currentTarget != null) {
			targetDistance = Vector3.Distance (transform.position, currentTarget.transform.position);
		} else {
			targetDistance = 0.0f;
		}
		if (Input.GetButtonDown ("RightJoystickButton") || Input.GetButtonDown ("BButton")) {
			checkForTargets = true;
			targetOn = !targetOn;
			if (targetsOnScreen.Count == 0 && currentTarget == null) {
				targetOn = false;
			}
			DetermineTargetNearestCenter ();
			if (targetOn == false) {
				currentTarget = null;
			}
		}

		if (targetDistance > targetMaxDistance) {
			targetOn = false;
			currentTarget = null;
		}

		if (targetOn && currentTarget != null) {
			if (!facingTarget) {
				Vector3 diffTargetPlayer = (currentTarget.transform.position - transform.position) / 2;
				Quaternion targetedRotation = Quaternion.LookRotation (diffTargetPlayer);
				transform.rotation = Quaternion.Lerp (transform.rotation, targetedRotation, timeToTarget * Time.smoothDeltaTime);
			}

		} else {
			facingTarget = false;
			unlockedRotation = transform.eulerAngles;
			transform.eulerAngles = unlockedRotation;
		}

	}

	void OnGUI(){

		if (currentTarget != null && currentTarget.GetComponent<TargetableBehavior>().isVisible) {
			Rect currentGUIRect = GUIRectWithObject (currentTarget);
			float difference;

			//If X or Y size of GUI is too small
			if (currentGUIRect.size.x < minGUISize) {
				difference = minGUISize - currentGUIRect.size.x;
				currentGUIRect.size = new Vector2 (currentGUIRect.size.x + difference, currentGUIRect.size.y + difference);
			}
			//If Y size of GUI is too small
			if (currentGUIRect.size.y < minGUISize) {
				difference = minGUISize - currentGUIRect.size.y;
				currentGUIRect.size = new Vector2 (currentGUIRect.size.x, currentGUIRect.size.y + difference);
			}
			//If X size of GUI is too big
			if (currentGUIRect.size.x > maxGUISize) {
				difference = currentGUIRect.size.x - maxGUISize;
				currentGUIRect.size = new Vector2 (currentGUIRect.size.x - difference, currentGUIRect.size.y);
			}
			//If Y size of GUI is too big
			if (currentGUIRect.size.y > maxGUISize) {
				difference = currentGUIRect.size.y - maxGUISize;
				currentGUIRect.size = new Vector2 (currentGUIRect.size.x, currentGUIRect.size.y - difference);
			}

			GUI.DrawTexture (currentGUIRect, redTargetRect);
		}
	}

	public static Rect GUIRectWithObject(GameObject go)
	{
		Vector3 cen = go.GetComponent<Renderer>().bounds.center;
		Vector3 ext = go.GetComponent<Renderer>().bounds.extents;


		Vector2[] extentPoints = new Vector2[8]
		{
			WorldToGUIPoint(new Vector3(cen.x - targetingGUIScale, cen.y - targetingGUIScale, cen.z - targetingGUIScale)),
			WorldToGUIPoint(new Vector3(cen.x + targetingGUIScale, cen.y - targetingGUIScale, cen.z - targetingGUIScale)),
			WorldToGUIPoint(new Vector3(cen.x - targetingGUIScale, cen.y - targetingGUIScale, cen.z + targetingGUIScale)),
			WorldToGUIPoint(new Vector3(cen.x + targetingGUIScale, cen.y - targetingGUIScale, cen.z + targetingGUIScale)),
			WorldToGUIPoint(new Vector3(cen.x - targetingGUIScale, cen.y + targetingGUIScale, cen.z - targetingGUIScale)),
			WorldToGUIPoint(new Vector3(cen.x + targetingGUIScale, cen.y + targetingGUIScale, cen.z - targetingGUIScale)),
			WorldToGUIPoint(new Vector3(cen.x - targetingGUIScale, cen.y + targetingGUIScale, cen.z + targetingGUIScale)),
			WorldToGUIPoint(new Vector3(cen.x + targetingGUIScale, cen.y + targetingGUIScale, cen.z + targetingGUIScale))
		};
		Vector2 min = extentPoints[0];
		Vector2 max = extentPoints[0];
		foreach (Vector2 v in extentPoints)
		{
			min = Vector2.Min(min, v);
			max = Vector2.Max(max, v);
		}

		return new Rect(min.x, min.y, max.x - min.x, max.y - min.y);
	}

	public static Vector2 WorldToGUIPoint(Vector3 world)
	{
		Vector2 screenPoint = Camera.main.WorldToScreenPoint(world);
		screenPoint.y = (float) Screen.height - screenPoint.y;
		return screenPoint;
	}

	public void DetermineTargetNearestCenter(){
		if (checkForTargets && targetOn == true){

			float closestDistanceToPlayer = 0.0f;

			for (int i = 0; i < targetsOnScreen.Count; i++) {

				Vector2 potentialTargetPos = WorldToGUIPoint (targetsOnScreen [i].transform.position);
				float potentialTargetDistance = Vector3.Distance (transform.position,  targetsOnScreen [i].transform.position);

				if (i == 0) {
					
					potentialTarget = targetsOnScreen [0];
					potentialTargetDistance = Vector3.Distance (transform.position, potentialTarget.transform.position);
					targetClosestPos = WorldToGUIPoint (targetsOnScreen [i].transform.position);
					closestDistanceToPlayer = potentialTargetDistance;
				}
				if (i != 0 && Vector2.Distance (potentialTargetPos, centerOfScreen) < Vector2.Distance (targetClosestPos, centerOfScreen)) {
					targetClosestPos = potentialTargetPos;
					potentialTarget = targetsOnScreen [i];
				}
			}
			currentTarget = potentialTarget;
			checkForTargets = false;
		}
	}

	//Used in player weapon scripts to check if the target has died
	public void TargetDied(){
		Debug.Log ("target dead");
		potentialTarget = null;
		currentTarget = null;
		targetOn = false;
	}

}
	