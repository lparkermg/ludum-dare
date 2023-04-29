using Cinemachine;
using Game.Collector;
using Game.Entities;
using Game.Field;
using Game.Global;
using Game.Managers;
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
        [Range(10f, 40f)]
        private float _multiplier = 10f;

        [SerializeField]
        private CollectorComponent _currentCollector;

        [SerializeField]
        private FieldComponent _currentField;

        private CharacterController _controller;
        private Vector2 _velocity = Vector2.zero;
        private Vector2 _look = Vector2.zero;

        private CinemachineVirtualCamera _camera;

        [SerializeField]
        private UiManager _uiManager;

        [SerializeField]
        private GameManager _gameManager;

        private bool _inventorySetup = false;

        [SerializeField]
        private AudioSource _stepSource;

        [SerializeField]
        private AudioClip _stepClip;

        private float _currentStepTime = 0.0f;

        [SerializeField]
        private float _stepTime = 0.7f;

        [SerializeField]
        private float _panAmount = 0.25f;

        private bool _isLeft = false;

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
            if (!_inventorySetup)
            {
                if(_uiManager != null)
                {
                    try
                    {
                        UpdateInventoryUi();
                        _inventorySetup = true;
                    }
                    catch 
                    {
                    }
                }
            }
        }

        void FixedUpdate()
        {
            if(_velocity != Vector2.zero)
            {
                UpdateStepTime();
            }
            else
            {
                _currentStepTime = _stepTime - (_stepTime / 3);
            }
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

            var straff = transform.right;
            straff *= _velocity.x;
            straff *= _moveSpeed;

            _controller.Move((move + straff) * Time.deltaTime);
        }

        public void HandleMove(InputAction.CallbackContext ctx)
        {
            // TODO: Handles moving forward/backward and straffing left/right.
            if (ctx.phase == InputActionPhase.Canceled || GameSettings.IsPaused || GameSettings.IsComplete)
            {
                _velocity = Vector3.zero;
            }
            else
            {
                _velocity = ctx.ReadValue<Vector2>();
            }
        }

        public void HandleLook(InputAction.CallbackContext ctx)
        {
            // TODO: Handles looking around.
            if (ctx.phase == InputActionPhase.Canceled || GameSettings.IsPaused || GameSettings.IsComplete)
            {
                _look = Vector2.zero;
            }
            else
            {
                _look = ctx.ReadValue<Vector2>() * _mouseSensitivity;
            }
        }

        public void HandleCollectFromField(InputAction.CallbackContext ctx)
        {
            if (_currentField == null || ctx.phase != InputActionPhase.Canceled || GameSettings.IsPaused || GameSettings.IsComplete)
            {
                return;
            }

            _inventory.First(i => i.Type == _currentField.Type).Amount += _currentField.Collect();
            UpdateInventoryUi();
            Debug.Log($"Collected from {_currentField.Type}");
        }

        public void HandlePutInField(InputAction.CallbackContext ctx)
        {
            if (_currentField == null || ctx.phase != InputActionPhase.Canceled || GameSettings.IsPaused || GameSettings.IsComplete)
            {
                return;
            }

            var slot = _inventory.FirstOrDefault(i => i.Type == _currentField.Type);

            if(slot != null && slot.Amount > 0)
            {
                _currentField.Plant();
                slot.Amount--;
                UpdateInventoryUi();
            }
        }

        public void HandlePutCollect(InputAction.CallbackContext ctx)
        {
            if (_currentCollector == null || ctx.phase != InputActionPhase.Canceled || GameSettings.IsPaused || GameSettings.IsComplete)
            {
                return;
            }

            var slot = _inventory.FirstOrDefault(i => i.Type == _currentCollector.Type);

            if (slot != null && slot.Amount > 0)
            {
                _currentCollector.PutInCollector(slot.Amount);
                _gameManager.AddTime(0.5f * slot.Amount);
                Debug.Log($"Put {slot.Amount} in {_currentCollector.Type}");
                slot.Amount = 0;
                UpdateInventoryUi();
            }
        }

        public void HandlePause(InputAction.CallbackContext ctx)
        {
            if(ctx.phase != InputActionPhase.Canceled || GameSettings.IsPaused || GameSettings.IsComplete)
            {
                return;
            }

            GameSettings.SetPause(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            other.TryGetComponent<CollectorComponent>(out _currentCollector);
            other.TryGetComponent<FieldComponent>(out _currentField);

            if(_currentCollector != null)
            {
                _uiManager.ShowActionUi("RMB to put...");
                Debug.Log($"Entered collector {_currentCollector.Type}");
            }

            if(_currentField != null)
            {
                _uiManager.ShowActionUi("LMB to take and RMB to plant...");
                Debug.Log($"Entered field {_currentField.Type}");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.TryGetComponent<CollectorComponent>(out var _))
            {
                Debug.Log($"Exiting collector {_currentCollector.Type}");
                _currentCollector = null;
                _uiManager.HideActionUi();
            }

            if(other.TryGetComponent<FieldComponent>(out var _))
            {
                Debug.Log($"Exiting field {_currentField.Type}");
                _currentField = null;
                _uiManager.HideActionUi();
            }
        }

        private void UpdateInventoryUi()
        {
            InventorySlotUi[] slots = _inventory.Select(i => new InventorySlotUi
            {
                Image = i.Image,
                Amount = i.Amount,
            }).ToArray();
            _uiManager.RenderInventory(slots);
        }

        private void UpdateStepTime()
        {
            if(_currentStepTime >= _stepTime)
            {
                _isLeft = !_isLeft;
                if (_isLeft)
                {
                    _stepSource.panStereo = -_panAmount;
                }
                else
                {
                    _stepSource.panStereo = _panAmount;
                }

                _stepSource.PlayOneShot(_stepClip);
                _currentStepTime = 0f;
            }
            else
            {
                _currentStepTime += Time.deltaTime;
            }
        }
    }
}
