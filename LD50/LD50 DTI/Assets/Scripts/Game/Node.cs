using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Transform RotatorOne;
    public Transform RotatorTwo;
    public Transform RotatorThree;

    public CinemachineVirtualCamera Camera;

    private Transform[] _selectableRotators;
    private int _currentSelection = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        RotatorOne.SetPositionAndRotation(RotatorOne.position, Quaternion.Euler(0, Random.Range(0f, 359f), 0));
        RotatorTwo.SetPositionAndRotation(RotatorTwo.position, Quaternion.Euler(0, Random.Range(0f, 359f), 0));
        RotatorThree.SetPositionAndRotation(RotatorThree.position, Quaternion.Euler(0, Random.Range(0f, 359f), 0));

        _selectableRotators = new[] { RotatorOne, RotatorTwo, RotatorThree };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivatePuzzle()
    {
        Camera.Priority = 3;
    }

    public void DeactivatePuzzle()
    {
        Camera.Priority = 1;
    }

    public void Up()
    {
        if (_currentSelection == _selectableRotators.Length)
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
        if (_currentSelection == 0)
        {
            _currentSelection = _selectableRotators.Length;
        }
        else
        {
            _currentSelection--;
        }
    }

    public void Rotate(float amount)
    {
        var currentRotation = _selectableRotators[_currentSelection].rotation.eulerAngles;
        _selectableRotators[_currentSelection].SetPositionAndRotation(_selectableRotators[_currentSelection].position, Quaternion.Euler(new Vector3(currentRotation.x, currentRotation.y + amount, currentRotation.z)));
    }
}
