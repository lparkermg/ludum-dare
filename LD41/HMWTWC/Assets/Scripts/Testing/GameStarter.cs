﻿using System.Collections;
using System.Collections.Generic;
using Managers;
using SO;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public Names NamesList;
    public List<GameObject> PlayerTemplates;

	// Use this for initialization
	void Start () {
		GameplayManager.Initialise(NamesList,PlayerTemplates);
        GameplayManager.StartGame(25);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
