using LPSoft.LD46.Entities.UI;
using LPSoft.LD46.Enums;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LPSoft.LD46.Management
{
    public sealed class UI : MonoBehaviour
    {
        [Header("Slots")]
        [SerializeField]
        private GameObject _slotPrefab;

        [SerializeField]
        private Transform _slotParent;

        [Header("Details")]
        [SerializeField]
        private TextMeshProUGUI _waveText;

        [SerializeField]
        private TextMeshProUGUI _shipHealthText;

        [SerializeField]
        private TextMeshProUGUI _carrierHealthText;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void InitializeSlots(Element[] slots)
        {
            for(var i = 0; i < slots.Length; i++)
            {
                var slotGo = GameObject.Instantiate(_slotPrefab, _slotParent);
                slotGo.GetComponent<UISlot>().Initialize(slots[i], i + 1);
            }
        }

        public void UpdateShipHealth(float health)
        {
            _shipHealthText.text = $"Ship Health: {health:000}";
        }

        public void UpdateCarrierHealth(float health)
        {
            _carrierHealthText.text = $"Carrier Health: {health:000}";
        }

        public void UpdateWave(int wave)
        {
            _waveText.text = $"Wave: {wave:##}";
        }
    }
}
