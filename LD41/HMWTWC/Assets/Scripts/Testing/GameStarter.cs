using System.Collections;
using System.Collections.Generic;
using Managers;
using SO;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public Names NamesList;

	// Use this for initialization
	void Start () {
		GameplayManager.Initialise(NamesList);
        GameplayManager.StartGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
