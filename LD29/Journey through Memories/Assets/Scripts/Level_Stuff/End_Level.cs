using UnityEngine;
using System.Collections;

public class End_Level : MonoBehaviour {

	public int sceneToLoad = 0;

	void OnTriggerEnter2D(Collider2D objCol){
		if(objCol.gameObject.tag == "Player"){
			Application.LoadLevel (sceneToLoad);
		}
	}
}
