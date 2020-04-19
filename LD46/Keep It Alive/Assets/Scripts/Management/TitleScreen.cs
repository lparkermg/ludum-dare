using LPSoft.LD46.Enums;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LPSoft.LD46.Management
{
    public sealed class TitleScreen : MonoBehaviour
    {
        [Header("Difficulty Info")]
        [SerializeField]
        private TextMeshProUGUI _difficulty;

        [SerializeField]
        private TextMeshProUGUI _availableSlots;

        [Header("Slot Elements")]
        [SerializeField]
        private TextMeshProUGUI _slot1;
        private Element? _slot1Element = Element.General;

        [SerializeField]
        private TextMeshProUGUI _slot2;
        private Element? _slot2Element = Element.Fire;

        [SerializeField]
        private TextMeshProUGUI _slot3;
        private Element? _slot3Element;

        [SerializeField]
        private TextMeshProUGUI _slot4;
        private Element? _slot4Element;

        [SerializeField]
        private TextMeshProUGUI _slot5;
        private Element? _slot5Element;

        [Header("Enemy Elements")]
        [SerializeField]
        private GameObject _fireSlot;

        [SerializeField]
        private GameObject _generalSlot;

        [SerializeField]
        private GameObject _waterSlot;

        [SerializeField]
        private GameObject _lightningSlot;

        [SerializeField]
        private GameObject _earthSlot;

        private Element[] _enemyElements;

        private int _enemyElementAmount = 2;

        [SerializeField]
        private TextMeshProUGUI _waves;

        [Header("Slot Buttons")]
        [SerializeField]
        private Button _slot1Button;

        [SerializeField]
        private Button _slot2Button;

        [SerializeField]
        private Button _slot3Button;

        [SerializeField]
        private Button _slot4Button;

        [SerializeField]
        private Button _slot5Button;

        private int _selectedSlot = 1;
        private int _selectedDifficulty = 0;
        private int _wavesExpected = 5;

        private int[] _lockedSlots;


        // Start is called before the first frame update
        void Start()
        {
            // TODO: Change this to load form player prefs?
            _lockedSlots = new int[]
            {
                3,4,5
            };
            GenerateEnemyElements();
            HideLockedSlots();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void HideLockedSlots()
        {
            foreach(var lockedSlot in _lockedSlots)
            {
                switch (lockedSlot)
                {
                    case 3:
                        _slot3.text = "Slot 3: Locked";
                        _slot3Button.gameObject.SetActive(false);
                        break;
                    case 4:
                        _slot4.text = "Slot 4: Locked";
                        _slot4Button.gameObject.SetActive(false);
                        break;
                    case 5:
                        _slot5.text = "Slot 5: Locked";
                        _slot5Button.gameObject.SetActive(false);
                        break;
                    default:
                        break;
                }
            }
        }

        private void GenerateEnemyElements()
        {
            var availableElements = new List<Element>
            {
                Element.General,
                Element.Fire,
                Element.Water,
                Element.Lightning,
                Element.Earth
            };

            var elementLabels = new List<GameObject> {
                _fireSlot,
                _generalSlot,
                _lightningSlot,
                _waterSlot,
                _earthSlot
            };

            foreach(var slot in elementLabels)
            {
                slot.SetActive(false);
            }

            var elements = new List<Element>();
            for(var i = 0; i < _enemyElementAmount; i++)
            {
                var toAdd = Random.Range(0, availableElements.Count);
                var element = availableElements[toAdd];
                elements.Add(element);
                availableElements.RemoveAt(toAdd);

                switch (element)
                {
                    case Element.General:
                        _generalSlot.SetActive(true);
                        break;
                    case Element.Fire:
                        _fireSlot.SetActive(true);
                        break;
                    case Element.Water:
                        _waterSlot.SetActive(true);
                        break;
                    case Element.Lightning:
                        _lightningSlot.SetActive(true);
                        break;
                    case Element.Earth:
                        _earthSlot.SetActive(true);
                        break;
                    default:
                        break;
                }
            }

            _enemyElements = elements.ToArray();
        }

        public void SelectDifficulty(int difficulty)
        {
            _selectedDifficulty = difficulty;
            switch (difficulty)
            {
                case 0:
                    _difficulty.text = "Difficulty: Easy";
                    _wavesExpected = 5;
                    _waves.text = $"Waves: {_wavesExpected}";
                    _enemyElementAmount = 2;
                    break;
                case 1:
                    _difficulty.text = "Difficulty: Medium";
                    _wavesExpected = 7;
                    _waves.text = $"Waves: {_wavesExpected}";
                    _enemyElementAmount = 3;
                    break;
                case 2:
                    _difficulty.text = "Difficulty: Hard";
                    _wavesExpected = 10;
                    _waves.text = $"Waves: {_wavesExpected}";
                    _enemyElementAmount = 4;
                    break;
                default:
                    break;
            }

            GenerateEnemyElements();
        }

        public void SelectBarrierSlot(int slotNumber)
        {
            _selectedSlot = slotNumber;
        }

        public void SelectElementForSlot(int element)
        {
            switch (_selectedSlot)
            {
                case 1:
                    _slot1Element = _slot1Element == (Element)element ? Element.None : (Element)element;
                    _slot1.text = $"Slot 1: {(Element)element}";
                    break;
                case 2:
                    _slot2Element = _slot2Element == (Element)element ? Element.None : (Element)element;
                    _slot2.text = $"Slot 2: {(Element)element}";
                    break;
                case 3:
                    _slot3Element = _slot3Element == (Element)element ? Element.None : (Element)element;
                    _slot3.text = $"Slot 3: {(Element)element}";
                    break;
                case 4:
                    _slot4Element = _slot4Element == (Element)element ? Element.None : (Element)element;
                    _slot4.text = $"Slot 4: {(Element)element}";
                    break;
                case 5:
                    _slot5Element = _slot5Element == (Element)element ? Element.None : (Element)element;
                    _slot5.text = $"Slot 5: {(Element)element}";
                    break;
            }
        }

        public void StartGame()
        {
            GameManager.Initialize(4f);
            GameManager.SetupGame(_wavesExpected, _slot1Element.Value, _slot2Element.Value, _slot3Element, _slot4Element, _slot5Element, _enemyElements);
            SceneManager.LoadScene(1);
        }
    }
}
