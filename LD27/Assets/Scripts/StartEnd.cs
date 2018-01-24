using UnityEngine;
using System.Collections;

public class StartEnd : MonoBehaviour {
	
	private int GravCapsRemaining;
	private int TimePointsGot;
	private int PreviousPoints;
	
	public GameObject TPHolder;
	public GameObject GCInfoHolder;
	
	public int NextLevel;
	void OnTriggerEnter(Collider ObjCol)
	{
		if(gameObject.tag == "LevelStart")
		{
			
		}
		if(gameObject.tag == "LevelEnd")
		{
			GravCapsRemaining = GCInfoHolder.GetComponent<GravCapStuff>().GravCapPointsLeft;
			TimePointsGot = TPHolder.GetComponent<MainMech>().TimePointsTR;
			PreviousPoints = PlayerPrefs.GetInt ("PlayerPointsInt");
			PlayerPrefs.SetInt ("DoneFirstLevel",1);
			PlayerPrefs.SetInt ("PlayerPointsInt",GravCapsRemaining + TimePointsGot + PreviousPoints);
			Physics.gravity = new Vector3(0.0f,0.0f,0.0f);
			Application.LoadLevel (NextLevel);	
		}
	}
	
	void OnTriggerExit(Collider ObjCol)
	{
		
	}
}
