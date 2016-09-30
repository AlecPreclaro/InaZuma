using UnityEngine;
using System.Collections;

public class InazumaMenuController : MonoBehaviour {

	public GameObject mainMenu, mainMenuCursor;
	public float movementThreshold;
	public bool canMoveCursor;

	private GameObject currentMenu, currentCursor, sameCursorCheck;
	private int currentCursorIndex, currentCursorIndexMax;

	// Use this for initialization
	void Start () {
		canMoveCursor = true;
		sameCursorCheck = null;
		SetCurrentMenuAndCursor (mainMenu, mainMenuCursor);
	
	}
	
	// Update is called once per frame
	void Update () {
		SetCursorInfo ();
		currentCursor.GetComponent<CursorIndexTracker>().currentCursorIndex = VerticalCursorMovement (movementThreshold, currentCursor, currentCursor.GetComponent<CursorIndexTracker>().currentCursorIndex, currentCursorIndexMax);
	}

	int VerticalCursorMovement(float thumbStickThreshold, GameObject cursor, int cursorPositionIndex, int cursorPositionIndexMax){

		float[] cursorYPositions = cursor.GetComponent<CursorIndexTracker> ().cursorYPositions;
		Vector3 cursorPosition = new Vector3 (cursor.GetComponent<RectTransform> ().localPosition.x, cursor.GetComponent<RectTransform> ().localPosition.y, cursor.GetComponent<RectTransform> ().localPosition.z);

		//If the thumbstick has been pushed far enough
		if ((Mathf.Abs (Input.GetAxis ("LeftJoystickVertical")) >= thumbStickThreshold && canMoveCursor) || (Input.GetButtonDown("WKey") || Input.GetButtonDown("SKey")) ){
			canMoveCursor = false;
			//If the thumbstick has been pushed up and is not at the top of the menu
			//Move the cursor up
			if (Input.GetAxis ("LeftJoystickVertical") > 0 || Input.GetButtonDown("WKey")) {
			//	Debug.Log ("I moved up");
				if (cursorPositionIndex == 0) {
					cursorPositionIndex = cursorPositionIndexMax;
				} else {

					cursorPositionIndex--;
			//		Debug.Log("CursorPositionIndex is " + cursorPositionIndex);
				}
				if (cursorYPositions.Length > 0) {
					cursor.GetComponent<RectTransform> ().localPosition = new Vector2 (cursorPosition.x, cursorYPositions [cursorPositionIndex]);
				}
			//	Debug.Log("CursorPositionIndex is " + cursorPositionIndex);
				return cursorPositionIndex;

			}
			//If the thumbstick has been pushed down and is not at the bottom of the menu
			//Move the cursor down
			else if (Input.GetAxis ("LeftJoystickVertical") < 0 || Input.GetButtonDown("SKey")) {
			//	Debug.Log ("I moved down");
				if (cursorPositionIndex == cursorPositionIndexMax) {
					cursorPositionIndex = 0;
				} else {
					cursorPositionIndex++;
				//	Debug.Log("CursorPositionIndex is " + cursorPositionIndex);
				}
				if (cursorYPositions.Length > 0) {
					cursor.GetComponent<RectTransform> ().localPosition = new Vector2 (cursorPosition.x, cursorYPositions [cursorPositionIndex]);
				}
				return cursorPositionIndex;
				//Debug.Log("CursorPositionIndex is " + cursorPositionIndex);
			} 
			//No movement done (must return something)
			else {
			//	Debug.Log("CursorPositionIndex is " + cursorPositionIndex);
				return cursorPositionIndex;
			}
		} 
		//Thumbstick lower than threshold
		else if ((Mathf.Abs(Input.GetAxis ("LeftJoystickVertical")) <= thumbStickThreshold/2) || Input.GetButtonUp("WKey") || Input.GetButtonUp("SKey")) {
			canMoveCursor = true;
			return cursorPositionIndex;
		} 
		//No movement done (must return something)
		else {
			//Debug.Log("CursorPositionIndex is " + cursorPositionIndex);
			return cursorPositionIndex;
		}
	}

	void  SetCursorInfo(){
		if (currentCursor != sameCursorCheck){
		//	Debug.Log ("CursorInfoSet!");
		CursorIndexTracker myCursorTracker = currentCursor.GetComponent<CursorIndexTracker> ();
		currentCursorIndexMax = myCursorTracker.cursorIndexMax;
		currentCursorIndex = myCursorTracker.currentCursorIndex;
		sameCursorCheck = currentCursor;
		}

	}

	public void SetCurrentMenuAndCursor(GameObject menu, GameObject cursor){
		currentMenu = menu;
		currentCursor = cursor;
	}

}
