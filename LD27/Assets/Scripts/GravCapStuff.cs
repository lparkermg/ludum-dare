using UnityEngine;
using System.Collections;

public class GravCapStuff : MonoBehaviour {
	
	private Ray ray;
	private RaycastHit hit;
	public GameObject GravCapObj;
	
	private bool GravCapPlaced = false;
	private bool GravCapDirChanged = false;
	private string curGravDir;
	private string tempGravDir;
	private int GravCapsLeftGCS;
	public int GravCapPointsLeft = 30;
	
	private bool hasGameStarted = false;
	
	public int CurrentLevel;
	public GameObject MainMechObj;
	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("GravCapsLeftInt",3);
		if(PlayerPrefs.GetInt ("DoneFirstLevel") == 0)
		{
		PlayerPrefs.SetInt ("PlayerPointsInt",100);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//GravCap Placement Code - Left Click
		GravCapsLeftGCS = PlayerPrefs.GetInt ("GravCapsLeftInt");
		hasGameStarted = MainMechObj.GetComponent<MainMech>().GameStarted;
		if(Input.GetMouseButtonDown (0) && GravCapPlaced == false && hasGameStarted == true && GravCapsLeftGCS > 0)
		{
			ray = camera.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit,250.0f))
			{
				
				if(hit.point.x <= 10.0f && hit.point.x >= -10.0f && hit.point.z <= 9.0f && hit.point.z >= -9.0f){
					
				GravCapsLeftGCS = PlayerPrefs.GetInt ("GravCapsLeftInt");
				GameObject GravCapInst = GameObject.Instantiate (GravCapObj,new Vector3(hit.point.x,2.0f, hit.point.z),Quaternion.Euler(new Vector3(0.0f,0.0f,0.0f))) as GameObject;
				GravCapInst.GetComponent <GravChange>().GravChangeDir = "North";
				GravCapsLeftGCS = GravCapsLeftGCS - 1;
				GravCapPointsLeft = GravCapPointsLeft - 10;
				PlayerPrefs.SetInt ("GravCapsLeftInt",GravCapsLeftGCS);
				GravCapPlaced = true;
				}
				Debug.Log (hit.point);
			}
			//Debug.Log (Input.mousePosition.x + " " + Input.mousePosition.z);
		}
		
		if(Input.GetMouseButtonUp (0) && GravCapPlaced == true)
		{
			GravCapPlaced = false;
		}
		//GravCap Direction Code - Right Click
		if(Input.GetMouseButtonDown (1) && GravCapDirChanged == false)
		{
			ray = camera.ScreenPointToRay (Input.mousePosition);
			if(Physics.Raycast (ray, out hit,250.0f))
			{
				if(hit.collider.tag == "GravCapTag")
				{
					curGravDir = hit.collider.GetComponent<GravChange>().GravChangeDir;
					hit.collider.GetComponent<GravChange>().GravChangeDir = NextDir (curGravDir);
					GravCapDirChanged = true;
				}
			}
		}
		if(Input.GetMouseButtonUp (1) && GravCapDirChanged == true)
		{
			GravCapDirChanged = false;	
		}
		if(Input.GetKeyDown (KeyCode.R) && hasGameStarted == true)
		{
			Physics.gravity = new Vector3(0.0f,0.0f,0.0f);
			Application.LoadLevel (CurrentLevel);	
		}
	}
	string NextDir(string curDir)
	{
		
		if(curDir == "North")
		{
			tempGravDir = "NorthEast";
		}
		else if(curDir == "NorthEast")
		{
			tempGravDir = "East";
		}
		else if(curDir == "East")
		{
			tempGravDir = "SouthEast";
		}
		else if(curDir == "SouthEast")
		{
			tempGravDir = "South";
		}
		else if(curDir == "South")
		{
			tempGravDir = "SouthWest";
		}
		else if(curDir == "SouthWest")
		{
			tempGravDir = "West";
		}
		else if(curDir == "West")
		{
			tempGravDir = "NorthWest";
		}
		else if(curDir == "NorthWest")
		{
			tempGravDir = "North";
		}
		return tempGravDir;
	}
}
