using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Node : MonoBehaviour
{
    public Transform RotatorOne;
    public Transform RotatorTwo;
    public Transform RotatorThree;

    public CinemachineVirtualCamera Camera;

    private Transform[] _selectableRotators;
    private bool[] _completedRotators;
    private int _currentSelection = 0;

    // The guide as to where the other nodes have to be.
    public Transform Guide;

    private bool _canChangeSelected = true;
    private float _currentTime = 0.0f;
    public float ChangeWaitTime = 0.5f;

    private bool _nodeComplete = false;

    private Action _onComplete;
    
    // Start is called before the first frame update
    void Start()
    {
        Guide.SetPositionAndRotation(Guide.position, Quaternion.Euler(0, Random.Range(0f, 359f), 0));
        RotatorOne.SetPositionAndRotation(RotatorOne.position, Quaternion.Euler(0, Random.Range(0f, 359f), 0));
        RotatorTwo.SetPositionAndRotation(RotatorTwo.position, Quaternion.Euler(0, Random.Range(0f, 359f), 0));
        RotatorThree.SetPositionAndRotation(RotatorThree.position, Quaternion.Euler(0, Random.Range(0f, 359f), 0));

        _selectableRotators = new[] { RotatorOne, RotatorTwo, RotatorThree };
        _completedRotators = new[] { false, false, false };
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canChangeSelected)
        {
            if(_currentTime < ChangeWaitTime)
            {
                _currentTime += Time.deltaTime;
            }
            else
            {
                _canChangeSelected = true;
                _currentTime = 0.0f;
            }
        }

        if (_nodeComplete)
        {
            DeactivatePuzzle();
            _onComplete.Invoke();
        }
    }

    public bool ActivatePuzzle(Action onComplete)
    {
        if (_nodeComplete)
        {
            return false;
        }

        _onComplete = onComplete;
        Camera.Priority = 3;
        return true;
    }

    public void DeactivatePuzzle()
    {
        Camera.Priority = 1;
    }

    public void Up()
    {
        if (!_canChangeSelected)
        {
            return;
        }

        if (_currentSelection == _selectableRotators.Length - 1)
        {
            _currentSelection = 0;
        }
        else
        {
            _currentSelection++;
        }
    }

    public void Down()
    {
        if (!_canChangeSelected)
        {
            return;
        }

        if (_currentSelection == 0)
        {
            _currentSelection = _selectableRotators.Length - 1;
        }
        else
        {
            _currentSelection--;
        }
    }

    public void Rotate(float amount)
    {
        if (_completedRotators[_currentSelection])
        {
            return;
        }

        if(_currentSelection > _selectableRotators.Length - 1)
        {
            return;
        }

        var currentRotation = _selectableRotators[_currentSelection].rotation.eulerAngles;
        var newRotation = new Vector3(currentRotation.x, currentRotation.y + amount, currentRotation.z);
        _selectableRotators[_currentSelection].SetPositionAndRotation(_selectableRotators[_currentSelection].position, Quaternion.Euler(newRotation));

        if(newRotation.y <= Guide.rotation.eulerAngles.y + 2.5f && newRotation.y >= Guide.rotation.eulerAngles.y - 2.5f)
        {
            _completedRotators[_currentSelection] = true;

            _nodeComplete = _completedRotators.All(r => r);
        }
    }
}
