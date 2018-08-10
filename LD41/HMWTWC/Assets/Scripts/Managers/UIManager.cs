using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Text FinishText;
    [SerializeField] private CanvasGroup FinishCanvas;
    [SerializeField] private Text TurnCountdownText;
    [SerializeField] private Text NextSinkTime;
    [SerializeField] private CanvasGroup GameHud;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowFinishCanvas(string message)
    {
        GameHud.Hide();
        FinishText.text = message;
        FinishCanvas.Show();
    }

    public void ShowGameHud()
    {
        GameHud.Show();
    }

    public void UpdateGameHud(float turnFinishIn, float nextSinkIn)
    {
        TurnCountdownText.text = turnFinishIn.ToString("#");
        NextSinkTime.text = nextSinkIn.ToString("#.#");
    }
}