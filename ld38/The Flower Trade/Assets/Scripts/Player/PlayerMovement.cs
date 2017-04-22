using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputManager _input;
    private Rigidbody _rb;

    private bool _move;
    private float _lookAngle;

	// Use this for initialization
	void Start ()
	{
	    _input = GameObject.FindGameObjectWithTag("Managers").GetComponent<InputManager>();
	    _rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
	}

    void FixedUpdate()
    {
        if (_move)
        {
            _rb.AddForce(transform.forward,ForceMode.Acceleration);
        }

    }

    private void GetInput()
    {
        var vertical = _input.VerticalMove;
        var horizontal = _input.HorizontalMove;

        if (vertical != 0.0f || horizontal != 0.0f)
        {
            _lookAngle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
            var rotation = Quaternion.Euler(new Vector3(0.0f, _lookAngle, 0.0f));
            transform.rotation = rotation;
            Debug.Log("Look Angle: " + _lookAngle);
            _move = true;
        }
        else
        {
            _rb.velocity = Vector3.zero;
            _move = false;
        }
    }
}
