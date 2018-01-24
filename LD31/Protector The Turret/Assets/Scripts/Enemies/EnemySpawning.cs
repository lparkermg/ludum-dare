using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemySpawning : MonoBehaviour {

	//Variables
	public List<GameObject> spawnNodes;
	public int currentSpawn;
	public int maxSpawnNodes;

	public int currentWave = 1;
	public int maxMobsCWave = 8;

	public int mobsSpawned = 0;

	public int upMaxMobsTop = 5;
	public int upMaxMobsBottom = 2;

	public float minTimeSpawn = 2.5f;
	public float maxTimeSpawn = 5.0f;
	public float currentSpawnTime = 0.0f;
	public float currentWait = 0.0f;

	public GameObject fireMob;
	public GameObject waterMob;
	public GameObject earthMob;
	public GameObject airMob;

	public int mobType;

	//Spawn the next 2 instantly or the next 4 instantly
	public bool isDouble = false;
	public bool isAll = false;

	public int amountPlaced = 0;
	public bool wasDouble = false;

	public int nextMoreIn = 5;
	public bool isPaused = false;
	private int pauseInt = 0;

	public GameObject currentWaveText;
	public GameObject currentPointsText;
	public GameObject endSceneObject;
	// Use this for initialization
	void Start () {
		SetupSpawns ();
		SetupInitialSettings ();
		nextMoreIn = Random.Range (2,currentWave + 5);
		currentPointsText.GetComponent<Text>().text = "Points: 0";
		currentWaveText.GetComponent<Text>().text = "Current Wave: " + currentWave;
	}
	
	// Update is called once per frame
	void Update () {
		CheckPause();
		if(isPaused == false){
			SpawnSystem ();
			currentPointsText.GetComponent<Text>().text = "Points: " + PlayerPrefs.GetFloat ("CurrentPoints").ToString("0");
		}
	}

	void SpawnSystem(){
		if(isDouble == true && amountPlaced < 2){
			currentSpawnTime = currentWait + 0.1f;
			amountPlaced++;
			//Debug.Log ("Is Double at " + Time.time);
		}
		else if(isAll ==  true && amountPlaced < 4){
			currentSpawnTime = currentWait + 0.1f;
			amountPlaced++;
			//Debug.Log ("Is all at " + Time.time);
		}
		else if(isDouble == false && isAll == false){
			if(currentSpawnTime < currentWait){
				//We add to the timer.
				currentSpawnTime = currentSpawnTime + Time.deltaTime;
			}
		}
		if(currentSpawnTime >= currentWait){
				//We Spawn.
				SelectMobType ();
				switch(mobType){
				case(1):
					GameObject fireMobClone = GameObject.Instantiate (fireMob,spawnNodes[currentSpawn].transform.position,Quaternion.Euler (new Vector3(0.0f,0.0f,0.0f))) as GameObject;
					fireMobClone.GetComponent<EnemyMovement>().enemySpeed = Random.Range (0.0001f,(float)currentWave / 100.0f);
					fireMobClone.GetComponent<EnemyHit>().endScene = endSceneObject;
					break;
				case(2):
					GameObject waterMobClone = GameObject.Instantiate (waterMob,spawnNodes[currentSpawn].transform.position,Quaternion.Euler (new Vector3(0.0f,0.0f,0.0f))) as GameObject;
					waterMobClone.GetComponent<EnemyMovement>().enemySpeed = Random.Range (0.0001f,(float)currentWave / 100.0f);
					waterMobClone.GetComponent<EnemyHit>().endScene = endSceneObject;
					break;
				case(3):
					GameObject earthMobClone = GameObject.Instantiate (earthMob,spawnNodes[currentSpawn].transform.position,Quaternion.Euler (new Vector3(0.0f,0.0f,0.0f))) as GameObject;
					earthMobClone.GetComponent<EnemyMovement>().enemySpeed = Random.Range (0.0001f,(float)currentWave / 100.0f);
					earthMobClone.GetComponent<EnemyHit>().endScene = endSceneObject;
					break;
				case(4):
					GameObject airMobClone = GameObject.Instantiate (airMob,spawnNodes[currentSpawn].transform.position,Quaternion.Euler (new Vector3(0.0f,0.0f,0.0f))) as GameObject;
					airMobClone.GetComponent<EnemyMovement>().enemySpeed = Random.Range (0.0001f,(float)currentWave / 100.0f);
					airMobClone.GetComponent<EnemyHit>().endScene = endSceneObject;
					break;
				case(5):
					GameObject air2MobClone = GameObject.Instantiate (airMob,spawnNodes[currentSpawn].transform.position,Quaternion.Euler (new Vector3(0.0f,0.0f,0.0f))) as GameObject;
					air2MobClone.GetComponent<EnemyMovement>().enemySpeed = Random.Range (0.0001f,(float)currentWave / 100.0f);
					air2MobClone.GetComponent<EnemyHit>().endScene = endSceneObject;
					break;
				}
				//Debug.Log ("Spawned at: " + Time.time);
				if(isDouble == true && amountPlaced >= 2){
					isDouble = false;
					wasDouble = true;
					amountPlaced = 0;
				}
				else if(isAll ==  true && amountPlaced >= 4){
					isAll = false;
					wasDouble = false;
					amountPlaced = 0;
				}

				WaveCheck ();
				NextSpawnTime ();
				NextSpawnPlace ();
				mobsSpawned++;
		}

	}

	void OneOrDouble(){
		if(nextMoreIn == 0 && wasDouble == false){
			isDouble = true;
			nextMoreIn = Random.Range (2,currentWave * mobsSpawned + 1);
		}
		else if(nextMoreIn == 0 & wasDouble == true){
			isAll = true;
			nextMoreIn = Random.Range (2,currentWave * mobsSpawned + 1);
		}
	}

	void WaveCheck(){
		//Check if we need to up the wave.
		if(mobsSpawned >= maxMobsCWave){
			WaveComplete ();
		}

	}
	void SelectMobType(){
		mobType = Random.Range (1,5);
	}

	void NextSpawnPlace(){
		currentSpawn++;
		if(currentSpawn > maxSpawnNodes - 1){
			currentSpawn = 0;
		}
	}

	void SetupSpawns(){
		GameObject[] sNodes = GameObject.FindGameObjectsWithTag("SpawnArea");
		foreach(GameObject sNode in sNodes)
		{
			AddSpawnPoints (sNode);
			maxSpawnNodes = maxSpawnNodes + 1;
		}
	}

	void AddSpawnPoints(GameObject spawnPoint){
		spawnNodes.Add (spawnPoint);
	}

	void WaveComplete(){
		currentWave = currentWave + 1;
		maxMobsCWave = maxMobsCWave + Random.Range (upMaxMobsBottom,upMaxMobsTop);
		if(minTimeSpawn > 0.5f){
			minTimeSpawn = minTimeSpawn - 0.01f;
		}
		if(maxTimeSpawn > 1.0f){
			maxTimeSpawn = maxTimeSpawn - 0.01f;
		}
		mobsSpawned = 0;
		isAll = true;
		amountPlaced = 0;
		PlayerPrefs.SetInt ("CurrentWave",currentWave);
		currentWaveText.GetComponent<Text>().text = "Current Wave: " + currentWave;
		//Debug.Log ("New Wave at " + Time.time);
	}

	void NextSpawnTime(){
		currentWait = Random.Range (minTimeSpawn,maxTimeSpawn);
		currentSpawnTime = 0.0f;
	}

	void SetupInitialSettings(){
		currentWait = Random.Range (minTimeSpawn,maxTimeSpawn);
		currentWave = 1;
		maxMobsCWave = 8;
	}

	void CheckPause(){
		pauseInt = PlayerPrefs.GetInt ("IsPaused");
		if(pauseInt == 0){
			isPaused = false;
		}
		else if(pauseInt == 1){
			isPaused = true;
		}
	}
}
