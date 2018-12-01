using System.Collections;
using System.Collections.Generic;
using Entities;
using Managers;
using UnityEngine;

namespace Interactables
{
    public class FinishObject : ManagedObjectBehaviour
    {
        private GameplayManager _gameplayManager;

        public override void StartMe(GameObject managers)
        {
            _gameplayManager = managers.GetComponent<GameplayManager>();
        }

        public override void UpdateMe(){}

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _gameplayManager.CompleteLevel(true);
            }
        }
    }
}
