using UnityEngine;
using System.Collections;

public class BulletLimit : MonoBehaviour {

	void OnTriggerExit(Collider objCol){
		if(objCol.gameObject.tag == "FireBullet" || objCol.gameObject.tag == "WaterBullet" || objCol.gameObject.tag == "AirBullet" || objCol.gameObject.tag == "EarthBullet"){
			Destroy (objCol.gameObject);
		}
	}
}
