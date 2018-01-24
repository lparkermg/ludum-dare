using UnityEngine;
using System.Collections;

public class TwoSecondTimer : MonoBehaviour {

	private float WaitMax = 2.0f;
	private float WaitCurrent = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(WaitMax < WaitCurrent){
			Application.LoadLevel(1);
		}
		WaitCurrent = WaitCurrent + Time.deltaTime;
	}
}
