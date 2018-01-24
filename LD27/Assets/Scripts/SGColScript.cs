using UnityEngine;
using System.Collections;

public class SGColScript : MonoBehaviour {

	void OnTriggerEnter(Collider ObjCol)
	{
		if(ObjCol.tag == "Player")
		{
			PlayerPrefs.SetInt ("DoneFirstLevel",0);
			Application.LoadLevel (1);	
		}
	}
}
