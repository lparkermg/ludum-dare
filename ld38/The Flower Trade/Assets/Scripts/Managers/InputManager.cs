using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public float VerticalMove { get; private set; }
    public float HorizontalMove { get; private set; }

    public float DeadZone = 0.1f;
    //Start/Select
    public bool StartButton { get; private set; }
    public bool SelectButton { get; private set; }

    //Action Buttons
    public bool ActionButton { get; private set; }
    public bool CancelButton { get; private set; }

    //DPad
    public bool DpadUpButton { get; private set; }
    public bool DpadLeftButton { get; private set; }
    public bool DpadDownButton { get; private set; }
    public bool DpadRightButton { get; private set; }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateInputs();
	}

    private void UpdateInputs()
    {
        VerticalMove = Input.GetAxis("Vertical");
        HorizontalMove = Input.GetAxis("Horizontal");
        StartButton = Input.GetButtonDown("Start");
        SelectButton = Input.GetButtonDown("Select");
        ActionButton = Input.GetButtonDown("Action");
        CancelButton = Input.GetButtonDown("Cancel");
        DpadUpButton = Input.GetButtonDown("DPadUp");
        DpadLeftButton = Input.GetButtonDown("DPadLeft");
        DpadDownButton = Input.GetButtonDown("DPadDown");
        DpadRightButton = Input.GetButtonDown("DPadRight");
    }
}
