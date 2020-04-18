using LPSoft.LD46.Extensions;
using LPSoft.LD46.Management;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LPSoft.LD46.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Player : MonoBehaviour
    {
        private InputWrapper _input;
        private Rigidbody2D _rb;

        [SerializeField]
        private float _speed = 0.0f;

        private Carrier _selectedCarrier;

        private bool _barrierActive = false;

        private void Awake()
        {
            var manager = GameObject.FindGameObjectWithTag("Managers");
            manager.TryGetComponent(out _input);
            TryGetComponent(out _rb);

            var carrier = GameObject.FindGameObjectWithTag("Carrier");
            carrier.TryGetComponent(out _selectedCarrier);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            if (_input.CanMove)
            {
                _rb.AddForce(_input.Direction * _speed);
            }
        }

        private void ToggleSelectedCarrierBarrier(object source, EventArgs args)
        {
            _barrierActive = !_barrierActive;
            Debug.Log($"Barrier Active: {_barrierActive}");
            _selectedCarrier.BarrierActive = _barrierActive;
        }
    }
}
