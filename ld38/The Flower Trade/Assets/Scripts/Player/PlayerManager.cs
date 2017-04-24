using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    //private PlayerMovement _movement; ADD THIS!
    private InputManager _input;
    private UiManager _ui;
    private List<Plant> _seedInventory;
    private List<Plant> _flowerInventory;
    private Plant _selectedSeed;
    private int _selectedSeedIndex;

    private GroundManager _selectedGround;
    private ShopManager _selectedShop;
    private Selected _currentlySelected;

    private float _coinCount;

    private bool _confirmingSale = false;

    private bool _started = false;

	// Use this for initialization
	void Start ()
	{
	    var managers = GameObject.FindGameObjectWithTag("Managers");
	    _input = managers.GetComponent<InputManager>();
	    _ui = managers.GetComponent<UiManager>();
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
        if (_started)
        {
            if (_input.DpadLeftButton && !_input.DpadRightButton)
                ChangeSelection(false);
            else if (!_input.DpadLeftButton && _input.DpadRightButton)
                ChangeSelection(true);

            if (_input.ActionButton)
                TryAction(false);
            else if (_input.CancelButton)
                TryAction(true);

            if(_input.ExitButton)
                Application.Quit();
        }
        else
        {
            if (_input.ExitButton)
            {
                _ui.HideStartInfo();
                _started = true;
            }
        }
    }

    #region Action Stuff

    private void TryAction(bool pickupSeed)
    {
        if (_currentlySelected == Selected.Ground && !_selectedGround.GroundDisabled)
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

                    _ui.UpdateSeedCount(_seedInventory.Count);
                });

            if (plant != null && !pickupSeed)
            {

                _flowerInventory.Add(plant);
                _ui.UpdateFlowerCount(_flowerInventory.Count);
            }
            else if (plant != null && pickupSeed)
            {
                if (_seedInventory.Count == 0)
                    _selectedSeedIndex = 0;
                _seedInventory.Add(plant);
                _selectedSeed = _seedInventory[_selectedSeedIndex];
                _ui.UpdateSeedCount(_seedInventory.Count);
            }
        }
        else if (_currentlySelected == Selected.ShopSell)
        {
            TryShopAction(true);

            //TODO: Sell those plants n00b!

        }
        else if (_currentlySelected == Selected.ShopBuy)
        {
            TryShopAction(false);
            //TODO:Buy some seeds here.
        }
    }

    private void TryShopAction(bool selling)
    {
        if (selling)
        {
            if (_flowerInventory.Count > 0)
            {
                _selectedShop.OpenSellTransaction(_flowerInventory);
                var coins = _selectedShop.CloseSellTransaction(true, () =>
                {
                    _flowerInventory = new List<Plant>();
                });
                _coinCount += coins;
                _ui.UpdateCoinCount(_coinCount);
                _ui.UpdateFlowerCount(_flowerInventory.Count);
                var message = "You sold all your flowers for " + _coinCount.ToString("#.##") + " coins.";
                _ui.ShowBannerForX(false, message, 2.5f);
            }
            else
            {
                _ui.ShowBannerForX(true, "You have no flowers to sell...", 2.5f);
            }

            _ui.UpdateCoinCount(_coinCount);
            _ui.UpdateFlowerCount(_flowerInventory.Count);
            _ui.UpdateSeedCount(_seedInventory.Count);
        }
        else
        {
            var basket = new List<Plant>();
            var plant = _selectedShop.PickFromInventory();
            if (plant == null)
            {
                _ui.ShowBannerForX(true,"There are currently no seeds available to buy...", 2.5f);
                return;
            }
            basket.Add(plant);
            var cost = _selectedShop.OpenBuyTransaction(basket);
            if (cost > _coinCount)
            {
                _ui.ShowBannerForX(true, "You don't have enough coins.", 2.5f);
                _selectedShop.CancelBuyTransaction();
            }
            else
            {
                _seedInventory.AddRange(_selectedShop.CloseBuyTransaction(true, cost, () =>
                {
                    _coinCount -= cost;
                    _ui.UpdateCoinCount(_coinCount);
                    var message = "You brought a " + plant.Name + " seed for " + cost.ToString("#.##") + " coins.";
                    _ui.ShowBannerForX(false, message, 2.5f);
                }));
            }
            _ui.UpdateCoinCount(_coinCount);
            _ui.UpdateFlowerCount(_flowerInventory.Count);
            _ui.UpdateSeedCount(_seedInventory.Count);
            _selectedSeedIndex = _seedInventory.Count - 1;
            SelectPlant();
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

    public void UpdateCurrentlySelected(Selected selected, GroundManager ground = null,ShopManager shop = null)
    {
        _currentlySelected = selected;

        if (ground != null && selected == Selected.Ground)
            _selectedGround = ground;
        else if (shop != null && (selected == Selected.ShopSell || selected == Selected.ShopBuy))
            _selectedShop = shop;
    }
    #endregion
}
