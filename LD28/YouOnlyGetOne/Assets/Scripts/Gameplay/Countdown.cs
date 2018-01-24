using UnityEngine;
using System.Collections;

public class Countdown : MonoBehaviour {

	private int TimeLeft =0;
	private int TotalTime = 0;

	private float WaitMax = 1.0f;
	private float WaitCurrent = 0.0f;

	public GUIText CountText;
	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("TimeLeftStore", 60);
		PlayerPrefs.SetInt ("TotalTimeStore",0);
		PlayerPrefs.SetInt ("CoinsCollectedStore",0);
		PlayerPrefs.SetInt ("CheckCubeSetAmount",0);
	}
	
	// Update is called once per frame
	void Update () {
		WaitCurrent += Time.deltaTime;
		if(WaitMax < WaitCurrent){
			TimeLeft = PlayerPrefs.GetInt ("TimeLeftStore");
			TotalTime = PlayerPrefs.GetInt ("TotalTimeStore");
			TimeLeft = TimeLeft - 1;
			TotalTime = TotalTime + 1;
			PlayerPrefs.SetInt ("TimeLeftStore", TimeLeft);
			PlayerPrefs.SetInt ("TotalTimeStore", TotalTime);
			CountText.text = "Time Left: " + TimeLeft + "(Seconds)";
			if(TimeLeft <= 0){
				Application.LoadLevel (5);
			}
			WaitCurrent = 0.0f;
		}
	}
}
