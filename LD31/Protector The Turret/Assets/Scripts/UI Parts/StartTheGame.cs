using UnityEngine;
using System.Collections;

public class StartTheGame : MonoBehaviour {

	public void StartGame(){
		PlayerPrefs.SetInt ("IsPaused",0);
		Destroy (gameObject);
	}
}
