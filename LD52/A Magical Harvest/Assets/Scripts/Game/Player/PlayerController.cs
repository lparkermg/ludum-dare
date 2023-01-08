using Cinemachine;
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
        [Range(0.1f, 0.5f)]
        private float _mouseSensitivity = 0.1f;

        [SerializeField]
        [Range(1f, 5f)]
        private float _moveSpeed = 2.5f;

        [SerializeField]
        [Range(10f, 20f)]
        private float _multiplier = 10f;

        private CollectorComponent _currentCollector;
        private FieldComponent _currentField;

        private CharacterController _controller;
        private Vector2 _velocity = Vector2.zero;
        private Vector2 _look = Vector2.zero;

        private CinemachineVirtualCamera _camera;

        void Awake()
        {
            _camera = GetComponentInChildren<CinemachineVirtualCamera>();
        }

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
            transform.Rotate(transform.up, _look.x);

            // Correct for deltaTime so your behaviour is framerate independent.
            // (You may need to increase your speed as it's now measured in degrees per second, not per frame)
            float angularIncrement = _multiplier * _look.y * Time.deltaTime;

            // Get the current rotation angles.
            Vector3 eulerAngles = _camera.transform.localEulerAngles;

            // Returned angles are in the range 0...360. Map that back to -180...180 for convenience.
            if (eulerAngles.x > 180f)
                eulerAngles.x -= 360f;

            // Increment the pitch angle, respecting the clamped range.
            eulerAngles.x = Mathf.Clamp(eulerAngles.x - angularIncrement, -35f, 35f);

            // Orient to match the new angles.
            _camera.transform.localEulerAngles = eulerAngles;

            var move = transform.forward;
            move *= _velocity.y;
            move *= _moveSpeed;

            _controller.Move(move * Time.deltaTime);
        }

        public void HandleMove(InputAction.CallbackContext ctx)
        {
            // TODO: Handles moving forward/backward and straffing left/right.
            if (ctx.phase == InputActionPhase.Canceled)
            {
                _velocity = Vector3.zero;
            }

            _velocity = ctx.ReadValue<Vector2>();
        }

        public void HandleLook(InputAction.CallbackContext ctx)
        {
            // TODO: Handles looking around.
            if (ctx.phase == InputActionPhase.Canceled)
            {
                _look = Vector2.zero;
            }

            _look = ctx.ReadValue<Vector2>() * _mouseSensitivity;
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
