using UnityEngine;
using System.Collections;

public class CheckCube : MonoBehaviour {

	public GameObject CheckCubeObj;
	public bool CheckCubePlaced = false;

	private int CheckCubePlacedAmount = 0;

	public Vector3 PlacementPosition;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.C) == true && CheckCubePlaced == false){
			PlacementPosition = transform.position;
			GameObject CheckCubeClone = GameObject.Instantiate (CheckCubeObj,PlacementPosition,Quaternion.Euler (new Vector3(0.0f,0.0f,0.0f))) as GameObject;
			CheckCubePlacedAmount = PlayerPrefs.GetInt ("CheckCubeSetAmount");
			CheckCubePlacedAmount = CheckCubePlacedAmount + 1;
			PlayerPrefs.SetInt ("CheckCubeSetAmount",CheckCubePlacedAmount);
			CheckCubePlaced = true;
		}
	}
}
