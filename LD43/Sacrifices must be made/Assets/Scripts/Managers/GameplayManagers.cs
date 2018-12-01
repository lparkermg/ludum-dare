using System.Collections;
using System.Collections.Generic;
using Entities;
using Rewired;
using UnityEngine;

namespace Managers
{
    public class GameplayManagers : ManagedObjectBehaviour
    {
        // input
        private Player _player;
        private Vector2 _currentTilt;
        private float _cameraRotation;

        //external props
        [SerializeField] private Transform _container;
        [SerializeField] private float _tiltMultiplier = 15.0f;
        [SerializeField] private float _tiltTime = 1.0f;

        public override void StartMe(GameObject managers)
        {
            _player = ReInput.players.GetPlayer(0);
        }

        public override void UpdateMe()
        {
            CheckInput();
            UpdateWorld();
        }

        private void CheckInput()
        {
            _currentTilt = _player.GetAxis2D("TiltHorizontal", "TiltVertical");
            _cameraRotation = _player.GetAxis("CameraMove");
        }

        private void UpdateWorld()
        {
            _container.rotation = Quaternion.Lerp(_container.rotation,
                Quaternion.Euler(new Vector3(_currentTilt.x * _tiltMultiplier, 0.0f, _currentTilt.y * _tiltMultiplier)),
                _tiltTime * Time.deltaTime);

        }
    }
}
