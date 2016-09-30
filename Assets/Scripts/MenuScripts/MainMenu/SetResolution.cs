using UnityEngine;
using System.Collections;

public class SetResolution : MonoBehaviour {

	void Awake () {
		Screen.SetResolution (1920, 1080, true);
		QualitySettings.SetQualityLevel (3, false);
	}
}
