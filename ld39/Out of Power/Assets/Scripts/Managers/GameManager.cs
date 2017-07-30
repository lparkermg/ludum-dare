using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class GameManager : MonoBehaviour
{
	public CanvasGroup GameOverCanvas;
	public Fortress Fortress;

	public bool GameOver { get; private set; }

	private LevelManager _levelManager;
	// Use this for initialization
	void Start ()
	{
		_levelManager = GetComponent<LevelManager>();
		_levelManager.LoadLevelLayout(Fortress);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void ShowGameOver()
	{
		GameOver = true;
		GameOverCanvas.Show();
	}
}
