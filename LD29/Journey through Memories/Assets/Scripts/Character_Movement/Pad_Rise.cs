using UnityEngine;
using System.Collections;

public class Pad_Rise : MonoBehaviour {

	public float riseSpeed = 0.05f;

	public bool isRising = false;
	private GameObject enteredObject;

	public bool padActive = false;
	// Update is called once per frame
	void Update () {
		if(isRising == true){
			enteredObject.transform.position = new Vector3(enteredObject.transform.position.x,enteredObject.transform.position.y + riseSpeed, enteredObject.transform.position.z);
		}
	}

	void OnTriggerEnter2D(Collider2D objCol){
		if(objCol.gameObject.tag == "Player" && padActive == true){
			enteredObject = objCol.gameObject;
			isRising = true;
		}
	}

	void OnTriggerExit2D(Collider2D ObjCol){
		if(ObjCol.gameObject.tag == "Player"){
			isRising = false;
		}
	}
}
