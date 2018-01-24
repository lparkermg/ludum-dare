using UnityEngine;
using System.Collections;

public class TeleportToo : MonoBehaviour {

	public Vector3 TeleportToLoc;
	
	void OnTriggerEnter(Collider objCol)
	{
		if(objCol.tag == "Player")
		{
			objCol.gameObject.transform.localPosition = new Vector3(TeleportToLoc.x,TeleportToLoc.y,TeleportToLoc.z);	
		}
	}
	
}
