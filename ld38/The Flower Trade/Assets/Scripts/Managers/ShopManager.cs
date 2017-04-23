using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShopManager : MonoBehaviour
{
    private ObjectManager _objectManager;

    public List<Plant> SeedsForSale { get; private set; }
    public List<Plant> SeedBasket { get; private set; }

    public bool SellingSeeds = false;

    private float _currentOpenTransactionAmount = 0.00f;
    private bool _transactionOpen = false;

    private float _newSeedMax = 120.0f;
    private float _newSeedMin = 60.0f;
    private float _newSeedTime = 10.0f;
    private float _currentTime = 0.0f;
	// Use this for initialization
	void Start ()
	{
	    SeedsForSale = new List<Plant>();
	    _objectManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<ObjectManager>();
	    _newSeedTime = Random.Range(_newSeedMin, _newSeedMax);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    CheckTiming();
	}

    private void CheckTiming()
    {
        if (_currentTime >= _newSeedTime)
        {
            AddSeed();
            _currentTime = 0.0f;
            _newSeedTime = Random.Range(_newSeedMin, _newSeedMax);
        }
        else
        {
            //TODO: Change to GameManager when implemented.
            _currentTime += Time.deltaTime;
        }
    }

    private void AddSeed()
    {
        SeedsForSale.Add(_objectManager.GetRandomShopSeed());
    }

    #region Selling methods
    public float OpenSellTransaction(List<Plant> flowers)
    {
        _transactionOpen = true;
        //Calculate value.
        _currentOpenTransactionAmount = CalculateSale(flowers,true);
        //return value.
        return _currentOpenTransactionAmount;
    }

    public float CloseSellTransaction(bool confirmed,Action completed)
    {
        if (!confirmed)
            return 0.0f;

        _transactionOpen = false;
        var saleAmount = _currentOpenTransactionAmount;
        _currentOpenTransactionAmount = 0.0f;
        completed.Invoke();
        return saleAmount;
    }

    public void CancelSellTransaction()
    {
        _transactionOpen = false;
        _currentOpenTransactionAmount = 0.0f;
    }
    #endregion

    #region Buying Methods

    public Plant PickFromInventory()
    {
        try
        {
            return SeedsForSale[0];
        }
        catch (Exception)
        {
            return null;
        }
    }

    public float OpenBuyTransaction(List<Plant> seeds)
    {
        Debug.Log("ForSale Before = " + SeedsForSale.Count);
        _transactionOpen = true;
        for (int i = 0; i < seeds.Count - 1; i++)
        {
            SeedsForSale.RemoveAt(0);
        }

        Debug.Log("ForSale After = " + SeedsForSale.Count);
        SeedBasket = seeds;
        _currentOpenTransactionAmount = CalculateSale(seeds, false);
        return _currentOpenTransactionAmount;
    }

    public List<Plant> CloseBuyTransaction(bool confirmed, float amount, Action completed)
    {
        if (!confirmed || amount < _currentOpenTransactionAmount)
        {
            CancelBuyTransaction();
            return null;
        }

        _transactionOpen = false;
        var basket = SeedBasket;
        _currentOpenTransactionAmount = 0.0f;
        completed.Invoke();
        SeedBasket = new List<Plant>();
        return basket;
    }

    public void CancelBuyTransaction()
    {
        for (int i = 0; i < SeedBasket.Count - 1; i++)
        {
            SeedsForSale.Add(SeedBasket[i]);
        }
        _transactionOpen = false;
        _currentOpenTransactionAmount = 0.0f;
        SeedBasket = new List<Plant>();
    }
    #endregion

    #region Calculation Methods
    private float CalculateSale(List<Plant> flowers,bool selling)
    {
        float cumilativeTotal = 0.0f;
        for (int i = 0; i < flowers.Count; i++)
        {
            cumilativeTotal += GetFlowerPrice(flowers[i],selling);
        }
        return cumilativeTotal;
    }

    private float GetFlowerPrice(Plant plant,bool selling)
    {
        float price = 0.0f;
        switch (plant.Type)
        {
            case PlantType.Type0:
                price += selling ? 0.1f : 0.2f;
                break;
            case PlantType.Type1:
                price += selling ? 0.2f : 0.4f;
                break;
            case PlantType.Type2:
                price += selling ? 0.3f : 0.6f;
                break;
            case PlantType.Type3:
                price += selling ? 0.4f : 0.8f;
                break;
            case PlantType.Type4:
                price += selling ? 0.5f : 1.0f;
                break;
            case PlantType.Type5:
                price += selling ? 0.75f : 1.50f;
                break;
        }

        switch (plant.Rarity)
        {
            case PlantRarity.VeryCommon:
                price += selling ? 0.05f : 0.1f;
                break;
            case PlantRarity.Common:
                price += selling ? 0.25f : 0.5f;
                break;
            case PlantRarity.Rare:
                price += selling ? 0.75f : 1.50f;
                break;
            case PlantRarity.VeryRare:
                price += selling ? 1.5f : 3.0f;
                break;
            case PlantRarity.Legendary:
                price += selling ? 3.0f : 6.0f;
                break;
        }

        return price;
    }
    #endregion
}
