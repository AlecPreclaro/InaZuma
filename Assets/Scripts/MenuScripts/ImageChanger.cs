using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour {

	public Sprite[] images;
	public InazumaMenuController myController;

	private CursorIndexTracker myCursor;
	private Image currentImage;

	void Start () {
		myCursor = GetComponent<CursorIndexTracker> ();
		currentImage = GetComponent<Image> ();
	}

	void Update () {
		SwitchImagesWithCursor ();
	}

	void SwitchImagesWithCursor(){
		if (!myController.canMoveCursor) {
			//Debug.Log ("Switching images");
			currentImage.sprite = images [myCursor.currentCursorIndex];
		}
	}
}
