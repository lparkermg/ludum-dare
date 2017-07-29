using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour {

	void OnTriggerStay(Collider col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			Destroy(gameObject);
		}
	}
}
