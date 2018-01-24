using UnityEngine;
using System.Collections;

public class UDDetect : MonoBehaviour {
	
	public bool CompletedGame;
	public bool EndGame = false;
	// Update is called once per frame
	void Update () {
		if(PlayerPrefs.GetInt ("CompletedGame") == 1)
		{
			CompletedGame = true;
		}
	if(Input.GetKeyDown(KeyCode.UpArrow) == true && EndGame == false)
		{
			rigidbody.AddForce (new Vector3(0.0f,0.0f,4.0f),ForceMode.Impulse);

		}
	if(Input.GetKeyDown(KeyCode.DownArrow) == true && EndGame == false)
		{
			rigidbody.AddForce (new Vector3(0.0f,0.0f,-4.0f),ForceMode.Impulse);

		}
	if(Input.GetKeyDown (KeyCode.RightArrow) == true && CompletedGame == true && EndGame == false)	
		{
			rigidbody.AddForce (new Vector3(4.0f,0.0f,0.0f),ForceMode.Impulse);
		}
	}
}
