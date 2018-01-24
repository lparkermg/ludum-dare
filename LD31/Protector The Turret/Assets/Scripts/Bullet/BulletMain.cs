using UnityEngine;
using System.Collections;

public class BulletMain : MonoBehaviour {

	//Variables
	public float bulletSpeed = 2.0f;

	public bool isPaused = false;
	private int pauseInt = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		CheckPause ();
		if(isPaused == false){
			rigidbody.AddForce (transform.forward * bulletSpeed * 100.0f * Time.deltaTime,ForceMode.Force);
		}
		else if(isPaused == true){
			rigidbody.velocity = Vector3.zero;
		}
	}

	void CheckPause(){
		pauseInt = PlayerPrefs.GetInt ("IsPaused");
		if(pauseInt == 0){
			isPaused = false;
		}
		else if(pauseInt == 1){
			isPaused = true;
		}
	}
}
