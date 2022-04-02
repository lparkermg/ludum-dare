using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Transform Water;

    public float WaterRaiseTime = 5.0f;
    private float _currentRaiseTime = 0.0f;

    public float WaterRaiseAmount = 0.5f;

    private int _waterRaises = 0;
    private int _currentLimit = 0;

    private int _raisesPerLimit = 4;

    private bool _waterPaused = false;
    public float WaterPauseTime = 10.0f;
    private float _waterPauseCurrentTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
        _raisesPerLimit = (int)(2.0f / WaterRaiseAmount) / 3;
        Debug.Log(_raisesPerLimit);
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
    }

    public void PauseWater()
    {
        _waterPaused = true;
    }

    private void LimitHit()
    {
        Debug.Log("Limit Hit!");
        _currentLimit++;

        // Set music to change to next phase.
        
        // Set all monitors to change display or start collapsing?
    }
}
