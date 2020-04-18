using LPSoft.LD46.Entities;
using LPSoft.LD46.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LPSoft.LD46.Management
{
    public class InputWrapper : MonoBehaviour
    {
        public Vector2 Direction { get; private set; }

        public bool CanMove => Direction != Vector2.zero;

        public Vector2 MousePosition { get; private set; }

        public delegate void OnClickEventHandler(object source, OnClickEventArgs args);
        public event OnClickEventHandler OnClick;

        // Start is called before the first frame update
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
        }

        private void MouseInput()
        {
            MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                OnClick?.Invoke(null, new OnClickEventArgs { Button = MouseButton.Left });
            }

            if (Input.GetMouseButtonDown(1))
            {
                OnClick?.Invoke(null, new OnClickEventArgs { Button = MouseButton.Right });
            }
        }
    }
}
