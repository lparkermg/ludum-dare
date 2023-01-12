using Game.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Managers
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField]
        private UIDocument _ui;

        [SerializeField]
        private VisualTreeAsset _inventoryIconTemplate;


        private VisualElement _inventoryHolder;
        private Label _timeLabel;

        void Awake()
        {
            _inventoryHolder = _ui.rootVisualElement.Q("InventoryContainer");
            _timeLabel = _ui.rootVisualElement.Q<Label>("TimeLeftLabel");
        }

        // Start is called before the first frame update
        void Start()
        {
            _inventoryHolder = _ui.rootVisualElement.Q("InventoryContainer");
            _timeLabel = _ui.rootVisualElement.Q<Label>("TimeLeftLabel");
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void RenderInventory(InventorySlotUi[] slots)
        {
            _inventoryHolder.Clear();

            foreach(var slot in slots)
            {
                var template = _inventoryIconTemplate.Instantiate();

                var icon = template.Q("DisplayIcon");
                var amountLabel = template.Q<Label>("AmountLabel");

                icon.style.backgroundImage = new StyleBackground(slot.Image);
                amountLabel.text = slot.Amount.ToString();

                _inventoryHolder.Add(template);
            }
        }

        public void UpdateTimeDisplay(string time)
        {
            _timeLabel.text = time;
        }
    }
}
