using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMotion : MonoBehaviour
{

    private bool _up;

    private Vector3 _top = new Vector3(5.0f,-0.30f,5.0f);
    private Vector3 _bottom = new Vector3(5.0f,-0.70f,5.0f);

    private float _maxTime = 5.0f;
    private float _minTime = 2.0f;

    private float _selectedTime = 0.5f;

    private float _currentTime = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		TimeCheck();
	    MotionWater();
	}

    private void TimeCheck()
    {

        if (_currentTime >= _selectedTime)
        {
            _up = !_up;
            _selectedTime = Random.Range(_minTime, _maxTime);
            _currentTime = 0.0f;
        }
        else
        {
            _currentTime += Time.deltaTime;
        }
    }

    private void MotionWater()
    {
        var delta = Time.deltaTime;
        if (_up)
        {
            transform.position = Vector3.Slerp(transform.position, _top, 0.25f * delta);
        }
        else
        {
            transform.position = Vector3.Slerp(transform.position, _bottom, 0.25f * delta);
        }
    }
}
