using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class SelectionDetection : MonoBehaviour
{

    public PlayerManager PlayerManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            PlayerManager.UpdateCurrentlySelected(Selected.Ground,other.gameObject.GetComponent<GroundManager>());
        }
    }
}
