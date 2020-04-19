using LPSoft.LD46.Enums;
using LPSoft.LD46.Management;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LPSoft.LD46.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Carrier : MonoBehaviour
    {
        [SerializeField]
        private float _movementArea = 1.0f;

        private Vector2 _centralPoint;
        private Vector2 _pointMovingTo;
        private Vector2 _oldPoint;

        private float _timeSpeed = 0.01f;
        private float _currentTime = 1.0f;

        private Rigidbody2D _rb;

        [SerializeField]
        private float _health = 100.0f;

        public bool BarrierActive { get; private set; }

        [SerializeField]
        private Barrier _barrier;

        [SerializeField]
        private float _barrierMaxEnergy = 25f;

        private Gameplay _gameplay;
        private Management.UI _ui;
        private void Awake()
        {
            var managers = GameObject.FindGameObjectWithTag("Managers");
            managers.TryGetComponent(out _gameplay);
            managers.TryGetComponent(out _ui);

            TryGetComponent(out _rb);
            _centralPoint = transform.position;

            _barrier.Initialize(_barrierMaxEnergy);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }

        public void Damage(float amount,Element element)
        {
            if (!BarrierActive)
            {

                _health -= amount;
                _ui.UpdateCarrierHealth(_health);
                if (_health <= 0.0f)
                {
                    _gameplay.Gameover();
                }
            }
            else
            {
                _barrier.Damage(amount, element);
            }
        }

        public void ActivateBarrier(Element element)
        {
            BarrierActive = true;
            _barrier.Activate(element);
        }

        public void DeactivateBarrier()
        {
            BarrierActive = false;
            _barrier.Deactivate();
        }

        private void Move() 
        {
            _currentTime += _timeSpeed;
            if(_currentTime < 1.0f)
            {
                _rb.MovePosition(Vector2.Lerp(_oldPoint, _pointMovingTo, _currentTime));
            }
            else
            {
                _oldPoint = _pointMovingTo;
                _pointMovingTo = (_centralPoint + Random.insideUnitCircle) * _movementArea;
                _currentTime = 0.0f;
                _timeSpeed = Random.Range(0.001f, 0.005f);
            }
        }
    }
}
