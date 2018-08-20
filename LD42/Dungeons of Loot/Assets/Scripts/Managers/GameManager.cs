using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ManagedObjectBehaviour[] _currentBehaviours;
    private bool _loadingObjects = true;

	// Use this for initialization
	void Start ()
	{
	    LoadObjects();
	}
	
	// Update is called once per frame
	void Update () {
	    if (!_loadingObjects)
	    {
	        RunUpdate();
	    }
	}

    #region Central Start/Updates
    public void LoadObjects()
    {
        _loadingObjects = true;

        _currentBehaviours = FindObjectsOfType<ManagedObjectBehaviour>();

        for (var i = 0; i < _currentBehaviours.Length; i++)
        {
            _currentBehaviours[i].StartMe(gameObject);
        }

        _loadingObjects = false;
    }

    public void AddObjects(List<ManagedObjectBehaviour> obj, bool runStart = false)
    {
        var behaviours = _currentBehaviours.ToList();
        if (runStart)
        {
            for (var i = 0; i < obj.Count; i++)
            {
                obj[i].StartMe(gameObject);
            }
        }

        behaviours.AddRange(obj);
        _currentBehaviours = behaviours.ToArray();
    }

    private void RunUpdate()
    {
        for (var i = 0; i < _currentBehaviours.Length; i++)
        {
            _currentBehaviours[i].UpdateMe();
        }
    }
    #endregion
}