using UnityEngine;
using System.Collections;

public class Camera_Follow : MonoBehaviour {

	public GameObject playerObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(playerObject.transform.position.x,playerObject.transform.position.y,-0.5f);
	}
}
