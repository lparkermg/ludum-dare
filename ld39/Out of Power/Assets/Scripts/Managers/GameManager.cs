using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class GameManager : MonoBehaviour
{
	public CanvasGroup GameOverCanvas;

	public bool GameOver { get; private set; }
	// Use this for initialization
	void Start () {
		
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
