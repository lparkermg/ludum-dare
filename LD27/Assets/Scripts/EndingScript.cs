using UnityEngine;
using System.Collections;

public class EndingScript : MonoBehaviour {

	//public GameObject UDDHolder;
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.Space) == true)
		{
			PlayerPrefs.SetInt ("CompletedGame",1);
			Application.LoadLevel (0);
		}
	}
}
