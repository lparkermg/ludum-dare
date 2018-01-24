using UnityEngine;
using System.Collections;

public class BulletFire : MonoBehaviour {

	public GameObject fireBullet;
	public GameObject waterBullet;
	public GameObject airBullet;
	public GameObject earthBullet;

	public int nextBullet = 0;

	public GameObject spawnPoint1;
	public GameObject spawnPoint2;
	public int currentSpawnPoint;

	public bool needToWait = false;
	public float waitTime = 0.5f;
	public float currentTime = 0.0f;

	public bool isPaused = false;
	private int pauseInt = 0;

	// Use this for initialization
	void Start () {
		nextBullet = Random.Range (1,5);
	}
	
	// Update is called once per frame
	void Update () {
		CheckPause ();
		BulletInput ();
		if(Input.GetKeyDown (KeyCode.W) == true && needToWait == false && isPaused == false){
			FireTheBullet ();
			needToWait = true;
			currentTime = 0.0f;
		}
		if(currentTime < waitTime && needToWait == true && isPaused == false){
			currentTime = currentTime + Time.deltaTime;
		}
		if(currentTime >= waitTime && needToWait == true && isPaused == false){
			needToWait = false;
			currentTime = 0.0f;
		}

	}
	void BulletInput(){
		if(Input.GetKeyDown (KeyCode.Alpha1) == true || Input.GetKeyDown (KeyCode.Keypad1) == true){
			BulletChange ("Fire");
		}
		else if(Input.GetKeyDown (KeyCode.Alpha2) == true || Input.GetKeyDown (KeyCode.Keypad2) == true){
			BulletChange ("Water");
		}
		else if(Input.GetKeyDown (KeyCode.Alpha3) == true || Input.GetKeyDown (KeyCode.Keypad3) == true){
			BulletChange ("Earth");
		}
		else if(Input.GetKeyDown (KeyCode.Alpha4) == true || Input.GetKeyDown (KeyCode.Keypad4) == true){
			BulletChange ("Air");
		}
	}
	public void BulletChange(string BulletElement){
		if(BulletElement == "Fire"){
			nextBullet = 1;
		}
		else if(BulletElement == "Water"){
			nextBullet = 2;
		}
		else if(BulletElement == "Earth"){
			nextBullet = 4;
		}
		else if(BulletElement == "Air"){
			nextBullet = 3;
		}
	}

	void FireTheBullet(){
		switch(nextBullet){
		case(1):
			if(currentSpawnPoint == 0){
				GameObject bulletClone = GameObject.Instantiate (fireBullet,spawnPoint1.transform.position,spawnPoint1.transform.rotation) as GameObject;
			}
			else if(currentSpawnPoint == 1){
				GameObject bulletClone = GameObject.Instantiate (fireBullet,spawnPoint2.transform.position,spawnPoint2.transform.rotation) as GameObject;
			}
			break;
		case(2):
			if(currentSpawnPoint == 0){
				GameObject bulletClone = GameObject.Instantiate (waterBullet,spawnPoint1.transform.position,spawnPoint1.transform.rotation) as GameObject;
			}
			else if(currentSpawnPoint == 1){
				GameObject bulletClone = GameObject.Instantiate (waterBullet,spawnPoint2.transform.position,spawnPoint2.transform.rotation) as GameObject;
			}
			break;
		case(3):
			if(currentSpawnPoint == 0){
				GameObject bulletClone = GameObject.Instantiate (airBullet,spawnPoint1.transform.position,spawnPoint1.transform.rotation) as GameObject;
			}
			else if(currentSpawnPoint == 1){
				GameObject bulletClone = GameObject.Instantiate (airBullet,spawnPoint2.transform.position,spawnPoint2.transform.rotation) as GameObject;
			}
			break;
		case(4):
			if(currentSpawnPoint == 0){
				GameObject bulletClone = GameObject.Instantiate (earthBullet,spawnPoint1.transform.position,spawnPoint1.transform.rotation) as GameObject;
			}
			else if(currentSpawnPoint == 1){
				GameObject bulletClone = GameObject.Instantiate (earthBullet,spawnPoint2.transform.position,spawnPoint2.transform.rotation) as GameObject;
			}
			break;
		case(5):
			//Missfire
			break;
		}

	}

	void ChangeSpawnPoint(){
		if(currentSpawnPoint == 0){
			currentSpawnPoint = 1;
		}
		else if(currentSpawnPoint == 1){
			currentSpawnPoint = 0;
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
