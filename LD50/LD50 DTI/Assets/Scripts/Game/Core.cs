using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Core : MonoBehaviour
{
    public Transform Water;

    public UIDocument UI;

    public float WaterRaiseTime = 5.0f;
    private float _currentRaiseTime = 0.0f;

    public float WaterRaiseAmount = 0.5f;

    private int _waterRaises = 0;
    private int _currentLimit = 0;

    private int _raisesPerLimit = 4;

    private bool _waterPaused = false;
    public float WaterPauseTime = 10.0f;
    private float _waterPauseCurrentTime = 0.0f;

    private TimeSpan _inGameTime = new TimeSpan(0,0,0);

    // TODO: Move these to a UI class.
    private VisualElement _timeHolder;
    private VisualElement _gameOverHolder;
    private Label _timeLabel;

    private 

    // Start is called before the first frame update
    void Start()
    {
        _timeHolder = UI.rootVisualElement.Q<VisualElement>("TimeData");
        _gameOverHolder = UI.rootVisualElement.Q<VisualElement>("GameOver");
        _timeLabel = UI.rootVisualElement.Q<Label>("CurrentTime");
        _raisesPerLimit = (int)(2.0f / WaterRaiseAmount) / 3;
        _timeLabel.text = _inGameTime.ToString("G");
    }

    // Update is called once per frame
    void Update()
    {
        if (!_waterPaused)
        {
            if (_currentRaiseTime < WaterRaiseTime)
            {
                _currentRaiseTime += Time.deltaTime;
            }
            else
            {
                _waterRaises++;
                Water.SetPositionAndRotation(new Vector3(Water.position.x, Water.position.y + WaterRaiseAmount, Water.position.z), Water.rotation);
                _currentRaiseTime = 0.0f;
                if (_waterRaises == _raisesPerLimit)
                {
                    _waterRaises = 0;
                    LimitHit();
                }
            }
        }
        else
        {
            if(_waterPauseCurrentTime < WaterPauseTime)
            {
                _waterPauseCurrentTime += Time.deltaTime;
            }
            else
            {
                _waterPauseCurrentTime = 0.0F;
                _waterPaused = false;
            }
        }

        _inGameTime = _inGameTime.Add(TimeSpan.FromSeconds(Time.deltaTime));
        _timeLabel.text = _inGameTime.ToString("mm\\:ss");
    }

    public void PauseWater()
    {
        _waterPaused = true;
    }

    private void LimitHit()
    {
        Debug.Log("Limit Hit!");
        _currentLimit++;

        if (_currentLimit == 1)
        {
            var displayStyle = _timeHolder.style.display;
            displayStyle.value = DisplayStyle.None;
            _timeHolder.style.display = displayStyle;
            _gameOverHolder.style.opacity = 1f;
        }
        // Set music to change to next phase.
        
        // Set all monitors to change display or start collapsing?
    }
}
