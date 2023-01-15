using Game.Collector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Managers
{
    public class MarketManager : MonoBehaviour
    {
        [SerializeField]
        private Transform _collectorsParent;

        private CollectorComponent[] _collectors;

        [SerializeField]
        private float _updateFrequency = 15.0f;

        private float _currentTime = 0f;
        // Start is called before the first frame update
        void Start()
        {
            _collectors = _collectorsParent.GetComponentsInChildren<CollectorComponent>();
            UpdateMarket();

        }

        // Update is called once per frame
        void Update()
        {
            if(_currentTime < _updateFrequency)
            {
                _currentTime += Time.deltaTime;
            }
            else
            {
                UpdateMarket();
                _currentTime = 0f;
            }
        }

        private void UpdateMarket()
        {
            foreach(var collector in _collectors)
            {
                collector.SetTimePerShard(Random.Range(0.1f, 0.5f));
            }
        }
    }
}
