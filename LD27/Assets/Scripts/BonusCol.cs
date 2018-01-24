using UnityEngine;
using System.Collections;

public class BonusCol : MonoBehaviour {

	void OnTriggerEnter(Collider objCol){
		if(objCol.tag == "Player")
		{
			Application.LoadLevel (15);	
		}
	}
}
