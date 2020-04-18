using LPSoft.LD46.Enums;
using LPSoft.LD46.Extensions;
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
        private float _energyRemaining;

        [SerializeField]
        private float _passiveReductionRate = 0.5f;


        // Start is called before the first frame update
        void Start()
        {
            Element = Element.General;
        }

        // Update is called once per frame
        void Update()
        {
            PassiveReduction();
        }

        public void Initialize(float maxEnergy)
        {
            _maxEnergy = maxEnergy;
            _energyRemaining = maxEnergy;
        }

        public void Damage(float baseAmount, Element element)
        {
            _energyRemaining -= baseAmount;
        }

        public void Activate(Element element)
        {
            if (_energyRemaining <= 0.0f)
            {
                _energyRemaining = _maxEnergy;
            }

            Element = element;
            var color = Element.ToColor();
            color.a = 0.7f;
            _renderer.color = color;
            gameObject.SetActive(true);
            
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        private void PassiveReduction()
        {
            _energyRemaining -= Time.deltaTime * _passiveReductionRate;

            if (_energyRemaining <= 0.0f)
            {
                Deactivate();
            }
        }
    }
}
