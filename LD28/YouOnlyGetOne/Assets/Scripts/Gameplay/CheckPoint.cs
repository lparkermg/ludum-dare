using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	private bool CheckPassed = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider ObjCol){
		if(ObjCol.tag == "Player" && CheckPassed == false){
			particleSystem.startColor = Color.green;
			RenderSettings.fogDensity = RenderSettings.fogDensity + 0.01f;
		}
	}
}
