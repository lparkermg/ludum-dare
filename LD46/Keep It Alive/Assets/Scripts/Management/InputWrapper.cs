using LPSoft.LD46.Entities;
using LPSoft.LD46.Enums;
using LPSoft.LD46.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LPSoft.LD46.Management
{
    public sealed class InputWrapper : MonoBehaviour
    {
        public Vector2 Direction { get; private set; }

        public bool CanMove => Direction != Vector2.zero;

        public Vector2 MousePosition { get; private set; }

        public delegate void ToggleBarrierEventHandler(object source, BarrierToggleEventArgs args);
        public event ToggleBarrierEventHandler OnToggleBarrier;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            MouseInput();
            KeyboardInput();
        }

        private void KeyboardInput()
        {
            var newDir = Direction;
            newDir.x = Input.GetAxis("Horizontal");
            newDir.y = Input.GetAxis("Vertical");
            Direction = newDir;

            if (Input.GetButtonDown("Barrier Slot 1"))
            {
                Debug.Log("Toggling barrier slot 1.");
                OnToggleBarrier?.Invoke(null, new BarrierToggleEventArgs { Slot = 1 });
            }

            if (Input.GetButtonDown("Barrier Slot 2"))
            {
                Debug.Log("Toggling barrier slot 2.");
                OnToggleBarrier?.Invoke(null, new BarrierToggleEventArgs { Slot = 2 });
            }
        }

        private void MouseInput()
        {
            MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
