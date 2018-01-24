using UnityEngine;
using System.Collections;

public class Collect_Shard : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D objCol){
		//Debug.Log ("Collision.Shard");
		if(objCol.gameObject.tag == "Player"){
			//Debug.Log ("Collision.With.Player");
			objCol.gameObject.audio.PlayOneShot(objCol.gameObject.audio.clip);
			GameObject.FindGameObjectWithTag("ShardWatcher").GetComponent<Shard_Watcher>().currentShards = GameObject.FindGameObjectWithTag("ShardWatcher").GetComponent<Shard_Watcher>().currentShards + 1;
			Destroy(gameObject);

		}
	}
}
