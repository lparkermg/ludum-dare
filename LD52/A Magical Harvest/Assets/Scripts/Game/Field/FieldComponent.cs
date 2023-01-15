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

        private int _currentGrowthStage = 3;

        [SerializeField]
        private Transform _plantParent;

        private PlantComponent[] _plants;

        void Awake()
        {
            _plants = _plantParent.GetComponentsInChildren<PlantComponent>();
        }

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

            UpdateGrowth();
        }

        public void InitialiseField(Material typeMaterial)
        {
            foreach(var plant in _plants)
            {
                plant.SetupPlant(typeMaterial, _currentGrowthStage);
            }
        }

        private void UpdateGrowth()
        {
            var amountPerStage = _growTime / 4f;
            var oldGrowth = _currentGrowthStage;

            if (!_grown)
            {
                if (_currentGrowTime >= amountPerStage && _currentGrowthStage == 0)
                {
                    // Stage 1 - Bud
                    _currentGrowthStage++;
                }
                else if (_currentGrowTime >= (amountPerStage * 2f) && _currentGrowthStage == 1)
                {
                    // Stage 2 - Grow 1
                    _currentGrowthStage++;
                }
                else if (_currentGrowTime >= (amountPerStage * 3f) && _currentGrowthStage == 2)
                {
                    // Stage 3 - Grow 2
                    _currentGrowthStage++;
                }
            }

            if (oldGrowth != _currentGrowthStage)
            {
                foreach (var plant in _plants)
                {
                    plant.UpdatePlant(_currentGrowthStage);
                }
            }
        }

        public int Collect()
        {
            _grown = false;
            var amount = _amount;
            _amount = 0;
            _currentGrowthStage = 0;

            foreach (var plant in _plants)
            {
                plant.UpdatePlant(_currentGrowthStage);
            }

            return amount;
        }

        public void Plant()
        {
            _growthMultiplier += 0.25f;
        }
    }
}
