using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    //private PlayerMovement _movement; ADD THIS!
    private InputManager _input;
    //TODO: UI Manager maybe?
    private List<Plant> _seedInventory;
    private List<Plant> _flowerInventory;
    private Plant _selectedSeed;
    private int _selectedSeedIndex;

    private GroundManager _selectedGround;
    private Selected _currentlySelected;

	// Use this for initialization
	void Start ()
	{
	    _input = GameObject.FindGameObjectWithTag("Managers").GetComponent<InputManager>();
	    _seedInventory = new List<Plant>();
	    _flowerInventory = new List<Plant>();
	    _selectedSeedIndex = 0;
	    SelectPlant();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    InputCheck();
	}

    private void InputCheck()
    {
        if (_input.DpadLeftButton && !_input.DpadRightButton)
            ChangeSelection(false);
        else if(!_input.DpadLeftButton && _input.DpadRightButton)
            ChangeSelection(true);

        if (_input.ActionButton)
            TryAction(false);
        else if(_input.CancelButton)
            TryAction(true);
    }

    #region Action Stuff

    private void TryAction(bool pickupSeed)
    {
        if (_currentlySelected == Selected.Ground)
        {
            var plant = _selectedGround.TryAction(_selectedSeed,pickupSeed,() =>
                {
                    _seedInventory.Remove(_selectedSeed);
                    if (_selectedSeedIndex > _seedInventory.Count - 1)
                        _selectedSeedIndex = _seedInventory.Count - 1;
                    if (_seedInventory.Count > 0)
                        _selectedSeed = _seedInventory[_selectedSeedIndex];
                    else
                        _selectedSeed = null;
                });

            if (plant != null && !pickupSeed)
            {

                _flowerInventory.Add(plant);
            }
            else if (plant != null && pickupSeed)
            {
                if (_seedInventory.Count == 0)
                    _selectedSeedIndex = 0;
                _seedInventory.Add(plant);
                _selectedSeed = _seedInventory[_selectedSeedIndex];
            }
        }
        else if (_currentlySelected == Selected.Shop)
        {
            //TODO: Sell those plants n00b!
        }
    }
    #endregion

    #region Selection Stuff
    private void SelectPlant()
    {
        if (_seedInventory.Count > 0)
            _selectedSeed = _seedInventory[_selectedSeedIndex];
        else
            _selectedSeed = null;
    }

    private void ChangeSelection(bool up)
    {
        if (up && _selectedSeedIndex >= _seedInventory.Count - 1)
            _selectedSeedIndex = 0;
        else if (up && _selectedSeedIndex < _seedInventory.Count - 1)
            _selectedSeedIndex++;
        else if (!up && _selectedSeedIndex > 0)
            _selectedSeedIndex--;
        else if (!up && _selectedSeedIndex <= 0 && _seedInventory.Count != 0)
            _selectedSeedIndex = _seedInventory.Count - 1;

        SelectPlant();
    }

    public void UpdateCurrentlySelected(Selected selected, GroundManager ground = null)
    {
        _currentlySelected = selected;

        if (ground != null && selected == Selected.Ground)
            _selectedGround = ground;
    }
    #endregion
}
