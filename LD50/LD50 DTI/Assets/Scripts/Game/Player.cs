using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public Core Core;


    private CharacterController _controller;

    [SerializeField]
    private float _moveSpeed = 1.0f;
    private Vector2 _move = new Vector2(0.0f, 0.0f);

    [SerializeField]
    private float _lookSpeed = 1.0f;
    private Vector2 _look = new Vector2(0.0f, 0.0f);

    private bool _usingNode = false;
    private bool _canActivateNode = false;

    private Node _currentNode = null;
    void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_usingNode)
        {
            transform.Rotate(0, _look.x * _lookSpeed, 0);
            _controller.Move(transform.forward * (_move.y * _moveSpeed));
            _controller.Move(transform.right * (_move.x * _moveSpeed));
        }
        else
        {
            if(_move.y > 0.5f)
            {
                _currentNode.Down();
            }
            else if(_move.y < -0.5f)
            {
                _currentNode.Up();
            }

            _currentNode.Rotate(_move.x * (_moveSpeed * 3));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Node"))
        {
            _canActivateNode = true;
            _currentNode = other.GetComponent<Node>();
        }    
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Node"))
        {
            _canActivateNode = false;
            _currentNode = null;
        }   
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _look = context.ReadValue<Vector2>();
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if (_canActivateNode && !_usingNode && context.ReadValueAsButton())
        {
            // Activate Node Usage
            _usingNode = _currentNode.ActivatePuzzle(() => { 
                _usingNode = false;
                _currentNode.SetNotActive();
                Core.PauseWater();
            });
        }
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (_canActivateNode && _usingNode && context.ReadValueAsButton())
        {
            // TODO: We need to have a cancel action.
            _currentNode.DeactivatePuzzle();
            _usingNode = false;
        }
    }
}
