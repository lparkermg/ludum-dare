using UnityEngine;
using System.Collections;

public class TriggerEnd : MonoBehaviour {

	void OnTriggerEnter(){
		Application.LoadLevel (5);
	}
}
