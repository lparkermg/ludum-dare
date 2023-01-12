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

        void Awake()
        {
            _uiManager = GetComponent<UiManager>();
            _currentTime = _startTimeDefault;
        }
        // Start is called before the first frame update
        void Start()
        {
            _startTime = DateTime.Now;
        }

        // Update is called once per frame
        void Update()
        {
            if (_currentTime <= 0)
            {
                // TODO: Stop the game and display the length of time played.
                var timePlayed = DateTime.Now - _startTime;
            }
            else
            {
                _currentTime -= Time.deltaTime;
                _uiManager.UpdateTimeDisplay(_currentTime.ToString("##"));
            }
        }

        public void AddTime(float amount)
        {
            _currentTime += amount;
        }
    }
}
