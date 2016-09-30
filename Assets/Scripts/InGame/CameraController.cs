using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public float horizontalSpeed, verticalSpeed, cameraXSpeedBoost, cameraXBoostMin, cameraXBoostMid, cameraXBoostMax,
	cameraYSpeedBoost, cameraYBoostMin, cameraYBoostMid, cameraYBoostMax, cameraDistanceFromPlayer;
	public bool targetOn;
	public GameObject playerCameraOffSet, player, visualizePositions;


	private Rigidbody playerBody;
	private PlayerTargetSystem targetSystem;
	private float horizontalCamMovement, verticalCamMovement, thumbstickLeftHorz, thumbstickLeftVert;
	private Vector3 relativePosition, relativePositionRight, playerPosOffSet, cameraPositionOffSetX,
	cameraPositionOffSetY, cameraPositionOffset;

	void Start () {
		targetSystem = player.GetComponent<PlayerTargetSystem> ();
		playerBody = player.GetComponent<Rigidbody>();

		foreach (Transform playerChild in player.GetComponent<Transform>()) {
			if (playerChild.name == "player_camera_offset") {
				playerCameraOffSet = playerChild.gameObject;
			}
		}


	}

	void LateUpdate () {
		targetOn = targetSystem.targetOn;
		cameraDistanceFromPlayer = Vector3.Distance (playerCameraOffSet.transform.position, this.transform.position);
		//if (targetOn) {
			Targeting ();
	//	} else {
			//ThumbStickControls ();
		//}
	}

	void Targeting(){
		GameObject target = targetSystem.currentTarget ;
		//Vector3 behindPlayer = playerBody.position - (playerBody.transform.forward * cameraDistanceFromPlayer);
		Vector3 behindPlayer = playerCameraOffSet.transform.position - (player.transform.forward * cameraDistanceFromPlayer);
		transform.position = Vector3.Lerp (transform.position, behindPlayer, targetSystem.timeToTarget * Time.smoothDeltaTime);
	}



	void ThumbStickControls(){


		//Horizontal Control
		if (Mathf.Abs(Input.GetAxis ("RightJoystickHorizontal")) < 0.3f) {
			thumbstickLeftHorz = 0.0f;
		} else {
			thumbstickLeftHorz = Input.GetAxis ("RightJoystickHorizontal");
		}

		//Horizontal Boosts	
		if (Mathf.Abs(Input.GetAxis ("RightJoystickHorizontal")) < 0.4f)	{
			cameraXSpeedBoost = cameraXBoostMin;
		}
		else if (Mathf.Abs(Input.GetAxis ("RightJoystickHorizontal")) >= 0.4f  && Mathf.Abs(Input.GetAxis ("RightJoystickHorizontal")) < 0.9f){
			cameraXSpeedBoost = cameraXBoostMid;
		}
		else if (Mathf.Abs(Input.GetAxis ("RightJoystickHorizontal")) >= 0.9f){
			cameraXSpeedBoost = cameraXBoostMax;
		}

		//Vertical Control
		if (Mathf.Abs(Input.GetAxis ("RightJoystickVertical")) < 0.3f) {
			thumbstickLeftVert = 0.0f;
		} else {
			thumbstickLeftVert = Input.GetAxis ("RightJoystickVertical");
		}

		//Vertical Boosts
		if (Mathf.Abs(Input.GetAxis ("RightJoystickVertical")) < 0.4f)	{
			cameraYSpeedBoost = cameraYBoostMin;
		}
		else if (Mathf.Abs(Input.GetAxis ("RightJoystickVertical")) >= 0.4f && Mathf.Abs(Input.GetAxis ("RightJoystickVertical")) < 0.9f ){
			cameraYSpeedBoost = cameraYBoostMid;
		}
		else if (Mathf.Abs(Input.GetAxis ("RightJoystickVertical")) >= 0.9f){
			cameraYSpeedBoost = cameraYBoostMax;
		}

		horizontalCamMovement = thumbstickLeftHorz * horizontalSpeed * cameraXSpeedBoost * Time.deltaTime;
		verticalCamMovement = thumbstickLeftVert * verticalSpeed * cameraYSpeedBoost * Time.deltaTime;

		playerPosOffSet = playerCameraOffSet.transform.position;


		transform.LookAt (playerPosOffSet);



		relativePosition = player.transform.position - this.transform.position;
		relativePositionRight = Vector3.Cross (relativePosition, Vector3.up);




		transform.RotateAround (playerPosOffSet, Vector3.up, horizontalCamMovement);
		transform.RotateAround (playerPosOffSet, relativePositionRight.normalized, verticalCamMovement);
	}
}
