using UnityEngine;
using System.Collections;

public class BonusKeys : MonoBehaviour {

	public TextMesh GravDirText;
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.UpArrow) == true)
		{
			Physics.gravity = new Vector3(0.0f,0.0f,10.0f);	
			GravDirText.GetComponent<TextMesh>().text = "North";
		}
		if(Input.GetKeyDown (KeyCode.RightArrow) == true)
		{
			Physics.gravity = new Vector3(10.0f,0.0f,0.0f);	
			GravDirText.GetComponent<TextMesh>().text = "East";
		}
		if(Input.GetKeyDown (KeyCode.DownArrow) == true)
		{
			Physics.gravity = new Vector3(0.0f,0.0f,-10.0f);	
			GravDirText.GetComponent<TextMesh>().text = "South";
		}
		if(Input.GetKeyDown (KeyCode.LeftArrow) == true)
		{
			Physics.gravity = new Vector3(-10.0f,0.0f,0.0f);	
			GravDirText.GetComponent<TextMesh>().text = "West";
		}
		
	}
}
