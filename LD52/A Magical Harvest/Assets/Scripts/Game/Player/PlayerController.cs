using Game.Collector;
using Game.Entities;
using Game.Field;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private List<InventorySlot> _inventory;

        [SerializeField]
        [Range(0.1f, 1f)]
        private float _mouseSensitivity = 0.5f;

        [SerializeField]
        [Range(1f, 5f)]
        private float _moveSpeed = 2.5f;

        [SerializeField]
        [Range(50f, 100f)]
        private float _multiplier = 10f;

        private CollectorComponent _currentCollector;
        private FieldComponent _currentField;

        private CharacterController _controller;
        private Vector3 _velocity = Vector3.zero;

        // Start is called before the first frame updated
        void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void FixedUpdate()
        {
            _controller.SimpleMove(_moveSpeed * Time.deltaTime * (_velocity * _multiplier));
        }

        public void HandleMove(InputAction.CallbackContext ctx)
        {
            // TODO: Handles moving forward/backward and straffing left/right.
            if (ctx.phase == InputActionPhase.Canceled)
            {
                _velocity = Vector3.zero;
            }
            var move = ctx.ReadValue<Vector2>();
            _velocity = new Vector3(move.x, 0f, move.y);
        }

        public void HandleLook(InputAction.CallbackContext ctx)
        {
            // TODO: Handles looking around.
        }

        public void HandleCollectFromField(InputAction.CallbackContext ctx)
        {
            if (_currentField == null || ctx.phase != InputActionPhase.Canceled)
            {
                return;
            }

            _inventory.First(i => i.Type == _currentField.Type).Amount += _currentField.Collect();
        }

        public void HandlePutInField(InputAction.CallbackContext ctx)
        {
            if (_currentField == null || ctx.phase != InputActionPhase.Canceled)
            {
                return;
            }

            var slot = _inventory.FirstOrDefault(i => i.Type == _currentField.Type);

            if(slot != null && slot.Amount > 0)
            {
                _currentField.Plant();
                slot.Amount--;
            }
        }

        public void HandlePutCollect(InputAction.CallbackContext ctx)
        {
            if (_currentCollector == null || ctx.phase != InputActionPhase.Canceled)
            {
                return;
            }

            var slot = _inventory.FirstOrDefault(i => i.Type == _currentCollector.Type);

            if (slot != null)
            {
                _currentCollector.PutInCollector(slot.Amount);
                slot.Amount = 0;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            other.TryGetComponent<CollectorComponent>(out _currentCollector);
            other.TryGetComponent<FieldComponent>(out _currentField);
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.TryGetComponent<CollectorComponent>(out var _))
            {
                _currentCollector = null;
            }

            if(other.TryGetComponent<FieldComponent>(out var _))
            {
                _currentField = null;
            }
        }
    }
}
