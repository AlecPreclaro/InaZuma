using UnityEngine;
using System.Collections;

public class PlayerSpaceMovement : MonoBehaviour {

	public float speed, leftThumbstickVert, leftThumbstickHorz, boostedSpeed, 
	originalSpeed, driftReductionRate, forwardBackwardDirection, rightLeftDirection, boostDirectionFB,
	boostDirectionRL, driftLargestMagnitude, driftMultiplier, rightThumbstickVert, rightThumbstickHorz,
	vertRotationSpeed, horzRotationSpeed, rotationMinBoost, rotationMidBoost, rotationMaxBoost;

	public float speedBoostMultiplier = 700f;

	public bool autoMove, boosting, slideOn, leftTriggerBoost, rightTriggerBoost;
	public GameObject playerDirectionPosObj, cameraObj;

	private Rigidbody playerBody;
	private Vector3 frontBackMotion, leftRightMotion, largestFrontBack, largestLeftRight, driftCombinedVector;

	void Start(){
		QualitySettings.vSyncCount = 0;
		playerBody = GetComponent<Rigidbody> ();
		driftLargestMagnitude = 0.0f;
		forwardBackwardDirection = 0.0f;
		rightLeftDirection = 0.0f;
		driftCombinedVector = Vector3.zero;
		largestFrontBack = Vector3.zero;
		largestLeftRight = Vector3.zero;
		originalSpeed = speed;
		boostedSpeed = speed * speedBoostMultiplier;
		autoMove = false;
		boosting = false;
	}

	void Update(){
		ThumbStickControl ();

		Boost ();
		VerticalMovement ();

		if (!autoMove && !boosting) {
			frontBackMotion = cameraObj.transform.forward.normalized * leftThumbstickVert * speed * Time.deltaTime;
			leftRightMotion = cameraObj.transform.right.normalized * leftThumbstickHorz * speed * Time.deltaTime;
			transform.position += frontBackMotion;
			transform.position += leftRightMotion;
		} else {
			
			frontBackMotion = cameraObj.transform.forward.normalized * boostDirectionFB * speed * Time.deltaTime;
			leftRightMotion = cameraObj.transform.right.normalized * boostDirectionRL * speed * Time.deltaTime;
			transform.position += frontBackMotion;
			transform.position += leftRightMotion;
		}
		PlayerRotation ();
		Drift (frontBackMotion, leftRightMotion);
		

	}

	void ThumbStickControl()
	{
		//Left Thumbsitcks
		if (Input.GetJoystickNames ().Length != 0) {
			if (Mathf.Abs (Input.GetAxis ("LeftJoystickHorizontal")) > 0.3f) {
				leftThumbstickHorz = Input.GetAxis ("LeftJoystickHorizontal");
				if (!boosting) {
					autoMove = false;
				}
			} else {
				leftThumbstickHorz = 0.0f;
			}

			if (Mathf.Abs (Input.GetAxis ("LeftJoystickVertical")) > 0.3f) {
				leftThumbstickVert = Input.GetAxis ("LeftJoystickVertical");
				if (!boosting) {
					autoMove = false;
				}
			} else {
				leftThumbstickVert = 0.0f;
			}

			//Right Thumbsticks
			if (Mathf.Abs (Input.GetAxis ("RightJoystickHorizontal")) > 0.3f) {
				rightThumbstickHorz = Input.GetAxis ("RightJoystickHorizontal");
			} else {
				rightThumbstickHorz = 0.0f;
			}

			if (Mathf.Abs (Input.GetAxis ("RightJoystickVertical")) > 0.3f) {
				rightThumbstickVert = Input.GetAxis ("RightJoystickVertical");
			} else {
				rightThumbstickVert = 0.0f;
			}
		//Keyboard Controls
		} else {
		//Moving Character
			//Going Forwards
			if (Input.GetButton ("WKey")) {
				leftThumbstickVert = 1.0f;
			} else if (Input.GetButtonUp ("WKey") && !Input.GetButton("SKey")) {
				leftThumbstickVert = 0.0f;
			}
			//Going Backwards
			if (Input.GetButton ("SKey")) {
				leftThumbstickVert = -1.0f;
			} else if (Input.GetButtonUp ("SKey") && !Input.GetButton ("WKey")) {
				leftThumbstickVert = 0.0f;
			}
			//Going Right
			if (Input.GetButton ("DKey")) {
				leftThumbstickHorz = 1.0f;
			} else if (Input.GetButtonUp("DKey") && !Input.GetButton ("AKey")) {
				leftThumbstickHorz = 0.0f;
			}
			//Going Left
			if(Input.GetButton ("AKey")){
				leftThumbstickHorz = -1.0f;
			} else if (Input.GetButtonUp("AKey") && !Input.GetButton("DKey")){
				leftThumbstickHorz = 0.0f;
			}
		//Turning Character
			rightThumbstickHorz = Input.GetAxis ("MouseX");
			rightThumbstickVert = Input.GetAxis ("MouseY");

		}


	}

	void Boost(){
			leftTriggerBoost = false;
		if (Input.GetAxis ("RightTrigger") < 0.05f || Input.GetAxis ("LeftTrigger") < 0.05f || Input.GetButtonUp("LeftShift")) {
				if (Mathf.Abs (leftThumbstickVert) >= 0.2f) {
					forwardBackwardDirection = leftThumbstickVert;
				} else if (Mathf.Abs (leftThumbstickVert) < 0.2f && Mathf.Abs (leftThumbstickHorz) < 0.2f) {
					forwardBackwardDirection = 1.0f;
				} else if (Mathf.Abs (leftThumbstickVert) < 0.2f && Mathf.Abs (leftThumbstickHorz) > 0.2f) {
					forwardBackwardDirection = 0.0f;
				}
				rightLeftDirection = leftThumbstickHorz;
			}
		if (Input.GetAxis ("RightTrigger") > 0.05f || Input.GetAxis ("LeftTrigger") > 0.05f || Input.GetButton("LeftShift")) {
				autoMove = true;
				boosting = true;
				speed = boostedSpeed * Time.deltaTime;
				boostDirectionFB = forwardBackwardDirection;
				boostDirectionRL = rightLeftDirection;

			} else {
				autoMove = false;
				boosting = false;
				speed = originalSpeed;
			}
	}

	void VerticalMovement(){
		float mouseScrollBoost = 5f;
		if (Input.GetButton ("RightBumper") || Input.GetAxis("MouseWheel") > 0) {
			transform.position += transform.up * speed * mouseScrollBoost * Time.deltaTime;
		}

		if (Input.GetButton ("LeftBumper") || Input.GetAxis("MouseWheel") < 0) {
			transform.position += -transform.up * speed * mouseScrollBoost * Time.deltaTime;
		}
	}

	void PlayerRotation(){
		//Setting horizontal rotation speed
		if (Mathf.Abs(rightThumbstickHorz) > 0.3f && Mathf.Abs(rightThumbstickHorz) <= 0.6f) {
			horzRotationSpeed = rotationMinBoost;
		}
		else if (Mathf.Abs(rightThumbstickHorz) > 0.6f && Mathf.Abs(rightThumbstickHorz) <= 0.9f){
			horzRotationSpeed = rotationMidBoost;
		}
		else if (Mathf.Abs(rightThumbstickHorz) > 0.9f)
		{
			horzRotationSpeed = rotationMaxBoost;
		}

		//Setting vertical rotation speed
		if (Mathf.Abs(rightThumbstickVert) > 0.3f && Mathf.Abs(rightThumbstickVert) <= 0.6f) {
			vertRotationSpeed = rotationMinBoost;
		}
		else if (Mathf.Abs(rightThumbstickVert) > 0.6f && Mathf.Abs(rightThumbstickVert) <= 0.9f){
			vertRotationSpeed = rotationMidBoost;
		}
		else if (Mathf.Abs(rightThumbstickVert) > 0.9f)
		{
			vertRotationSpeed = rotationMaxBoost;
		}

		horzRotationSpeed = horzRotationSpeed * rightThumbstickHorz * Time.deltaTime;
		vertRotationSpeed = vertRotationSpeed * rightThumbstickVert * Time.deltaTime;

		transform.eulerAngles += new Vector3 (vertRotationSpeed, horzRotationSpeed, 0.0f);
	}

	void Drift(Vector3 frontBackVector, Vector3 leftRightVector){

		Vector3 depthDirection, horzDirection, combinedDirection, minimumDriftSpeed;

		minimumDriftSpeed = cameraObj.transform.forward.normalized * 0.3f * speed * Time.deltaTime;

		if ((Mathf.Abs (leftThumbstickHorz) > 0.3f) || (Mathf.Abs (leftThumbstickVert) > 0.3f)) {
			//Calculating Direction
			//May need to normalize the following
			depthDirection = (transform.position - frontBackVector);
			horzDirection = (transform.position - leftRightVector);
			combinedDirection = (depthDirection - horzDirection).normalized;


			//Calculating Magnitude
			if (Mathf.Abs (frontBackVector.magnitude) > Mathf.Abs (minimumDriftSpeed.magnitude)) {
				largestFrontBack = frontBackVector;
			}

		}

	}

	/*
	void Drift(Vector3 frontBackVector, Vector3 leftRightVector)
	{

		Vector3 minimumDriftSpeed, frontBackDir, leftRightDir;

		minimumDriftSpeed = cameraObj.transform.forward.normalized * 0.3f * speed * Time.deltaTime;

		//If the incoming magnitude meets the minimum size requirement, or if thedirection has changed
		//set largest front back to the incoming frowardbackward vector

		if (Mathf.Abs (frontBackVector.magnitude) > Mathf.Abs(minimumDriftSpeed.magnitude) || (Mathf.Sign(frontBackVector.normalized.magnitude) != Mathf.Sign(largestFrontBack.normalized.magnitude))) {
			largestFrontBack = frontBackVector;
		}

		//If the incoming magnitude meets the minimum size requirement, or if thedirection has changed
		//set largest left right to the incoming leftRight vector

		if (Mathf.Abs (leftRightVector.magnitude) > Mathf.Abs(minimumDriftSpeed.magnitude) || (Mathf.Sign(leftRightVector.normalized.magnitude) != Mathf.Sign(largestLeftRight.normalized.magnitude))) {
			largestLeftRight = leftRightVector;
		}

		if (Mathf.Abs (largestFrontBack.magnitude) >= Mathf.Abs (largestLeftRight.magnitude)) {
			driftLargestMagnitude = largestFrontBack.magnitude;
		} else if (Mathf.Abs (largestFrontBack.magnitude) < Mathf.Abs (largestLeftRight.magnitude)) {
			driftLargestMagnitude = largestLeftRight.magnitude;
		}

		if (Mathf.Abs (Input.GetAxis ("LeftJoystickVertical")) > 0.3f && Mathf.Abs (Input.GetAxis ("LeftJoystickHorizontal")) > 0.3f) {
			driftCombinedVector = (largestFrontBack + largestLeftRight).normalized * driftLargestMagnitude * driftMultiplier * Time.deltaTime;
		}

		if (Mathf.Abs (Input.GetAxis ("LeftJoystickVertical")) < 0.3f && Mathf.Abs (Input.GetAxis ("LeftJoystickHorizontal")) < 0.3f) {

			driftCombinedVector = driftCombinedVector / driftReductionRate;



			transform.position += driftCombinedVector;


		}
	 * */



}
