using LPSoft.LD46.Enums;
using LPSoft.LD46.Extensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LPSoft.LD46.Entities.UI
{
    public sealed class UISlot : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _slotText;

        [SerializeField]
        private Image _icon;
        // Start is called before the first frame update

        public void Initialize(Element element, int slotNumber)
        {
            _slotText.text = slotNumber.ToString();
            _icon.color = element.ToColor();
        }
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
