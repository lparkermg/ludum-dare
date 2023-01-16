using Game.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Field
{
    public class FieldComponent : MonoBehaviour
    {
        public ShardType Type;

        private int _amount = 10;
        private bool _grown = true;
        [Header("Growth Properties")]
        [SerializeField]
        private float _growTime = 10.0f;

        [SerializeField]
        private float _currentGrowTime = 0f;

        private float _growthMultiplier = 1f;

        private int _currentGrowthStage = 3;

        [SerializeField]
        private Transform _plantParent;

        private PlantComponent[] _plants;

        [Header("World Space UI")]
        [SerializeField]
        private Image _typeImage;

        [SerializeField]
        private Image _growthImage;

        [SerializeField]
        private TextMeshProUGUI _growthText;

        // World Space UI Images.
        private Sprite[] _growthStageSprites;

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

        public void InitialiseField(Material typeMaterial, Sprite[] growthStageSprites, Sprite fieldType)
        {
            foreach(var plant in _plants)
            {
                plant.SetupPlant(typeMaterial, _currentGrowthStage);
            }

            _typeImage.sprite = fieldType;
            _growthStageSprites = growthStageSprites;
            UpdateGrowthUi(_currentGrowthStage);
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
                UpdateGrowthUi(_currentGrowthStage);
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

            UpdateGrowthUi(_currentGrowthStage);

            return amount;
        }

        public void Plant()
        {
            _growthMultiplier += 0.25f;
        }

        private void UpdateGrowthUi(int stage)
        {
            _growthImage.sprite = _growthStageSprites[stage];
            _growthText.text = stage == 3 ? "Ready!" : "Growing!";
        }
    }
}
