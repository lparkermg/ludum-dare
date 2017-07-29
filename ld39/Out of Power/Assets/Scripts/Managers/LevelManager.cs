using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public List<LevelLayout> Levels;

	public List<GameObject> Islands;

	public GameObject ChargePoint;
	public GameObject FinishPoint;

	private bool _sinkingLevel = false;
	private bool _changingLevel = false;

	private float _minY = -5.0f;

	private int _level = 0;

	private int _levelsCompleted = 0;
	public int LevelsCompleted
	{
		get { return _levelsCompleted; }
	}

	private Fortress _fortress;
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void LoadLevelLayout(Fortress fortress)
	{
		var levelLayout = GameObject.FindGameObjectWithTag("Level");

		Destroy(levelLayout);
		//TODO: Loop through and destroy all the islands.
		var newLevel = Levels[Random.Range(0, Levels.Count)];
		Debug.Log(newLevel);
		GameObject levelClone =
			GameObject.Instantiate(newLevel.LayoutPrefab, new Vector3(0.0f, 0.0f, 0.0f), newLevel.LayoutPrefab.transform.rotation) as
				GameObject;
		_levelsCompleted++;
	}

	private void PopulateNewLevel()
	{
		//TODO: Select a single start position and spawn an island moving the fortress to it.
		var startPositions = GameObject.FindGameObjectsWithTag("StartSection").ToList();

		var startPos = startPositions[Random.Range(0, startPositions.Count)];
		GameObject startIsland = GameObject.Instantiate(Islands[Random.Range(0, Islands.Count)], startPos.transform.position,
			startPos.transform.rotation) as GameObject;
		
		//TODO: Randomly select half of the main area spawn random islands in with a few randomly having charging points.
		var midPositions = GameObject.FindGameObjectsWithTag("MainSection").ToList();
		foreach (var position in midPositions)
		{
			GameObject midIslandClone = GameObject.Instantiate(Islands[Random.Range(0, Islands.Count)],
				position.transform.position, position.transform.rotation) as GameObject;
			//TODO: Add charge point here or something.
		}
		//TODO: Select a single finish position and spawn an island adding the finish point to it.
		var finishPositions = GameObject.FindGameObjectsWithTag("FinishSection").ToList();

		var finishPos = finishPositions[Random.Range(0, finishPositions.Count)];
		GameObject finishIsland = GameObject.Instantiate(Islands[Random.Range(0, Islands.Count)],
			finishPos.transform.position, finishPos.transform.rotation) as GameObject;

	}
}
