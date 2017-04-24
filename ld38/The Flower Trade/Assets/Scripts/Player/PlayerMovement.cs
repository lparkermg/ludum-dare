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

    public float MovementSpeed = 1;

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
            _rb.velocity = transform.forward * MovementSpeed;
        }
        else
        {
            _rb.MovePosition(new Vector3(transform.position.x,0.6f,transform.position.z));
            _rb.velocity = Vector3.zero;
        }
    }

    private void GetInput()
    {
        var vertical = _input.VerticalMove;
        var horizontal = _input.HorizontalMove;
        var deadZone = _input.DeadZone;

        if ((vertical >= deadZone || vertical <= -deadZone) || (horizontal >= deadZone || horizontal <= -deadZone))
        {
            _lookAngle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
            var rotation = Quaternion.Euler(new Vector3(0.0f, _lookAngle, 0.0f));
            transform.rotation = rotation;
            _move = true;
        }
        else
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            _move = false;
        }
    }
}
