using UnityEngine;
using System.Collections;

public class EnemyHit : MonoBehaviour {

	//Variables
	public string bulletEffectsMe = "None";
	public float currentScore = 0.0f;
	public GameObject endScene;
	public AudioClip hitClip;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider objCol){
		//Check what the bullet tag is.
		if(objCol.gameObject.tag == bulletEffectsMe){
			AudioSource.PlayClipAtPoint (hitClip,transform.position);
			
			Destroy(objCol.gameObject);
			Destroy (gameObject);
			currentScore = PlayerPrefs.GetFloat ("CurrentPoints");
			PlayerPrefs.SetFloat("CurrentPoints",currentScore + 10.0f);
		}

		if(objCol.gameObject.tag == "Player"){
			PlayerPrefs.SetInt ("IsPaused",1);
			GameObject endSceneClone = GameObject.Instantiate (endScene) as GameObject;
		}
	}
}
