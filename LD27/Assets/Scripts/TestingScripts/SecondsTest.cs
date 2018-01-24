using UnityEngine;
using System.Collections;

public class SecondsTest : MonoBehaviour {
	
	private int SecondsInt;
	private float waitTime;
	// Update is called once per frame
	void Update () {
		waitTime = waitTime + Time.deltaTime;
		//Debug.Log (waitTime);
		if (waitTime > 1.0f)
		{
			SecondsInt++;
			waitTime = 0.0f;
			Debug.Log ("Seconds Passed = " + SecondsInt);
		}
	}
}
