using UnityEngine;
using System.Collections;

public class ShowInfo : MonoBehaviour {

	public GUIText CoinCollectText;
	public GUIText CheckCubePlcAmount;
	public GUIText TotalTimeText;

	private int CoinsCollected = 0;
	private int CheckCubePlacedTimes = 0;
	private int TotalSeconds = 0;

	private bool DataLoaded = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(DataLoaded == false){
			CoinsCollected = PlayerPrefs.GetInt ("CoinsCollectedStore");
			CheckCubePlacedTimes = PlayerPrefs.GetInt ("CheckCubeSetAmount");
			TotalSeconds = PlayerPrefs.GetInt ("TotalTimeStore");

			CoinCollectText.text = "Coins Collected: " + CoinsCollected.ToString ("0");
			CheckCubePlcAmount.text = "Check Cube Placed: " + CheckCubePlacedTimes.ToString ("0");
			TotalTimeText.text = "Total Time (Seconds): " + TotalSeconds.ToString ("0");

			DataLoaded = true;
		}
	}
}
