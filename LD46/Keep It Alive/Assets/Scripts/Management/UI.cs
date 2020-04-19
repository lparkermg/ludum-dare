using LPSoft.LD46.Entities.UI;
using LPSoft.LD46.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LPSoft.LD46.Management
{
    public sealed class UI : MonoBehaviour
    {
        [SerializeField]
        private GameObject _slotPrefab;

        [SerializeField]
        private Transform _slotParent;

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
    }
}
