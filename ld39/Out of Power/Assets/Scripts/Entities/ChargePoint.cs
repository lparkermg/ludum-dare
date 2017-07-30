using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargePoint : MonoBehaviour
{
	public float ChargeAmount = 10.0f;
	public float ChargeBandwidth = 1.2f;

	private ParticleSystem _particles;

	private Fortress _fortress;
	// Use this for initialization
	void Start ()
	{
		_particles = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		ChargeCheck();
	}

	private void ChargeCheck()
	{
		if (_fortress == null)
			return;

		if (ChargeAmount > 0.0f)
		{
			_fortress.AddCharge(ChargeAmount > ChargeBandwidth ? ChargeBandwidth : ChargeAmount);
			ChargeAmount -= ChargeBandwidth;
		}

		if (ChargeAmount < 0.0f)
		{
			ChargeAmount = 0.0f;
			_particles.Stop();
		}
	}

	public void SetFortress(Fortress fortress)
	{
		_fortress = fortress;
	}
}
