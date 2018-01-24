using UnityEngine;
using System.Collections;

public class RotateBkg : MonoBehaviour {

	private float curAng;
	public float RotateInc = 0.01f;
	public bool ButtonClicked = false;
	// Update is called once per frame
	void Update () {
		if(ButtonClicked == true)
		{
		curAng = gameObject.transform.rotation.y;
		gameObject.transform.Rotate (new Vector3(0.0f,curAng + RotateInc,0.0f));
		}
	}
}
