using UnityEngine;
using System.Collections;

public class grabCameraCurrentRotation : MonoBehaviour {

	public float offSet;
	public GameObject cameraObj;
	public Vector3 cameraRotation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		cameraRotation = cameraObj.transform.eulerAngles;
		transform.position = new Vector3 (cameraObj.transform.position.x, cameraObj.transform.position.y + offSet, cameraObj.transform.position.z);
		transform.eulerAngles = cameraRotation;
	}
}
