using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {

	public bool TenSecondsReached = false;
	private bool FirstMoveDone = false;
	// Update is called once per frame
	void Update () {
		if(TenSecondsReached == true && FirstMoveDone == false)
		{
			Physics.gravity = new Vector3(0.0f,0.0f,-10.0f);
			rigidbody.AddForce(new Vector3(0.0f,0.0f,0.1f),ForceMode.Impulse);
			FirstMoveDone = true;
		}
	}
}
