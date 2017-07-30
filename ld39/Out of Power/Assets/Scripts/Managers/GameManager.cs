using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public CanvasGroup GameOverCanvas;
	public Text AmountText;
	
	public CanvasGroup StartCanvas;
	public Text StartText;
	public Fortress Fortress;

	public bool GameOver { get; private set; }

	public bool Started { get; private set; }
	
	private LevelManager _levelManager;
	// Use this for initialization
	void Start ()
	{
		_levelManager = GetComponent<LevelManager>();
		_levelManager.LoadLevelLayout(Fortress,true);
		StartText.text = "Press 'Space' to fart.";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void ShowGameOver()
	{
		GameOver = true;
		AmountText.text = "You completed " + (_levelManager.LevelsCompleted == 1 ? _levelManager.LevelsCompleted + " Level.": _levelManager.LevelsCompleted + " Levels.");
		GameOverCanvas.Show();
	}

	public void StartGame()
	{
		if (!Started && !GameOver)
		{
			Started = true;
			StartCanvas.Hide();
		}
		else if (Started && GameOver)
		{
			GameOver = false;
			_levelManager.ResetCounter();
			_levelManager.LoadLevelLayout(Fortress);
			GameOverCanvas.Hide();
		}
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
