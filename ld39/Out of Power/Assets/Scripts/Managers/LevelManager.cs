using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

	public GameObject[] Levels;

	private GameObject _currentLevel;

	private bool _sinkingLevel = false;
	private bool _changingLevel = false;

	private float _minY = -5.0f;

	private int _level = 0;

	private Fortress _fortress;
	// Use this for initialization
	void Start ()
	{
		_currentLevel = Levels[_level];
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_changingLevel)
			ChangeLevel();
	}

	private void ChangeLevel()
	{
		LevelMovement(_sinkingLevel);

		if (_sinkingLevel && _currentLevel.transform.position.y <= _minY)
		{
			LoadNewLevel(_level);
			_sinkingLevel = false;
		}
		else if (!_sinkingLevel && _currentLevel.transform.position.y >= 0.0f)
		{
			_changingLevel = false;
			_fortress.MoveToStartPoint();
		}
	}

	private void LevelMovement(bool sinking)
	{
		var currentY = _currentLevel.transform.position.y;
		
		if (sinking)
		{
			_currentLevel.transform.position = new Vector3(0.0f,currentY - Time.deltaTime * 5.0f,0.0f);
		}
		else
		{
			_currentLevel.transform.position = new Vector3(0.0f, currentY + Time.deltaTime * 5.0f,0.0f);
		}
	}

	private void LoadNewLevel(int newLevel)
	{
		var levelLocation = _currentLevel.transform.position;
		
		_currentLevel =
			GameObject.Instantiate(Levels[newLevel], levelLocation, Levels[newLevel].transform.rotation) as GameObject;
		
	}
	
	public void LoadLevel(int level,Fortress fortress)
	{
		_level = level;
		_changingLevel = true;
		_sinkingLevel = true;
	}
}
