using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Entities
{
    public class PlayerHolder : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _playerCam;

        public void SetAsViewingCamera()
        {
            _playerCam.Priority = 100;
        }
    }
}
