using UnityEngine;
using System.Collections;

public class Shard_Watcher : MonoBehaviour {

	public int currentShards = 0;
	public int shardsNeeded = 5;

	public GameObject theDoor;
	//public GameObject theCamera;
	public GameObject theSign;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(currentShards >= shardsNeeded){
			//Remove the fekkin door.
			Destroy (theDoor);
			//Add the Fekkin Sign.
			theSign.SetActive(true);
			//GameObject signClone = GameObject.Instantiate (theSign) as GameObject;
			//signClone.transform.IsChildOf (theCamera.transform);
		}
		else{
			theSign.SetActive(false);
		}
	}
}
