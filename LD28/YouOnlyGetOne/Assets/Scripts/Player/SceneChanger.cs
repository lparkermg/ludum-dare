using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour {
	public int SceneTo = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.Space) == true){
			Application.LoadLevel (SceneTo);
		}
	}
}
