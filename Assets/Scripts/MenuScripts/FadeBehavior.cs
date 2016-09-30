using UnityEngine;
using System.Collections;

public class FadeBehavior : MonoBehaviour {

	public bool doesDestroyOnFade;
	public bool doesDeactivateOnFade;
	public bool doesFadeIn;
	public bool controlFadeOut;
	public bool doesFadeOut;
	public float displayTime = 3.0f;
	public float timeToFade = 1.0f;

	private CanvasGroup myUIGroup;
	private float rateOfChange;
	private float currentTime;
	private float beginAlpha;
	private float endAlpha;
	private bool fading;
	private bool fadingOut;

	void Start(){
		myUIGroup = GetComponent<CanvasGroup> ();
		rateOfChange = 0.0f;
		currentTime = 0.0f;
		beginAlpha = 0.0f;
		endAlpha = 1.0f;
		fading = false;
		fadingOut = false;

		if (myUIGroup == null) {
			Debug.Log ("No canvas group!");
			this.enabled = false;
		} else {
			if (doesFadeIn) {
				FadeIn ();
			}
			if (doesFadeOut) {
				StartCoroutine (BeginFadeOut ());
			}
		}
	}

	void Update(){
		if (doesFadeOut) {
			if (fadingOut) {
				FadeOut ();
			}
		}
	}

	public void FadeIn(){
		beginAlpha = 0.0f;
		endAlpha = 1.0f;
		fading = true;
		if (doesFadeOut) {
			StartCoroutine (FadingBehavior ());
		}
	}

	public void FadeOut(){
		beginAlpha = 1.0f;
		endAlpha = 0.0f;
		fading = true;
		fadingOut = true;
		StartCoroutine (FadingBehavior ());
	}

	IEnumerator BeginFadeOut (){
		yield return new WaitForSeconds (displayTime);
		if (!controlFadeOut) {
			FadeOut ();
		}
	}

	IEnumerator FadingBehavior(){
		currentTime = 0.0f;
		rateOfChange = (endAlpha - beginAlpha) / timeToFade;
		ChangeAlpha (beginAlpha);
		while (fading) {

			currentTime += Time.deltaTime;

			if (currentTime > timeToFade) {

				fading = false;
				ChangeAlpha (endAlpha);

				if (fadingOut) {
					if (doesDestroyOnFade) {
						Destroy (this.gameObject);
					}
					if (doesDeactivateOnFade) {
						this.gameObject.SetActive (false);
					}
				}

				yield break;

			} else {

				ChangeAlpha (myUIGroup.alpha + (rateOfChange * Time.deltaTime));
			}

			yield return null;
		}
	}

	public void ChangeAlpha (float alpha){
		myUIGroup.alpha = Mathf.Clamp (alpha, 0, 1);
	}
}
