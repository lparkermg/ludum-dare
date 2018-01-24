using UnityEngine;
using System.Collections;

public class AboutCol : MonoBehaviour {

	void OnTriggerEnter(Collider objCol){
		if(objCol.tag == "Player")
		{
			Application.LoadLevel (16);	
		}
	}
}
