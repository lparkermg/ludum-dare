using Newtonsoft.Json.Schema;
using UnityEngine;

public class WaterComponent : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve _waterHeightCurve;

    private float _currentPosition = 0f;
    private float _maxPosition = 1f;

    private bool _goingUp = true;

    [SerializeField]
    [Range(1, 10)]
    private int _speedDivider = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentPosition >= _maxPosition)
        {
            _goingUp = false;
        }
        else if(_currentPosition <= 0f)
        {
            _goingUp = true;
        }

        _currentPosition = _goingUp ? _currentPosition + (Time.deltaTime / _speedDivider) : _currentPosition - (Time.deltaTime / _speedDivider);

        ProcessCurve();
    }

    private void ProcessCurve()
    {
        var pos = transform.position;
        pos.Set(pos.x, Map(0f, 1f, -1f, -0.5f, _waterHeightCurve.Evaluate(_currentPosition)), pos.z);

        transform.SetPositionAndRotation(pos, transform.rotation);
    }

    private float Map(float aLow, float aHigh, float bLow, float bHigh, float value)
    {
        float normal = Mathf.InverseLerp(aLow, aHigh, value);
        return Mathf.Lerp(bLow, bHigh, normal);
    }
}
