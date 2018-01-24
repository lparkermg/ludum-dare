using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	//Variables
	public float enemySpeed = 0.1f;
	public GameObject playerTarget;

	public Vector3 newPosition = new Vector3(0.0f,0.0f,0.0f);

	public bool isPaused = false;
	private int pauseInt = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		CheckPause ();
		if(isPaused == false){
			newPosition = transform.forward * (enemySpeed * Time.deltaTime);
			newPosition.y = 3.5f;
			transform.position = Vector3.Lerp (transform.position,newPosition,enemySpeed * Time.deltaTime);
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
