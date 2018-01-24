using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour {

	//Angle stuff
	public float currentCount = 0.0f;

	public Quaternion newRotation;
	public Quaternion currentRotation;

	public float rotationSpeed = 0.5f;

	public bool isPaused = false;
	private int pauseInt = 0;

	// Use this for initialization
	void Start () {
		currentRotation = transform.rotation;
		newRotation = currentRotation;
		currentCount = currentRotation.y;
		PlayerPrefs.SetInt ("IsPaused",1);
		PlayerPrefs.SetFloat ("CurrentPoints",0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		CheckPause ();
		/*if(Input.GetKeyDown (KeyCode.C) == true){
			Application.CaptureScreenshot ("screenFromUnity_" + Time.time + ".png");
		}*/
		if(Input.GetKey (KeyCode.D) == true && Input.GetKey (KeyCode.A) == false && isPaused == false){
			currentCount = currentCount + rotationSpeed;
		}
		else if(Input.GetKey (KeyCode.D) == false && Input.GetKey (KeyCode.A) == true && isPaused == false){
			currentCount = currentCount - rotationSpeed;
		}
		else if(Input.GetKeyDown(KeyCode.S) == true && Input.GetKey (KeyCode.D) == false && Input.GetKey (KeyCode.A) == false && isPaused == false)
		{
			currentCount = 0.0f;
		}
		if(Input.GetKeyDown (KeyCode.P) == true && isPaused == false){
			PlayerPrefs.SetInt ("IsPaused",1);
		}
		else if(Input.GetKeyDown (KeyCode.P) == true && isPaused == true){
			PlayerPrefs.SetInt ("IsPaused",0);
		}
		if(currentCount > 1.00f){
			currentCount = 1.00f;
		}
		else if(currentCount < -1.0f){
			currentCount = -1.0f;
		}
		MoveThePlayer (currentCount)
;
	}

	void MoveThePlayer(float currentAngle){
		//Rotate the player
		if(isPaused == false){
			transform.Rotate (0,currentAngle,0,Space.World);
		}
		else if(isPaused == true){
			currentCount = 0.0f;
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
