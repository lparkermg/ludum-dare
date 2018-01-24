using UnityEngine;
using System.Collections;

public class MainMech : MonoBehaviour {

	public bool GameStarted = false;
	public GameObject StartMessage;
	
	private float currentTime = 0.0f;
	private float secondWait = 1.0f;
	
	private int SecondsPassed = -10;
	
	public TextMesh SecondsTxt;
	public TextMesh GravCapsLeftText;
	public TextMesh CurrentPointsText;
	public GameObject GameStartHolder;
	
	public GameObject StartText;
	public GameObject FinishText;
	
	public bool isFirstLevel;

	public int TimePointsTR = 10;
	// Update is called once per frame
	void Update () {
		if(GameStarted == false && isFirstLevel == true)
		{
			StartText.renderer.enabled = false;
			FinishText.renderer.enabled = false;	
		}
		if(Input.GetKeyDown (KeyCode.Space) == true && GameStarted == false && isFirstLevel == true)
		{
			Destroy (StartMessage);
			StartText.renderer.enabled = true;
			FinishText.renderer.enabled = true;
			GameStarted = true;
		}
		if(isFirstLevel == false)
		{
			GameStarted = true;
		}
		if(GameStarted == true)
		{
			currentTime = currentTime + Time.deltaTime;
			
			if(currentTime >= secondWait)
			{
				SecondsPassed = SecondsPassed + 1;
				SecondsTxt.GetComponent<TextMesh>().text = SecondsPassed.ToString ();
				if(SecondsPassed >= 0)
				{
				TimePointsTR = TimePointsTR - 1;
				}
				currentTime = 0.0f;
			}
			if(SecondsPassed >= 0)
			{
				GameStartHolder.GetComponent<GameStart>().TenSecondsReached = true;
			}
		}

		GravCapsLeftText.GetComponent<TextMesh>().text = PlayerPrefs.GetInt ("GravCapsLeftInt").ToString ();
		CurrentPointsText.GetComponent<TextMesh>().text = PlayerPrefs.GetInt ("PlayerPointsInt").ToString ();
	}
}
