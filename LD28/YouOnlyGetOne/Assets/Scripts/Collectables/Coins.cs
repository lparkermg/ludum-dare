using UnityEngine;
using System.Collections;

public class Coins : MonoBehaviour {
	public bool PlayerPassed = false;

	private int CoinsCollected = 0;
	private int CurrentTime;

	void Update(){
		transform.LookAt (GameObject.FindGameObjectWithTag("Player").transform.position);
		if(PlayerPassed == true && audio.isPlaying == false){
			Destroy (gameObject);
		}
	}
	void OnTriggerEnter(Collider ObjCol){
		if(ObjCol.tag == "Player" && PlayerPassed == false){
			CurrentTime = PlayerPrefs.GetInt ("TimeLeftStore");
			CurrentTime = CurrentTime + 1;
			PlayerPrefs.SetInt ("TimeLeftStore",CurrentTime);
			CoinsCollected = PlayerPrefs.GetInt ("CoinsCollectedStore");
			CoinsCollected = CoinsCollected + 1;
			PlayerPrefs.SetInt ("CoinsCollectedStore",CoinsCollected);
			audio.Play ();
			PlayerPassed = true;
		}
	}
}
