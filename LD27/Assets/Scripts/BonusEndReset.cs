using UnityEngine;
using System.Collections;

public class BonusEndReset : MonoBehaviour {

	// Update is called once per frame
	void Update () {
	 	if(Input.GetKeyDown (KeyCode.Space) == true)
		{
			PlayerPrefs.SetInt ("GravCapsLeftInt",3);
			PlayerPrefs.SetInt ("CompletedGame", 0);
			Application.LoadLevel (0);
		}
	}
}
