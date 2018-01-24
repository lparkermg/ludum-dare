using UnityEngine;
using System.Collections;

public class SpaceBegin : MonoBehaviour {

	public int nextScene = 0;
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.Space) == true){
			Application.LoadLevel(nextScene);
		}
	}
}
