using UnityEngine;
using System.Collections;

public class PlayerDirection : MonoBehaviour {

	public Transform playerPos;
	public float xMaxDistance, yMaxDistance, ZMinDistFromPLayer, adjustedX, adjustedY, adjustedXSpeed, adjustedYSpeed, zPosOut, distanceX, distanceY, distanceZ, returnSpeed;

	private float zAdjustment, rotateDegX, rotateDegY, thumbStickX, thumbStickY;
	private Vector3 relativePos, relativePosRight;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetJoystickNames ().Length != 0) {
			thumbStickX = Input.GetAxis ("LeftJoystickHorizontal");
			thumbStickY = Input.GetAxis ("LeftJoystickVertical");
		} else {
			thumbStickX = Input.GetAxis ("MouseX");
			thumbStickY = Input.GetAxis ("MouseY");
		}

		if (Mathf.Abs(thumbStickX) < 0.5) 
		{
			adjustedX = 0f;
		} 
		else 
		{
			adjustedX = thumbStickX * adjustedXSpeed * Time.deltaTime;
		}

		if (Mathf.Abs(thumbStickY) < 0.5) 
		{
			adjustedY = 0f;
		} 
		else 
		{
			adjustedY = thumbStickY * adjustedYSpeed * Time.deltaTime;
		}

		relativePos = playerPos.localPosition - transform.localPosition;
		relativePosRight = Vector3.Cross (relativePos, Vector3.up);

		transform.RotateAround (playerPos.position, Vector3.up, adjustedX);
		transform.RotateAround (playerPos.position, relativePosRight.normalized, adjustedY);

	}
}
