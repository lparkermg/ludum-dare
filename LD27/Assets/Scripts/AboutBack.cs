using UnityEngine;
using System.Collections;

public class AboutBack : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.Space) == true)
		{
			Application.LoadLevel (0);	
		}
	}
}
