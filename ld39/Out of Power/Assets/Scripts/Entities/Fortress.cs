using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Fortress : MonoBehaviour
{
	public float MaxCharge = 10.0f;
	private float _currentCharge = 10.0f;

	public float MovementMultiplier = 25.0f;

	private bool _isFlying = false;
	public bool IsFlying
	{
		get
		{
			return _isFlying;
		}
	}

	private Rigidbody _rigidbody;

	private float _vertAxis;
	private float _horizAxis;
	
	//Input Vars
	private float _deadZone = 0.01f;
	
	//Managers
	private GameManager _gm;
	// Use this for initialization
	void Start ()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_gm = GameObject.FindGameObjectWithTag("Managers").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!_gm.GameOver)
			InputCheck();
		UpdateCharge();
	}

	void FixedUpdate()
	{
		MovementUpdate();
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("ChargePoint"))
		{
			col.gameObject.GetComponent<ChargePoint>().SetFortress(this);
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.CompareTag("ChargePoint"))
		{
			col.gameObject.GetComponent<ChargePoint>().SetFortress(null);
		}
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
		{
			_isFlying = false;
			_gm.ShowGameOver();
		}
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
		var forceDirection = new Vector3(_horizAxis * MovementMultiplier, 0.0f,_vertAxis * MovementMultiplier);
		if (currentY < 3.0f)
		{
			forceDirection = new Vector3(forceDirection.x,100.0f,forceDirection.z);
		}
		
		if (_isFlying)
		{
			_rigidbody.AddForce(forceDirection,ForceMode.Force);
		}
		
		
	}
	#endregion

	public void SetControlability(bool finishing)
	{
		if (finishing)
		{
			_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			_isFlying = false;
			_currentCharge = MaxCharge;
		}
		else
		{
			_rigidbody.constraints = RigidbodyConstraints.None;
			MoveToStartPoint();
		}
	}

	public void MoveToStartPoint()
	{
		var startPoint = GameObject.FindGameObjectWithTag("StartPoint").transform.position;
		Debug.Log(startPoint);
		
		_rigidbody.MovePosition(startPoint);
		
	}
}
