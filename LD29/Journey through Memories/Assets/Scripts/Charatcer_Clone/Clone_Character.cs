using UnityEngine;
using System.Collections;

public class Clone_Character : MonoBehaviour {

	public GameObject characterObject;
	private GameObject currentClone;
	public bool readyToClone = false;
	public bool firstTimeClone = true;

	public float cloneWaitTime = 15.0f;
	public float currentTime = 0.0f;

	void Update(){
		currentTime = currentTime + Time.deltaTime;
		if(Input.GetKeyUp (KeyCode.C) == true && readyToClone == true){
			if(firstTimeClone == false){
				currentClone = GameObject.FindGameObjectWithTag ("PlayerClone").gameObject;
			}
			
			if(currentClone != null){
				Destroy (currentClone);
			}
			currentClone = GameObject.Instantiate (characterObject,new Vector3(gameObject.transform.position.x + 1.0f,gameObject.transform.position.y), gameObject.transform.rotation) as GameObject;
			currentTime = 0.0f;
			readyToClone = false;
			firstTimeClone = false;
		}
		if(currentTime > cloneWaitTime){
			readyToClone = false;
			currentTime = 0.0f;
		}
	}
}
