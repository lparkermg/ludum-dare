using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Fortress : MonoBehaviour
{
	public float MaxCharge = 10.0f;
	private float _currentCharge = 10.0f;

	private bool _isFlying = false;
	public bool IsFlying
	{
		get
		{
			return _isFlying;
		}
	}

	private Rigidbody _rigidbody;

	private bool _inLandingArea = false;

	private Vector3 _forceDirection;
	private float _vertAxis;
	private float _horizAxis;
	
	//Input Vars
	private float _deadZone = 0.01f;
	// Use this for initialization
	void Start ()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		InputCheck();
		UpdateCharge();
	}

	void FixedUpdate()
	{
		MovementUpdate();
	}

	void OnTriggerEnter(Collider col)
	{
		//TODO: Detect when entered a charge point or finish area.
	}

	void OnTriggerExit(Collider col)
	{
		//TODO: Detectn when exited a charge point or finish area etc.
	}
	
	#region Fortress Charging
	public void AddCharge(float chargeAmount)
	{
		_currentCharge += chargeAmount;

		if (_currentCharge > MaxCharge)
			_currentCharge = MaxCharge;
	}

	private void UpdateCharge()
	{
		if (_isFlying)
			_currentCharge -= Time.deltaTime; //TODO: Change to a central deltaTime at a later date.

		if (_currentCharge <= 0.0f)
			_isFlying = false;
	}
	#endregion
	
	#region Input and Movement
	private void InputCheck()
	{
		_vertAxis = Input.GetAxis("Vertical");
		_horizAxis = Input.GetAxis("Horizontal");
		
		if (Input.GetButtonDown("Jump"))
		{
			_isFlying = !_isFlying;
		}
	}

	private void MovementUpdate()
	{
		var currentY = transform.position.y;
		_forceDirection = new Vector3(_horizAxis * 50.0f, 0.0f,_vertAxis * 50.0f);
		if (currentY < 3.0f)
		{
			_forceDirection = new Vector3(_forceDirection.x,100.0f,_forceDirection.z);
		}
		
		if (_isFlying)
		{
			_rigidbody.AddForce(_forceDirection,ForceMode.Force);
		}
		
		
	}
	#endregion
}
