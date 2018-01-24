using UnityEngine;
using System.Collections;

public class Hit_Block : MonoBehaviour {

	//Collision Shizzle
	void OnCollisionEnter2D(Collision2D objCol){
		//Debug.Log ("#CollisionDetected");
		if(objCol.gameObject.tag == "Player"){
			objCol.gameObject.GetComponent<Player_Movement_2D>().isGrounded = true;
			//Debug.Log ("#CanJumpNow");
		}
	}
}
