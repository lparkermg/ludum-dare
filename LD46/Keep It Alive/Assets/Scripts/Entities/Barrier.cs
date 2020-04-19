using LPSoft.LD46.Enums;
using LPSoft.LD46.Extensions;
using LPSoft.LD46.Management;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LPSoft.LD46.Entities
{
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class Barrier : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _renderer;

        public Element Element { get; private set; }
        private float _maxEnergy;

        [SerializeField]
        private float _energyRemaining;

        [SerializeField]
        private float _passiveReductionRate = 0.5f;

        private float _timeout = 5.0f;
        private bool _canActivate = true;
        private float _timeToActivatable = 0.0f;

        private float _barrierRechageAmount = 0.25f;
        private float _barrierRechargeEvery = 1.0f;
        private float _barrierCurrentRechargeTime = 0.0f;

        private bool _active = false;

        // Start is called before the first frame update
        void Start()
        {
            Element = Element.General;
        }

        // Update is called once per frame
        void Update()
        {
            PassiveReduction();
            PassiveRechage();
            Timeout();
        }

        public void Initialize(float maxEnergy)
        {
            _maxEnergy = maxEnergy;
            _energyRemaining = maxEnergy;
        }

        public void Damage(float baseAmount, Element element)
        {
            var damage = Element.Compare(element, baseAmount, GameManager.DamageMultiplier);
            _energyRemaining -= baseAmount;

            if (_energyRemaining > _maxEnergy)
            {
                _energyRemaining = _maxEnergy;
            }
        }

        public void Activate(Element element)
        {
            if (!_canActivate)
            {
                return;
            }

            if (_energyRemaining <= 0.0f)
            {
                _energyRemaining = _maxEnergy;
            }

            Element = element;
            var color = Element.ToColor();
            color.a = 0.7f;
            _renderer.color = color;
            _active = true;
        }

        public void Deactivate()
        {
            var color = _renderer.color;
            color.a = 0.0f;
            _renderer.color = color;
            _active = false;
        }

        private void PassiveReduction()
        {
            if (_active)
            {
                _energyRemaining -= Time.deltaTime * _passiveReductionRate;

                if (_energyRemaining <= 0.0f)
                {
                    _canActivate = false;
                    Deactivate();
                }
            }
        }

        private void PassiveRechage()
        {
            if (!_active)
            {
                if(_barrierCurrentRechargeTime >= _barrierRechargeEvery)
                {
                    _energyRemaining += _barrierRechageAmount;
                    if(_energyRemaining >= _maxEnergy)
                    {
                        _energyRemaining = _maxEnergy;
                    }
                    _barrierCurrentRechargeTime = 0.0f;
                }
                else
                {
                    _barrierCurrentRechargeTime += Time.deltaTime;
                }
            }
        }

        private void Timeout()
        {
            if (!_canActivate)
            {
                if (_timeToActivatable >= _timeout)
                {
                    _canActivate = true;
                    _timeToActivatable = 0.0f;
                }
                else
                {
                    _timeToActivatable += Time.deltaTime;
                }
            }
        }
    }
}
