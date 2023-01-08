using Game.Enums;
using UnityEngine;

namespace Game.Field
{
    public class FieldComponent : MonoBehaviour
    {
        public ShardType Type;

        private int _amount = 10;
        private bool _grown = true;

        [SerializeField]
        private float _growTime = 10.0f;

        [SerializeField]
        private float _currentGrowTime = 0f;

        private float _growthMultiplier = 1f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!_grown)
            {
                if(_currentGrowTime < _growTime)
                {
                    _currentGrowTime += Time.deltaTime * _growthMultiplier;
                }
                else
                {
                    _grown = true;
                    _currentGrowTime = 0f;
                    _growthMultiplier = 1f;
                    _amount = Random.Range(5,11);
                }
            }
        }

        public int Collect()
        {
            _grown = false;
            var amount = _amount;
            _amount = 0;
            return amount;
        }

        public void Plant()
        {
            _growthMultiplier = 0.25f;
        }
    }
}
