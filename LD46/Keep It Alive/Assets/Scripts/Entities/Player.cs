using LPSoft.LD46.Enums;
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
        private Gameplay _gameplay;
        private Rigidbody2D _rb;

        private Element[] Slots = new Element[2] { Element.General, Element.Fire };

        [SerializeField]
        private float _speed = 0.0f;

        private Carrier _selectedCarrier;

        private bool _barrierActive = false;

        private float _health = 100.0f;

        [SerializeField]
        private Transform _reflector;

        private bool _reflectorActive = false;

        private void Awake()
        {
            var manager = GameObject.FindGameObjectWithTag("Managers");
            manager.TryGetComponent(out _input);
            manager.TryGetComponent(out _gameplay);

            _input.OnToggleBarrier += ToggleSelectedCarrierBarrier;
            _input.OnToggleReflector += ToggleReflector;

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

            if (_reflectorActive)
            {
                MoveReflector();
            }
        }

        public void Damage(float amount)
        {
            _health -= amount;

            if(_health <= 0.0f)
            {
                _gameplay.Gameover();
            }
        }

        private void MovePlayer()
        {
            if (_input.CanMove)
            {
                _rb.AddForce(_input.Direction * _speed);
            }
        }

        private void MoveReflector()
        {
            float angle = 0.0f;

            Vector3 relative = _reflector.InverseTransformPoint(_input.MousePosition.ToVector3());
            angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
            _reflector.Rotate(0f, 0f, -angle);
        }

        private void ToggleSelectedCarrierBarrier(object source, BarrierToggleEventArgs args)
        {
            _barrierActive = !_barrierActive;
            
            if (_barrierActive)
            {
                _selectedCarrier.ActivateBarrier(Slots[args.Slot - 1]);
            }
            else
            {
                _selectedCarrier.DeactivateBarrier();
            }
        }

        private void ToggleReflector(object source, EventArgs args)
        {
            _reflectorActive = !_reflectorActive;
            
            if (_reflectorActive)
            {
                _reflector.gameObject.SetActive(true);
            }
            else
            {
                _reflector.gameObject.SetActive(false);
            }
        }
    }
}
