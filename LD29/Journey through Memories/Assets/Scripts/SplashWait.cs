using UnityEngine;
using System.Collections;

public class SplashWait : MonoBehaviour {

	public float waitTime = 2.0f;
	public float currentTime = 0.0f;

	public int nextSceneNumber = 0;

	// Update is called once per frame
	void Update () {
		if(waitTime < currentTime){
			Application.LoadLevel (nextSceneNumber);
		}
		currentTime = currentTime + Time.deltaTime;
	}
}
