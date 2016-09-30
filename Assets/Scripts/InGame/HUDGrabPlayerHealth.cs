using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDGrabPlayerHealth : MonoBehaviour {

	public PlayerHealthBehavior playerHpCmpnt;
	private string healthDisplayString;
	private Text myText;

	// Use this for initialization
	void Start () {
		myText = GetComponent<Text> ();
		healthDisplayString = "Health:" + playerHpCmpnt.playerHealth.ToString ();
		myText.text	= healthDisplayString;
	}
	
	// Update is called once per frame
	void Update () {
		if (myText.text != "Health:" + playerHpCmpnt.playerHealth.ToString()) {
			myText.text	= "Health:" + playerHpCmpnt.playerHealth.ToString();
		}
	}
}
