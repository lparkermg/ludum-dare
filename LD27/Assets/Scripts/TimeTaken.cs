using UnityEngine;
using System.Collections;

public class TimeTaken : MonoBehaviour {
	
	private bool GameStarted = false;
	private float currentTime = 0.0f;
	private float secondWait = 1.0f;
	private string secondstxt;
	private int SecondsPassed = -10;
	public TextMesh TimePassedTxt;
	// Update is called once per frame
	void Update () {
	if(GameStarted == false)
		{
			currentTime = currentTime + Time.deltaTime;
			
			if(currentTime >= secondWait)
			{
				SecondsPassed = SecondsPassed + 1;
				secondstxt = SecondsPassed.ToString();
				GetComponent<TextMesh>().text = secondstxt;
				currentTime = 0.0f;
			}
		}
	}
}
