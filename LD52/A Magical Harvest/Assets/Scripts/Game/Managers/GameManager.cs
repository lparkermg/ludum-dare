using Game.Collector;
using Game.Field;
using Game.Global;
using System;
using UnityEngine;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private float _startTimeDefault = 60.0f;

        private float _currentTime = 0f;

        private DateTime _startTime;

        private UiManager _uiManager;

        private GraphicsManager _graphics;

        [SerializeField]
        private Transform _fieldParent;

        private FieldComponent[] _fields;

        [SerializeField]
        private Transform _collectorParent;

        private CollectorComponent[] _collectors;

        void Awake()
        {
            _uiManager = GetComponent<UiManager>();
            _graphics = GetComponent<GraphicsManager>();
            _currentTime = _startTimeDefault;
            GameSettings.SetPause(false);
        }

        // Start is called before the first frame update
        void Start()
        {
            _startTime = DateTime.Now;
            _fields = _fieldParent.GetComponentsInChildren<FieldComponent>();
            foreach(var field in _fields)
            {
                field.InitialiseField(_graphics.GetMaterial(field.Type), _graphics.GrowthStages(), _graphics.GetSprite(field.Type));
            }

            _collectors = _collectorParent.GetComponentsInChildren<CollectorComponent>();
            foreach(var collector in _collectors)
            {
                collector.InitialiseCollector(_graphics.GetSprite(collector.Type));
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!GameSettings.IsPaused && !GameSettings.IsComplete)
            {
                if (_currentTime <= 0)
                {
                    _uiManager.SetCompletedTime(DateTime.Now - _startTime);
                    GameSettings.SetComplete(true);
                }
                else
                {
                    _currentTime -= Time.deltaTime;
                    _uiManager.UpdateTimeDisplay(_currentTime.ToString("##"));
                }
            }
        }

        public void AddTime(float amount)
        {
            _currentTime += amount;
        }
    }
}
