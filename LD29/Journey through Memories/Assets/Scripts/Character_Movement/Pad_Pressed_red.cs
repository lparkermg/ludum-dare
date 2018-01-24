using UnityEngine;
using System.Collections;

public class Pad_Pressed_red : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D objCol){
		if(objCol.gameObject.tag == "Player"){
			GameObject.FindGameObjectWithTag ("RedPad").GetComponent<Pad_Rise>().padActive = true;
			GameObject.FindGameObjectWithTag ("RedPartical").GetComponent<ParticleSystem>().Play();
		}
	}
}
