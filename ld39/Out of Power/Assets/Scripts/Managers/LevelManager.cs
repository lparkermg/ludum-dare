using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public List<Level> Levels;

	private bool _sinkingLevel = false;
	private bool _changingLevel = false;

	private float _minY = -5.0f;

	private int _level = 0;

	private Fortress _fortress;
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void LoadNewLevel(int levelNum,Fortress fortress)
	{
		var level = GameObject.FindGameObjectWithTag("Level");

		Destroy(level);
		var newLevel = Levels.First(lvl => lvl.Number == levelNum);
		Debug.Log(newLevel);
		GameObject levelClone =
			GameObject.Instantiate(newLevel.Prefab, new Vector3(0.0f, 0.0f, 0.0f), newLevel.Prefab.transform.rotation) as
				GameObject;
		fortress.MoveToStartPoint();
	}
	
	
}
