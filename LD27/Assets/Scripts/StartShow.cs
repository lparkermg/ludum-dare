using UnityEngine;
using System.Collections;

public class StartShow : MonoBehaviour {

	private bool CompletedGame;
	public GameObject BonusText;
	// Update is called once per frame
	void Update () {
		if(PlayerPrefs.GetInt ("CompletedGame") == 1)
		{
			CompletedGame = true;	
		}
		
		if(CompletedGame == false)
		{
			BonusText.renderer.enabled = false;
		}
		if(CompletedGame == true)
		{
			BonusText.renderer.enabled = true;
		}
	}
}
