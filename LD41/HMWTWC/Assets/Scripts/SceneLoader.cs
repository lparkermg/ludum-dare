using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    private Player _player;
	// Use this for initialization
	void Start ()
	{
	    _player = ReInput.players.GetPlayer(0);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    InputCheck();
	}

    private void InputCheck()
    {
        var select = _player.GetButtonDown("Select");

        if (select)
            SceneManager.LoadScene(1);
    }
}
