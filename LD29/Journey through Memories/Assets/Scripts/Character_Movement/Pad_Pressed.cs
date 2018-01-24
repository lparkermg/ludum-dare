using UnityEngine;
using System.Collections;

public class Pad_Pressed : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D objCol){
		if(objCol.gameObject.tag == "Player"){
			GameObject.FindGameObjectWithTag ("BluePad").GetComponent<Pad_Rise>().padActive = true;
			GameObject.FindGameObjectWithTag ("BluePartical").GetComponent<ParticleSystem>().Play();
		}
	}
}
