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
		var newLevel = Levels[Random.Range(0, Levels.Count)];
		GameObject levelClone =
			GameObject.Instantiate(newLevel.LayoutPrefab, new Vector3(0.0f, 0.0f, 0.0f), newLevel.LayoutPrefab.transform.rotation) as
				GameObject;
		levelClone.tag = "Level";
		_levelsCompleted++;
		StartCoroutine(PopulateNewLevel(fortress));
	}

	private IEnumerator PopulateNewLevel(Fortress fortress)
	{
		yield return null;
		//TODO: Select a single start position and spawn an island moving the fortress to it.
		var startPositions = GameObject.FindGameObjectsWithTag("StartSection").ToList();

		var startPos = startPositions[Random.Range(0, startPositions.Count)];

		var selectedStartIsland = Islands[Random.Range(0, Islands.Count)];
		GameObject startIsland = GameObject.Instantiate(selectedStartIsland, startPos.transform.position,
			selectedStartIsland.transform.rotation,startPos.transform) as GameObject;

		fortress.gameObject.transform.position =
			new Vector3(startPos.transform.position.x, 2.0f, startPos.transform.position.z);
		
		//TODO: Randomly select half of the main area spawn random islands in with a few randomly having charging points.
		var midPositions = GameObject.FindGameObjectsWithTag("MainSection").ToList();
		foreach (var position in midPositions)
		{
			var island = Islands[Random.Range(0, Islands.Count)];
			GameObject midIslandClone = GameObject.Instantiate(island,
				position.transform.position, island.transform.rotation,position.transform) as GameObject;
			GameObject chargePoint = GameObject.Instantiate(ChargePoint, position.transform.position,
				position.transform.rotation, midIslandClone.transform) as GameObject;
		}
		//TODO: Select a single finish position and spawn an island adding the finish point to it.
		var finishPositions = GameObject.FindGameObjectsWithTag("FinishSection").ToList();
		var finishPos = finishPositions[Random.Range(0, finishPositions.Count)];
		var selectedFinishIsland = Islands[Random.Range(0, Islands.Count)];
		GameObject finishIsland = GameObject.Instantiate(selectedFinishIsland,
			finishPos.transform.position, selectedFinishIsland.transform.rotation,finishPos.transform) as GameObject;
		GameObject finishPoint = GameObject.Instantiate(FinishPoint, finishPos.transform.position,
			finishPos.transform.rotation, finishIsland.transform) as GameObject;
		yield return null;
	}
}
