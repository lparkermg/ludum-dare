using UnityEngine;
using System.Collections;

public class OutOfTheWorld : MonoBehaviour {

	public GameObject PlayerObject;

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(PlayerObject.transform.position.x,-10.0f,PlayerObject.transform.position.z);
	}

	void OnTriggerEnter(Collider ObjCol){
		if(ObjCol.tag == "Player"){
			if(ObjCol.gameObject.GetComponent<CheckCube>().CheckCubePlaced == true){
				ObjCol.gameObject.transform.position = ObjCol.gameObject.GetComponent<CheckCube>().PlacementPosition;
				Destroy (GameObject.FindGameObjectWithTag ("CheckCube").gameObject);
				ObjCol.gameObject.GetComponent<CheckCube>().CheckCubePlaced = false;
			}
			else
			{
				Application.LoadLevel (5);
			}
		}
	}
}
