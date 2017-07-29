using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{

	public int ToLevel = 1;

	private LevelManager _levelManager;
	// Use this for initialization
	void Start () {
		_levelManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			LoadLevel(other.gameObject.GetComponent<Fortress>());
		}
		
	}

	private void LoadLevel(Fortress fortress)
	{
		fortress.SetControlability(true);
		_levelManager.LoadNewLevel(ToLevel,fortress);
		fortress.SetControlability(false);
		//Sink current level
		//Remove current level.
		//Spawn in new level.
		//Raise new level.
		//Move fortress to start point
		//Re-enable fortress controls.
	}
}
