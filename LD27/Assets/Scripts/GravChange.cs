using UnityEngine;
using System.Collections;

public class GravChange : MonoBehaviour {
	
	public string GravChangeDir;
	
	public GameObject NorthArrow;
	public GameObject NorthEastArrow;
	public GameObject EastArrow;
	public GameObject SouthEastArrow;
	public GameObject SouthArrow;
	public GameObject SouthWestArrow;
	public GameObject WestArrow;
	public GameObject NorthWestArrow;
	
	public int GravCapsLeft;
	//public TextMesh GravCapsLeftText;
	void Update(){
		GravCapsLeft = PlayerPrefs.GetInt ("GravCapsLeftInt");
		if(GravChangeDir == "North")
		{
			NorthArrow.renderer.enabled = true;
			NorthEastArrow.renderer.enabled = false;
			EastArrow.renderer.enabled = false;
			SouthEastArrow.renderer.enabled = false;
			SouthArrow.renderer.enabled =false;
			SouthWestArrow.renderer.enabled = false;
			WestArrow.renderer.enabled = false;
			NorthWestArrow.renderer.enabled = false;
		}
		else if (GravChangeDir == "NorthEast")
		{
			NorthArrow.renderer.enabled = false;
			NorthEastArrow.renderer.enabled = true;
			EastArrow.renderer.enabled = false;
			SouthEastArrow.renderer.enabled = false;
			SouthArrow.renderer.enabled =false;
			SouthWestArrow.renderer.enabled = false;
			WestArrow.renderer.enabled = false;
			NorthWestArrow.renderer.enabled = false;
		}
		else if (GravChangeDir == "East")
		{
			NorthArrow.renderer.enabled = false;
			NorthEastArrow.renderer.enabled = false;
			EastArrow.renderer.enabled = true;
			SouthEastArrow.renderer.enabled = false;
			SouthArrow.renderer.enabled =false;
			SouthWestArrow.renderer.enabled = false;
			WestArrow.renderer.enabled = false;
			NorthWestArrow.renderer.enabled = false;
		}
		else if (GravChangeDir == "SouthEast")
		{
			NorthArrow.renderer.enabled = false;
			NorthEastArrow.renderer.enabled = false;
			EastArrow.renderer.enabled = false;
			SouthEastArrow.renderer.enabled = true;
			SouthArrow.renderer.enabled =false;
			SouthWestArrow.renderer.enabled = false;
			WestArrow.renderer.enabled = false;
			NorthWestArrow.renderer.enabled = false;
		}
		else if (GravChangeDir == "South")
		{
			NorthArrow.renderer.enabled = false;
			NorthEastArrow.renderer.enabled = false;
			EastArrow.renderer.enabled = false;
			SouthEastArrow.renderer.enabled = false;
			SouthArrow.renderer.enabled =true;
			SouthWestArrow.renderer.enabled = false;
			WestArrow.renderer.enabled = false;
			NorthWestArrow.renderer.enabled = false;
		}
		else if (GravChangeDir == "SouthWest")
		{
			NorthArrow.renderer.enabled = false;
			NorthEastArrow.renderer.enabled = false;
			EastArrow.renderer.enabled = false;
			SouthEastArrow.renderer.enabled = false;
			SouthArrow.renderer.enabled =false;
			SouthWestArrow.renderer.enabled = true;
			WestArrow.renderer.enabled = false;
			NorthWestArrow.renderer.enabled = false;
		}
		else if (GravChangeDir == "West")
		{
			NorthArrow.renderer.enabled = false;
			NorthEastArrow.renderer.enabled = false;
			EastArrow.renderer.enabled = false;
			SouthEastArrow.renderer.enabled = false;
			SouthArrow.renderer.enabled =false;
			SouthWestArrow.renderer.enabled = false;
			WestArrow.renderer.enabled = true;
			NorthWestArrow.renderer.enabled = false;
		}
		else if (GravChangeDir == "NorthWest")
		{
			NorthArrow.renderer.enabled = false;
			NorthEastArrow.renderer.enabled = false;
			EastArrow.renderer.enabled = false;
			SouthEastArrow.renderer.enabled = false;
			SouthArrow.renderer.enabled =false;
			SouthWestArrow.renderer.enabled = false;
			WestArrow.renderer.enabled = false;
			NorthWestArrow.renderer.enabled = true;
		}
		//GravCapsLeftText.GetComponent<TextMesh>().text = GravCapsLeft.ToString();
	}
	
	void OnTriggerEnter (Collider ObjCol){
		if(GravChangeDir == "North")
		{
			Physics.gravity = new Vector3(0.0f,0.0f,10.0f);	
		}
		if(GravChangeDir == "NorthEast")
		{
			Physics.gravity = new Vector3(10.0f,0.0f,10.0f);	
		}
		if(GravChangeDir == "East")
		{
			Physics.gravity = new Vector3(10.0f,0.0f,0.0f);	
		}
		if(GravChangeDir == "SouthEast")
		{
			Physics.gravity = new Vector3(10.0f,0.0f,-10.0f);	
		}
		if(GravChangeDir == "South")
		{
			Physics.gravity = new Vector3(0.0f,0.0f,-10.0f);	
		}
		if(GravChangeDir == "SouthWest")
		{
			Physics.gravity = new Vector3(-10.0f,0.0f,-10.0f);	
		}
		if(GravChangeDir == "West")
		{
			Physics.gravity = new Vector3(-10.0f,0.0f,0.0f);	
		}
		if(GravChangeDir == "NorthWest")
		{
			Physics.gravity = new Vector3(-10.0f,0.0f,10.0f);	
		}

		Destroy (gameObject);
	}
}
