using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDGrabTargetHealth : MonoBehaviour {

	public PlayerTargetSystem targetSystem;
	public GameObject targetTextBackground;

	private GameObject currentTarget;
	private string targetName;
	private float targetHP;
	private Text enemyHPText;
	private string enemyHealthDisplayString;

	void Start () {
		targetHP = 0;
		targetName = "No Target";
		enemyHPText = GetComponent<Text> ();
		enemyHealthDisplayString = "Target: " + targetName + "\n" + "Health:" + targetHP.ToString ();
	}

	void Update () {
		GetTargetHPAndName ();
		if (enemyHPText.text != "Target: " + targetName + "\n" + "Health:" + targetHP.ToString ()) {
			enemyHPText.text = "Target: " + targetName + "\n" + "Health:" + targetHP.ToString ();
		}
	}

	void GetTargetHPAndName(){
		if (targetSystem.currentTarget != null && targetSystem.targetOn) {
			currentTarget = targetSystem.currentTarget;
			targetHP = currentTarget.GetComponent<EnemyHealthBehavior> ().health;
			targetName = currentTarget.gameObject.name;
			GetComponent<CanvasRenderer> ().SetAlpha (1);
			targetTextBackground.GetComponent<CanvasRenderer> ().SetAlpha (1);
		} else if (!targetSystem.targetOn) {
			GetComponent<CanvasRenderer> ().SetAlpha (0);
			targetTextBackground.GetComponent<CanvasRenderer> ().SetAlpha (0);
		}
	}
}
