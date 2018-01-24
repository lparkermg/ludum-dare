using UnityEngine;
using System.Collections;

public class BonusStart : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.Space) == true)
		{
			Application.LoadLevel (12);	
		}
	}
}
