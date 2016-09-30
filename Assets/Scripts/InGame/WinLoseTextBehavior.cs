using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class WinLoseTextBehavior : MonoBehaviour {

	public CarrierManager largeShipManager;
	public PlayerHealthBehavior playerAlive;
	public GameObject winLoseText;
	public float endGameWait;
	public bool endOfGame;
	public bool playerWin;

	// Use this for initialization
	void Start () {
		endOfGame = false;
	}
	
	// Update is called once per frame
	void Update () {
		CheckForWinOrLose ();
	}

	void CheckForWinOrLose(){
		if (!playerAlive.isAlive && !endOfGame) {
			endOfGame = true;
			playerWin = false;
			winLoseText.SetActive (true);
			winLoseText.GetComponent<Text> ().color = Color.red;
			winLoseText.GetComponent<Text>().text = "You Lose";
			StartCoroutine (DelayBeforeEndOfGame (endGameWait));
		}
		//if all large ships are destroyed
		if (largeShipManager.carrierList.Count == 0 && !endOfGame){
			endOfGame = true;
			playerWin = true;
			winLoseText.SetActive (true);
			winLoseText.GetComponent<Text> ().color = Color.green;
			winLoseText.GetComponent<Text>().text = "You Win!";
			StartCoroutine (DelayBeforeEndOfGame (endGameWait));
		}
	}

	void OnApplicationQuit(){
		GamePad.SetVibration (PlayerIndex.One, 0, 0);
	}

	IEnumerator DelayBeforeEndOfGame(float howLongToDelay){
		yield return new WaitForSeconds (howLongToDelay);
		GamePad.SetVibration (PlayerIndex.One, 0, 0);
		SceneManager.LoadScene ("endMenuScene");
	}
}
